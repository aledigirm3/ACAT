using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //STATS
    public int Coins;
    public int Pedoni;

    //UI
    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI PedoniText;
    public TextMeshProUGUI PedoniInGameOverText;
    public TextMeshProUGUI MaxPedoniText;
    public Button Revive;
    public Button Shield;
    public Button Multiplier;
    public Button Pause;
    public Button Close;

    //PANEL UI
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject GameplayPanel;

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
        SetupUI();
        Time.timeScale = 1;
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
            //TODO: Controlla perk multiplier
            Pedoni += 1;
            PedoniText.text = Pedoni.ToString();
        }
        Destroy(obj);
    }

    public void TogglePerks(bool value)
    {
        Revive.gameObject.SetActive(value);
        Shield.gameObject.SetActive(value);
        Multiplier.gameObject.SetActive(value);
    }

    public void OnGameOver()
    {
        Time.timeScale = 0;

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
}
