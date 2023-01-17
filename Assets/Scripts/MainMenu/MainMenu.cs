using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class MainMenu : MonoBehaviour
{
    public PlayfabManager playfabManager;
    public AudioManager audioManager;

    public static string Username;

    public TextMeshProUGUI HighscoreText;
    public TextMeshProUGUI MoneyText;

    //### LEADERBOARD ###//
    public GameObject LeaderboardPanel;
    public GameObject LeaderboardRowPrefab;
    public Transform LeaderboardRowsParent;

    void Start()
    {
        Application.targetFrameRate = 60;
        MoneyText.text = PlayerPrefs.GetInt("Money").ToString();
        HighscoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("Highscore").ToString();
    }

    public void PlayGame()
    {
        audioManager.PlayButtonSound();
        SceneManager.LoadScene("Game");
    }

    public void OpenStore()
    {
        audioManager.PlayButtonSound();
        SceneManager.LoadScene("Store");
    }

    public void OpenLeaderboard()
    {
        audioManager.PlayButtonSound();
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PedoniLeaderboard",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(
            request,
            result =>
            {
                foreach (var item in result.Leaderboard)
                {
                    if (item.DisplayName != null)
                    {
                        //Istanzia row nella leaderboard
                        GameObject newGo = Instantiate(LeaderboardRowPrefab, LeaderboardRowsParent);
                        TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();

                        //Inserisci dati nella row della leaderboard
                        texts[0].text = (item.Position + 1).ToString();
                        texts[1].text = item.DisplayName;
                        texts[2].text = item.StatValue.ToString();

                        //Se il giocatore è l'utente connesso, cambia il colore della row
                        if (item.DisplayName == MainMenu.Username)
                        {
                            texts[0].color = Color.yellow;
                            texts[1].color = Color.yellow;
                            texts[2].color = Color.yellow;
                        }
                    }
                }
                //Mostra la leaderboard
                LeaderboardPanel.gameObject.SetActive(true);
            },
            error =>
            {
                Debug.Log(error.ErrorMessage);
            });
    }

    public void CloseLeaderboard()
    {
        audioManager.PlayButtonSound();
        //Rimuovi eventuali dati precedenti dalla leaderboard
        foreach (Transform item in LeaderboardRowsParent)
        {
            Destroy(item.gameObject);
        }
        LeaderboardPanel.gameObject.SetActive(false);
    }
}
