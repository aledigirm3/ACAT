using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Button MusicButton;
    public Button MuteMusicButton;
    public Button SoundButton;
    public Button MuteSoundButton;

    //### AUDIO ###//

    //Musica
    public AudioSource BackgroundMenuMusic;
    public AudioSource BackgroundGameplayMusic;

    //Effetti
    public AudioSource ButtonSound;
    public AudioSource CoinSound;
    public AudioSource PedoneSound;

    //#############//


    void Start()
    {
        if (!PlayerPrefs.HasKey("Music"))
            PlayerPrefs.SetInt("Music", 1);
        if (!PlayerPrefs.HasKey("Sound"))
            PlayerPrefs.SetInt("Sound", 1);

        Debug.Log("Music: " + PlayerPrefs.GetInt("Music").ToString());
        Debug.Log("Sound: " + PlayerPrefs.GetInt("Sound").ToString());

        if (PlayerPrefs.GetInt("Music") == 1)
            MusicOn();
        else
            MusicOff();

        if (PlayerPrefs.GetInt("Sound") == 1)
            SoundOn();
        else
            SoundOff();
    }

    public void MusicOn()
    {
        PlayerPrefs.SetInt("Music", 1);
        if (SceneManager.GetActiveScene().name == "Game")
        {
            PlayBackgroundGameplayMusic();
        }
        else
        {
            PlayBackgroundMenuMusic();
        }
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            MuteMusicButton.gameObject.SetActive(false);
            MusicButton.gameObject.SetActive(true);
        }
    }

    public void MusicOff()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            PauseBackgroundGameplayMusic();
        }
        else
        {
            PauseBackgroundMenuMusic();
        }
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            MuteMusicButton.gameObject.SetActive(true);
            MusicButton.gameObject.SetActive(false);
        }
        PlayerPrefs.SetInt("Music", 0);
    }

    public void SoundOn()
    {
        PlayerPrefs.SetInt("Sound", 1);
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            MuteSoundButton.gameObject.SetActive(false);
            SoundButton.gameObject.SetActive(true);
        }
        ButtonSound.volume = 1;
    }

    public void SoundOff()
    {
        PlayerPrefs.SetInt("Sound", 0);
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            SoundButton.gameObject.SetActive(false);
            MuteSoundButton.gameObject.SetActive(true);
        }
        ButtonSound.volume = 0;
    }

    public void PlayBackgroundMenuMusic()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
            if (!BackgroundMusic.instance.gameObject.GetComponent<AudioSource>().isPlaying)
                BackgroundMusic.instance.gameObject.GetComponent<AudioSource>().Play();
    }

    public void PauseBackgroundMenuMusic()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
            BackgroundMusic.instance.gameObject.GetComponent<AudioSource>().Pause();
    }

    public void PlayBackgroundGameplayMusic()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
            BackgroundGameplayMusic.Play();
    }

    public void PauseBackgroundGameplayMusic()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
            BackgroundGameplayMusic.Pause();
    }

    public void PlayButtonSound()
    {
        ButtonSound.Play();
    }

    public void PlayCoinSound()
    {
        CoinSound.Play();
    }

    public void PlayPedoneSound()
    {
        PedoneSound.Play();
    }
}
