using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayfabManager : MonoBehaviour
{
    [Header("MENU_UI")]
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI MultiplierPerkText;
    public TextMeshProUGUI GhostPerkText;

    [Header("UI")]
    public TextMeshProUGUI MessageText;
    public TMP_InputField emailInput;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public Button LoginButton;
    public Button SignupButton;
    public Button ForgotPasswordButton;
    public Button BackButton;

    public bool IsLogginIn;
    public bool IsSigninUp;
    public bool IsResettingPassword;

    void Start()
    {
        IsLogginIn = true;
        IsResettingPassword= false;
    }

    //########## LOGIN PAGE ##########//

    void SetDisplayNameAfterSigninUp(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Display name set");
        OnLogin();
    }

    void OnSignupSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Signed up!");
        IsSigninUp = false;
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = usernameInput.text
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, SetDisplayNameAfterSigninUp, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Logged in!");
        if (result.InfoResultPayload.PlayerProfile.DisplayName != null)
        { 
            MainMenu.Username = result.InfoResultPayload.PlayerProfile.DisplayName;
            PlayFabClientAPI.GetUserData(
                new GetUserDataRequest(), 
                result =>
                {
                    OnDataReceived(result);
                    SceneManager.LoadScene("Menu");
                }, 
                OnError);
        }
        else
        {
            MessageText.text = "ERROR: Contact a developer";
        }
            
    }

    void OnPasswordResetSuccess(SendAccountRecoveryEmailResult result)
    {
        IsResettingPassword = false;
        MessageText.text = "Password reset email sent!";
        OnLogin();
    }

    public void OnSignup()
    {
        if (!IsSigninUp)
        {
            IsLogginIn = false;
            IsSigninUp = true;

            MessageText.text = "Please register";

            usernameInput.GetComponent<Transform>().localPosition = new Vector3(0f, 75f, 0f);
            passwordInput.GetComponent<Transform>().localPosition = new Vector3(0f, -75f, 0f);

            emailInput.gameObject.SetActive(true);
            BackButton.gameObject.SetActive(true);

            LoginButton.gameObject.SetActive(false);
            ForgotPasswordButton.gameObject.SetActive(false);
        }
        else
        { 
            if (passwordInput.text.Length < 6)
            {
                MessageText.text = "Password too short!";
                return;
            }
            MessageText.text = "Signin up...";
            var request = new RegisterPlayFabUserRequest
            {
                Email = emailInput.text,
                Password = passwordInput.text,
                Username = usernameInput.text,
                RequireBothUsernameAndEmail = true
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnSignupSuccess, OnError);
        }
    }

    public void OnLogin()
    {
        if (!IsLogginIn)
        {
            IsLogginIn = true;

            MessageText.text = "Please login";

            usernameInput.gameObject.SetActive(true);
            passwordInput.gameObject.SetActive(true);
            emailInput.gameObject.SetActive(false);

            usernameInput.GetComponent<Transform>().localPosition = new Vector3(0f, 200f, 0f);
            passwordInput.GetComponent<Transform>().localPosition = new Vector3(0f, 50f, 0f);

            LoginButton.gameObject.SetActive(true);
            SignupButton.gameObject.SetActive(true);
            ForgotPasswordButton.gameObject.SetActive(true);
        }
        else
        {
            MessageText.text = "Logging in...";
            var request = new LoginWithPlayFabRequest
            {
                Username = usernameInput.text,
                Password = passwordInput.text,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true
                }
            };
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
        }
    }

    public void OnResetPassword()
    {
        if (IsLogginIn)
        {
            IsLogginIn = false;
            IsResettingPassword = true;

            MessageText.text = "Please insert email";

            usernameInput.gameObject.SetActive(false);
            passwordInput.gameObject.SetActive(false);
            emailInput.gameObject.SetActive(true);

            BackButton.gameObject.SetActive(true);
            LoginButton.gameObject.SetActive(false);
            SignupButton.gameObject.SetActive(false);
        }
        else
        {
            MessageText.text = "Sending email reset request...";
            var request = new SendAccountRecoveryEmailRequest
            {
                Email = emailInput.text,
                TitleId = "1A2A4"
            };
            PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordResetSuccess, OnError);
        }
    }

    public void OnBack()
    {
        BackButton.gameObject.SetActive(false);
        IsLogginIn = false;
        IsSigninUp = false;
        IsResettingPassword = false;
        OnLogin();
    }

    //########## LEADERBOARD ##########//

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate {
                    StatisticName = "PedoniLeaderboard",
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful leaderboard sent");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PedoniLeaderboard",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard) 
        {
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }

    //########## PLAYER DATA ##########//

    public void GetAllPlayerData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    public void SetDefaultPlayerData()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Money", "0" },
                { "GhostPerk", "0" },
                { "MultiplierPerk", "0" }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    public void AddMoney(int money)
    {
        var temp = PlayerPrefs.GetInt("Money") + money;
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Money", temp.ToString() }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        GetAllPlayerData();
    }


    void OnDataReceived(GetUserDataResult result)
    {
        Debug.Log("Data received!");
        if (result.Data != null)
        {
            if (result.Data.ContainsKey("Money") && result.Data.ContainsKey("GhostPerk") && result.Data.ContainsKey("MultiplierPerk"))
            {
                PlayerPrefs.SetInt("Money", int.Parse(result.Data["Money"].Value));
                if (MoneyText != null)
                    MoneyText.text = PlayerPrefs.GetInt("Money").ToString();

                PlayerPrefs.SetInt("GhostPerk", int.Parse(result.Data["GhostPerk"].Value));
                if (GhostPerkText != null)
                    GhostPerkText.text = PlayerPrefs.GetInt("GhostPerk").ToString() + "/5";

                PlayerPrefs.SetInt("MultiplierPerk", int.Parse(result.Data["MultiplierPerk"].Value));
                if (MultiplierPerkText != null)
                    MultiplierPerkText.text = PlayerPrefs.GetInt("MultiplierPerk").ToString() + "/5";
            }
            else
            {
                Debug.Log("Data reset!");
                SetDefaultPlayerData();
            }
        }
        else
        {
            Debug.Log("Data reset!");
            SetDefaultPlayerData();
        }
    }

    void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Successful user data send");
    }

    void OnError(PlayFabError error)
    {
        MessageText.text = error.ErrorMessage;
    }
}
