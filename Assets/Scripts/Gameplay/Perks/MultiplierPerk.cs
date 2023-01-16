using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class MultiplierPerk : MonoBehaviour
{
    //UI
    public TextMeshProUGUI UITimeLeftText;
    public Button UIButton;

    //PERK DATA
    public bool IsActivated;
    public float Duration;
    public float TimeLeft;

    //AUDIO
    public AudioSource ButtonMusic;

    public Image[] Available;

    public GameManager GameManagerObj;

    // Start is called before the first frame update
    void Start()
    {
        IsActivated = false;
        TimeLeft = Duration;

        UITimeLeftText.gameObject.SetActive(false);
        UITimeLeftText.text = Duration.ToString();
        if (PlayerPrefs.GetInt("MultiplierPerk") == 0)
        {
            UIButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
        }
        for (int i = 0; i < PlayerPrefs.GetInt("MultiplierPerk"); i++)
        {
            Available[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerObj.GamePaused)
        {
            if (IsActivated)
            {
                UITimeLeftText.text = (Mathf.Ceil(TimeLeft)).ToString();
                TimeLeft -= Time.deltaTime;
                if (TimeLeft <= 0)
                {
                    IsActivated = false;
                    TimeLeft = Duration;
                    UITimeLeftText.gameObject.SetActive(false);
                    UITimeLeftText.text = Duration.ToString();
                }
            }
        }
    }

    public void Toggle(bool value)
    {
        UIButton.gameObject.SetActive(value);
    }

    public void Deactivate()
    {
        IsActivated = false;
        TimeLeft = 0f;
    }

    private void SendRequesteUsePerk()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "MultiplierPerk", (PlayerPrefs.GetInt("MultiplierPerk") - 1).ToString() }
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
                        Available[PlayerPrefs.GetInt("MultiplierPerk") - 1].gameObject.SetActive(false);
                        PlayerPrefs.SetInt("MultiplierPerk", int.Parse(result.Data["MultiplierPerk"].Value));
                        if (PlayerPrefs.GetInt("MultiplierPerk") == 0)
                        {
                            UIButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                        }
                        UITimeLeftText.gameObject.SetActive(true);
                        IsActivated = true;
                    },
                    error => Debug.Log(error.ErrorMessage));
            },
            error => Debug.Log(error.ErrorMessage));
    }


    public void Activate()
    {
        if (PlayerPrefs.GetInt("MultiplierPerk") > 0)
        {
            if (!IsActivated)
            {
                ButtonMusic.Play();
                SendRequesteUsePerk();
            }
        }
    }
}
