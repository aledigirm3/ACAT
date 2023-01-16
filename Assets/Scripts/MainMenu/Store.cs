using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class Store : MonoBehaviour
{
    public PlayfabManager playfabManager;
    
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI MultiplierPerkCounter;
    public TextMeshProUGUI GhostPerkCounter;

    public int MultiplierPerkPrice;
    public int GhostPerkPrice;

    public GameObject BuyWindow;
    public GameObject LimitReachedWindow;
    public GameObject NoMoneyWindow;
    public GameObject DialoguePanel;

    public AudioSource BackgroundMusic;
    public AudioSource ButtonMusic;


    private void DeactiveWholeDialoguePanel()
    {
        DialoguePanel.SetActive(false);
        BuyWindow.SetActive(false);
        LimitReachedWindow.SetActive(false);
        NoMoneyWindow.SetActive(false);
    }

    private void SetupUI()
    {
        DeactiveWholeDialoguePanel();
    }

    // Start is called before the first frame update
    void Start()
    {
        ButtonMusic.Stop();
        if (PlayerPrefs.GetInt("Music") == 1)
            BackgroundMusic.Play();
        else
            BackgroundMusic.Stop();
        MultiplierPerkPrice = 10;
        GhostPerkPrice = 30;
        MoneyText.text = PlayerPrefs.GetInt("Money").ToString();
        MultiplierPerkCounter.text = PlayerPrefs.GetInt("MultiplierPerk").ToString() + "/5";
        GhostPerkCounter.text = PlayerPrefs.GetInt("GhostPerk").ToString() + "/5";
        CheckIfPerkLimitHasReached();
    }

    public void CheckIfPerkLimitHasReached()
    {
        if (PlayerPrefs.GetInt("MultiplierPerk") == 5)
        {
            MultiplierPerkCounter.color = new Color(1.0f, 0f, 0f, 1.0f);
        }
        if (PlayerPrefs.GetInt("GhostPerk") == 5)
        {
            GhostPerkCounter.color = new Color(1.0f, 0f, 0f, 1.0f);
        }
    }

    private void SendRequestBuyPerk(string perkName, int perkPrice, TextMeshProUGUI perkCounter)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { perkName, (PlayerPrefs.GetInt(perkName) + 1).ToString() },
                { "Money", (PlayerPrefs.GetInt("Money") - perkPrice).ToString() }
            }
        };
        PlayFabClientAPI.UpdateUserData(
            request,
            result =>
            {
                PlayFabClientAPI.GetUserData(
                    new GetUserDataRequest(), 
                    result =>
                    {
                        PlayerPrefs.SetInt("Money", int.Parse(result.Data["Money"].Value));
                        MoneyText.text = PlayerPrefs.GetInt("Money").ToString();

                        PlayerPrefs.SetInt(perkName, int.Parse(result.Data[perkName].Value));
                        perkCounter.text = PlayerPrefs.GetInt(perkName).ToString() + "/5";
                        CheckIfPerkLimitHasReached();
                    },
                    error => Debug.Log(error.ErrorMessage));
            },
            error => Debug.Log(error.ErrorMessage));
    }

    public void BuyPerk(string perkName)
    {
        ButtonMusic.Play();
        int perkPrice = (perkName == "MultiplierPerk") ? MultiplierPerkPrice : GhostPerkPrice;
        TextMeshProUGUI perkCounter = (perkName == "MultiplierPerk") ? MultiplierPerkCounter : GhostPerkCounter;

        if (PlayerPrefs.GetInt("Money") >= perkPrice)
        {
            if (PlayerPrefs.GetInt(perkName) < 5)
            {
                SendRequestBuyPerk(perkName, perkPrice, perkCounter);
            }
            else
            {
                DialoguePanel.SetActive(true);
                LimitReachedWindow.SetActive(true);
            }
        }
        else
        {
            DialoguePanel.SetActive(true);
            NoMoneyWindow.SetActive(true);
        }
    }

    public void GoToMainMenu()
    {
         ButtonMusic.Play();
        SceneManager.LoadScene("Menu");
    }

    public void CloseDialogueWindow()
    {
        ButtonMusic.Play();
        DeactiveWholeDialoguePanel();
    }

}
