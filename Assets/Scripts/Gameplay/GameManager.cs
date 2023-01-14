using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject Bus;

    //STATS
    public int Coins;
    public int Pedoni;

    //UI
    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI PedoniText;
    public TextMeshProUGUI PedoniInGameOverText;
    public TextMeshProUGUI MaxPedoniText;
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

    //SETUP
    void SetupUI()
    {
        PedoniText.text = Pedoni.ToString();
        CoinsText.text = Coins.ToString();

        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
    }

    void Start()
    {
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
            Coins += 1;
            CoinsText.text = Coins.ToString();
        }
        else if (obj.tag == "Pedone")
        {
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

    public void OnGameOver()
    {
        Time.timeScale = 0;
        Gameover = true;

        //Deattivo i perk
        PerkManager.GetComponent<MultiplierPerk>().Deactivate();
        PerkManager.GetComponent<GhostPerk>().Deactivate();

        //Rimuovo elementi UI di Gameplay
        Close.gameObject.SetActive(false);
        GameplayPanel.SetActive(false);

        //Salvo il nuovo punteggio più alto e aggiungo le monete raccolte
        if (Pedoni > PlayerPrefs.GetInt("Highscore"))
            PlayerPrefs.SetInt("Highscore", Pedoni);
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + Coins);

        //Setup UI di Gameover
        MaxPedoniText.text = ("HIGHEST SCORE: " + PlayerPrefs.GetInt("Highscore").ToString());
        PedoniInGameOverText.text = ("SCORE: " + Pedoni);
        GameOverPanel.SetActive(true);
    }

    public void OnPause()
    {
        Time.timeScale = 0;
        GamePaused = true;
        Pause.gameObject.SetActive(false);
        Close.gameObject.SetActive(true);
        TogglePerks(false);
        PausePanel.gameObject.SetActive(true);
    }

    public void OnResume()
    {
        Time.timeScale = 1;
        Close.gameObject.SetActive(false);
        Pause.gameObject.SetActive(true);
        TogglePerks(true);
        PausePanel.gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    IEnumerator IncreaseDifficulty()
    {
        while (!Gameover)
        {
            yield return new WaitForSeconds(TimeBetweenDifficulties * Difficulty);
            Difficulty += 1;
            //Ferma la coroutine quando il livello di difficoltà è pari a 100
            if (Difficulty >= 100)
                StopCoroutine(coroutine);
        }
    }
}
