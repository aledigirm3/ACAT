using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI Highscore;
    public TextMeshProUGUI Money;
    public Button MusicButton;
    public Button MuteMusicButton;
    public Button SoundButton;
    public Button MuteSoundButton;

    private void FirstStart()
    {
        if (!PlayerPrefs.HasKey("Music"))
            PlayerPrefs.SetInt("Music", 1);
        if (!PlayerPrefs.HasKey("Sound"))
            PlayerPrefs.SetInt("Sound", 1);
        if (!PlayerPrefs.HasKey("Highscore"))
            PlayerPrefs.SetInt("Highscore", 0);
        if (!PlayerPrefs.HasKey("Money"))
            PlayerPrefs.SetInt("Money", 0);
    }

    private void Setup()
    {
        Money.text = PlayerPrefs.GetInt("Money").ToString();
        Highscore.text = PlayerPrefs.GetInt("Highscore").ToString();

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
        print("Reloaded");
        FirstStart();
        Setup();
    }

    void Update()
    {
        Highscore.text = ("HIGH SCORE: " + PlayerPrefs.GetInt("Highscore").ToString());
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenStore()
    {
        SceneManager.LoadScene("Store");
    }

    public void MusicOn()
    {
        PlayerPrefs.SetInt("Music", 1);
        MuteMusicButton.gameObject.SetActive(false);
        MusicButton.gameObject.SetActive(true);
    }

    public void MusicOff()
    {
        PlayerPrefs.SetInt("Music", 0);
        MusicButton.gameObject.SetActive(false);
        MuteMusicButton.gameObject.SetActive(true);
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
