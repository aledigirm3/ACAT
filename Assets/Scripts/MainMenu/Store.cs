using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Store : MonoBehaviour
{

    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI MultiplierPerkCounter;
    public TextMeshProUGUI GhostPerkCounter;

    public int MultiplierPerkPrice;
    public int GhostPerkPrice;

    public GameObject BuyWindow;
    public GameObject LimitReachedWindow;
    public GameObject NoMoneyWindow;
    public GameObject DialoguePanel;

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

    public void BuyPerk(string perkName)
    {
        int perkPrice = (perkName == "MultiplierPerk") ? MultiplierPerkPrice : GhostPerkPrice;
        TextMeshProUGUI perkCounter = (perkName == "MultiplierPerk") ? MultiplierPerkCounter : GhostPerkCounter;

        if (PlayerPrefs.GetInt("Money") >= perkPrice)
        {
            if (PlayerPrefs.GetInt(perkName) < 5)
            {
                //BUYWINDOW

                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - perkPrice);
                PlayerPrefs.SetInt(perkName, PlayerPrefs.GetInt(perkName) + 1);
                MoneyText.text = PlayerPrefs.GetInt("Money").ToString();
                perkCounter.text = PlayerPrefs.GetInt(perkName).ToString() + "/5";
                CheckIfPerkLimitHasReached();
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
        SceneManager.LoadScene("Menu");
    }

    public void CloseDialogueWindow()
    {
        DeactiveWholeDialoguePanel();
    }

}
