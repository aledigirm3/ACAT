using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class GameManager : MonoBehaviour
{
    public GameObject Bus;
    public AudioManager audioManager;

    //STATS
    public int Coins;
    public int Pedoni;

    //UI
    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI PedoniText;
    public TextMeshProUGUI PedoniInGameOverText;
    public TextMeshProUGUI HighscoreText;
    public Button Pause;
    public Button Close;

    //PANEL UI
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject GameplayPanel;
    public GameObject PerkManager;

    //GAME MANAGEMENT
    public bool GamePaused;
    public bool Gameover;
    public float Difficulty;
    public float TimeBetweenDifficulties;
    
    private Coroutine coroutine;

    void SetupUI()
    {
        PedoniText.text = Pedoni.ToString();
        CoinsText.text = Coins.ToString();

        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
    }

    void Start()
    {
        Destroy(BackgroundMusic.instance.gameObject);
        Coins = 0;
        Pedoni = 0;
        Difficulty = 1;
        GamePaused = false;
        Gameover = false;
        SetupUI();
        Time.timeScale = 1;
        coroutine = StartCoroutine(IncreaseDifficulty());
    }

    //Controllo di collisioni "friendly" aventi comportamenti simili
    public void FriendlyCollision(GameObject obj)
    {
        if (obj.tag == "Coin")
        {
            audioManager.PlayCoinSound();
            Coins += 1;
            CoinsText.text = Coins.ToString();
        }
        else if (obj.tag == "Pedone")
        {
            audioManager.PlayPedoneSound();
            if (PerkManager.GetComponent<MultiplierPerk>().IsActivated)
                Pedoni += 2;
            else    
                Pedoni += 1;
            PedoniText.text = Pedoni.ToString();
        }
        Destroy(obj);
    }

    public void TogglePerks(bool value)
    {
        PerkManager.GetComponent<MultiplierPerk>().Toggle(value);
        PerkManager.GetComponent<GhostPerk>().Toggle(value);
    }

    private void SendAddCoinsRequest()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Money", (PlayerPrefs.GetInt("Money") + Coins).ToString() }
            }
        };
        PlayFabClientAPI.UpdateUserData(
            request,
            result =>
            {
                PlayFabClientAPI.GetUserData(
                    new GetUserDataRequest(),
                    result => PlayerPrefs.SetInt("Money", int.Parse(result.Data["Money"].Value)),
                    error => Debug.Log(error.ErrorMessage));
            },
            error => Debug.Log(error.ErrorMessage));
    }

    private void SendNewHighscoreRequest()
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate {
                    StatisticName = "PedoniLeaderboard",
                    Value = Pedoni
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(
            request, 
            result =>
            {
                PlayerPrefs.SetInt("Highscore", Pedoni);
                HighscoreText.text = ("NEW HIGHSCORE: " + PlayerPrefs.GetInt("Highscore").ToString());
            }, 
            error => Debug.Log(error.ErrorMessage));
    }

    public void OnGameOver()
    {
        Time.timeScale = 0;
        Gameover = true;
        audioManager.PauseBackgroundGameplayMusic();

        //Deattivo i perk
        PerkManager.GetComponent<MultiplierPerk>().Deactivate();
        PerkManager.GetComponent<GhostPerk>().Deactivate();

        //Rimuovo elementi UI di Gameplay
        Close.gameObject.SetActive(false);
        GameplayPanel.SetActive(false);

        //Salvo il nuovo punteggio più alto
        if (Pedoni > PlayerPrefs.GetInt("Highscore"))
            SendNewHighscoreRequest();
        else
            HighscoreText.text = ("HIGHSCORE: " + PlayerPrefs.GetInt("Highscore").ToString());

        //Salvo le monete raccolte
        SendAddCoinsRequest();

        PedoniInGameOverText.text = ("SCORE: " + Pedoni);
        GameOverPanel.SetActive(true);
    }

    public void OnPause()
    {
        audioManager.PlayButtonSound();
        Time.timeScale = 0;
        GamePaused = true;
        Pause.gameObject.SetActive(false);
        Close.gameObject.SetActive(true);
        TogglePerks(false);
        PausePanel.gameObject.SetActive(true);
        audioManager.PauseBackgroundGameplayMusic();
    }

    public void OnResume()
    {
        audioManager.PlayButtonSound();
        Time.timeScale = 1;
        Close.gameObject.SetActive(false);
        Pause.gameObject.SetActive(true);
        TogglePerks(true);
        PausePanel.gameObject.SetActive(false);
        audioManager.PlayBackgroundGameplayMusic();
    }

    public void Restart()
    {
        audioManager.PlayButtonSound();
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        audioManager.PlayButtonSound();
        SceneManager.LoadScene("Menu");
    }

    IEnumerator IncreaseDifficulty()
    {
        while (!Gameover)
        {
            yield return new WaitForSeconds(TimeBetweenDifficulties * Difficulty);
            Difficulty += 1;
            //Ferma la coroutine quando il livello di difficolt� � pari a 100
            if (Difficulty >= 100)
                StopCoroutine(coroutine);
        }
    }
}
