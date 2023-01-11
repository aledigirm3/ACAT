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
    public TextMeshProUGUI PerkMultiplierTimeLeftText;
    public Button Revive;
    public Button Shield;
    public Button Multiplier;
    public Button Pause;
    public Button Close;

    //PANEL UI
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject GameplayPanel;

    //PERKS
    public bool PerkMultiplierActivated;
    public float PerkMultiplierDuration;
    public float PerkMultiplierTimeLeft;

    //GAME MANAGEMENT
    public bool GamePaused;

    //SETUP
    void SetupUI()
    {
        PedoniText.text = Pedoni.ToString();
        CoinsText.text = Coins.ToString();

        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);

        PerkMultiplierTimeLeftText.gameObject.SetActive(false);
        PerkMultiplierTimeLeftText.text = PerkMultiplierDuration.ToString();
    }

    void SetupPerks()
    {
        PerkMultiplierActivated = false;
        PerkMultiplierTimeLeft = PerkMultiplierDuration;
    }

    void Start()
    {
        Coins = 0;
        Pedoni = 0;
        GamePaused = false;
        SetupPerks();
        SetupUI();
        Time.timeScale = 1;
    }

    void Update()
    {
        if (!GamePaused)
        {
            if (PerkMultiplierActivated)
            {
                PerkMultiplierTimeLeftText.text = (Mathf.Ceil(PerkMultiplierTimeLeft)).ToString();
                PerkMultiplierTimeLeft -= Time.deltaTime;
                if (PerkMultiplierTimeLeft <= 0)
                {
                    PerkMultiplierActivated = false;
                    PerkMultiplierTimeLeft = PerkMultiplierDuration;
                    PerkMultiplierTimeLeftText.gameObject.SetActive(false);
                    PerkMultiplierTimeLeftText.text = PerkMultiplierDuration.ToString();
                }
            }
        }
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
            if (PerkMultiplierActivated)
                Pedoni += 2;
            else    
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

        //Deattivo i perk
        PerkMultiplierActivated = false;
        PerkMultiplierTimeLeft = 0f;

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

    public void ActivatePerkMultiplier()
    {
        if (!PerkMultiplierActivated)
        {
            PerkMultiplierTimeLeftText.gameObject.SetActive(true);
            PerkMultiplierActivated = true;
        } 
    }
}
