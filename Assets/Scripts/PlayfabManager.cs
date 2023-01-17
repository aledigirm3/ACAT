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
    public AudioManager audioManager;

    [Header("MENU")]
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
        if (result.InfoResultPayload.PlayerProfile.DisplayName != null)
        { 
            MainMenu.Username = result.InfoResultPayload.PlayerProfile.DisplayName;
            PlayFabClientAPI.GetUserData(
                new GetUserDataRequest(), 
                result =>
                {
                    if (result.Data != null)
                    {
                        if (result.Data.ContainsKey("Money") && result.Data.ContainsKey("GhostPerk") && result.Data.ContainsKey("MultiplierPerk"))
                        {
                            //Imposto i dati dell'utente
                            PlayerPrefs.SetInt("Money", int.Parse(result.Data["Money"].Value));
                            PlayerPrefs.SetInt("GhostPerk", int.Parse(result.Data["GhostPerk"].Value));
                            PlayerPrefs.SetInt("MultiplierPerk", int.Parse(result.Data["MultiplierPerk"].Value));

                            //Richiedo l'highscore al server dalla leaderboard e carico la scena Menu
                            GetPlayerHighscore("Menu");
                        }
                        else
                            SetDefaultPlayerData();
                    }
                    else
                        SetDefaultPlayerData();
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
            audioManager.PlayButtonSound();

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
            audioManager.PlayButtonSound();
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
            audioManager.PlayButtonSound();

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
            audioManager.PlayButtonSound();

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
            audioManager.PlayButtonSound();
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
            audioManager.PlayButtonSound();
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
        audioManager.PlayButtonSound();
        BackButton.gameObject.SetActive(false);
        IsLogginIn = false;
        IsSigninUp = false;
        IsResettingPassword = false;
        OnLogin();
    }

    //########## LEADERBOARD ##########//

    public int GetPlayerHighscore(string loadScene = null)
    {
        int score = 0;
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "PedoniLeaderboard",
            MaxResultsCount = 1
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(
            request,
            result => 
            { 
                PlayerPrefs.SetInt("Highscore", result.Leaderboard[0].StatValue);
                if (loadScene != null)
                    SceneManager.LoadScene("Menu");
            },
            error => 
            {
                PlayerPrefs.SetInt("Highscore", 0);
                if (loadScene != null)
                    SceneManager.LoadScene("Menu");
            });

        return score;
    }

    //########## PLAYER DATA ##########//

    public void GetAllPlayerData()
    {
        PlayFabClientAPI.GetUserData(
            new GetUserDataRequest(),
            result => Debug.Log("Data received!"), 
            OnError);
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
        PlayFabClientAPI.UpdateUserData(
            request,
            result =>
            {
                //Imposto i dati dell'utente di default
                PlayerPrefs.SetInt("Money", 0);
                PlayerPrefs.SetInt("GhostPerk", 0);
                PlayerPrefs.SetInt("MultiplierPerk", 0);
                PlayerPrefs.SetInt("Highscore", 0);

                SceneManager.LoadScene("Menu");
            },
            OnError);
    }

    void OnError(PlayFabError error)
    {
        MessageText.text = error.ErrorMessage;
    }
}
