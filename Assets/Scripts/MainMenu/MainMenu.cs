using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public static string Username;

    public TextMeshProUGUI HighscoreText;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI UsernameText;
    public Button MusicButton;
    public Button MuteMusicButton;
    public Button SoundButton;
    public Button MuteSoundButton;

    public PlayfabManager playfabManager;

    public AudioSource MenuMusic;
    public AudioSource ButtonMusic;

    private void FirstStart()
    {
        if (!PlayerPrefs.HasKey("Music"))
            PlayerPrefs.SetInt("Music", 1);
        if (!PlayerPrefs.HasKey("Sound"))
            PlayerPrefs.SetInt("Sound", 1);
        if (!PlayerPrefs.HasKey("Highscore"))
            PlayerPrefs.SetInt("Highscore", 0);
    }

    private void Setup()
    {
        ButtonMusic.Stop();
        MoneyText.text = PlayerPrefs.GetInt("Money").ToString();
        HighscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();

        if (PlayerPrefs.GetInt("Music") == 1)
            MusicOn();
        else
            MusicOff();

        if (PlayerPrefs.GetInt("Sound") == 1)
            SoundOn();
        else
            SoundOff();
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        UsernameText.text = "Logged in as: " + Username;
        FirstStart();
        Setup();
    }

    void Update()
    {
        HighscoreText.text = ("HIGH SCORE: " + PlayerPrefs.GetInt("Highscore").ToString());
    }

    public void PlayGame()
    {
        ButtonMusic.Play();
        SceneManager.LoadScene("Game");
    }

    public void OpenStore()
    {
        ButtonMusic.Play();
        SceneManager.LoadScene("Store");
    }

    public void MusicOn()
    {
        MenuMusic.Play();
        PlayerPrefs.SetInt("Music", 1);
        MuteMusicButton.gameObject.SetActive(false);
        MusicButton.gameObject.SetActive(true);
    }

    public void MusicOff()
    {
        PlayerPrefs.SetInt("Music", 0);
        MusicButton.gameObject.SetActive(false);
        MuteMusicButton.gameObject.SetActive(true);
        MenuMusic.Pause();
    }

    public void SoundOn()
    {
        PlayerPrefs.SetInt("Sound", 1);
        MuteSoundButton.gameObject.SetActive(false);
        SoundButton.gameObject.SetActive(true);
    }
    
    public void SoundOff()
    {
        PlayerPrefs.SetInt("Sound", 0);
        SoundButton.gameObject.SetActive(false);
        MuteSoundButton.gameObject.SetActive(true);
    }
}
