using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{

    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI MultiplierPerkCounter;
    public TextMeshProUGUI ShieldPerkCounter;

    public int MultiplierPerkPrice;
    public int ShieldPerkPrice;

    public GameObject BuyWindow;
    public GameObject LimitReachedWindow;
    public GameObject NoMoneyWindow;
    public GameObject DialoguePanel;

    private void SetupUI()
    {
        DialoguePanel.SetActive(false);
        BuyWindow.SetActive(false);
        LimitReachedWindow.SetActive(false);
        NoMoneyWindow.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        MultiplierPerkPrice = 10;
        ShieldPerkPrice = 30;
        MoneyText.text = PlayerPrefs.GetInt("Money").ToString();
        MultiplierPerkCounter.text = PlayerPrefs.GetInt("MultiplierPerk").ToString() + "/5";
        ShieldPerkCounter.text = PlayerPrefs.GetInt("ShieldPerk").ToString() + "/5";
        CheckIfPerkLimitHasReached();
    }

    public void CheckIfPerkLimitHasReached()
    {
        if (PlayerPrefs.GetInt("MultiplierPerk") == 5)
        {
            MultiplierPerkCounter.color = new Color(1.0f, 0.23f, 1.0f, 1.0f);
        }
        if (PlayerPrefs.GetInt("ShieldPerk") == 5)
        {
            MultiplierPerkCounter.color = new Color(1.0f, 0.23f, 1.0f, 1.0f);
        }
    }

    public void BuyMultiplierPerk()
    {
        if (PlayerPrefs.GetInt("Money") >= MultiplierPerkPrice)
        {
            if (PlayerPrefs.GetInt("MultiplierPerk") < 5)
            {
                //BUYWINDOW

                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - MultiplierPerkPrice);
                PlayerPrefs.SetInt("MultiplierPerk", PlayerPrefs.GetInt("MultiplierPerk") + 1);
                CheckIfPerkLimitHasReached();
            }
            else
            {
                //LIMITREACHEDWINDOW
            }
        }
        else
        {
            //NOMONEYWINDOW
        }
    }

    public void BuyShieldPerk()
    {
        if (PlayerPrefs.GetInt("Money") >= ShieldPerkPrice)
        {
            if (PlayerPrefs.GetInt("ShieldPerk") < 5)
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - ShieldPerkPrice);
                PlayerPrefs.SetInt("ShieldPerk", PlayerPrefs.GetInt("ShieldPerk") + 1);
                CheckIfPerkLimitHasReached();
            }
        }
    }

}
