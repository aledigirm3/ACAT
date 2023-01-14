using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GhostPerk : MonoBehaviour
{
    //UI
    public TextMeshProUGUI UITimeLeftText;
    public Button UIButton;

    //PERK DATA
    public bool IsActivated;
    public float Duration;
    public float TimeLeft;

    public Image[] Available;

    public GameManager GameManagerObj;

    // Start is called before the first frame update
    void Start()
    {
        IsActivated = false;
        TimeLeft = Duration;

        UITimeLeftText.gameObject.SetActive(false);
        UITimeLeftText.text = Duration.ToString();

        if (PlayerPrefs.GetInt("GhostPerk") == 0)
        {
            UIButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
        }

        for (int i = 0; i < PlayerPrefs.GetInt("GhostPerk"); i++)
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
                    GameManagerObj.Bus.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
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
        GameManagerObj.Bus.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    public void Activate()
    {
        if (PlayerPrefs.GetInt("GhostPerk") > 0)
        {
            if (!IsActivated)
            {
                Available[PlayerPrefs.GetInt("GhostPerk") - 1].gameObject.SetActive(false);
                PlayerPrefs.SetInt("GhostPerk", PlayerPrefs.GetInt("GhostPerk") - 1);
                if (PlayerPrefs.GetInt("GhostPerk") == 0)
                {
                    UIButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                }
                UITimeLeftText.gameObject.SetActive(true);
                GameManagerObj.Bus.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                IsActivated = true;
            }
        }
    }
}
