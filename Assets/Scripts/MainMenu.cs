using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI MaxScore;
    public Button MusicButton;
    public Button SoundButton;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Music", 1);
        PlayerPrefs.SetInt("Sound", 1);
    }

    // Update is called once per frame
    void Update()
    {
        MaxScore.text = ("MAX SCORE: " + PlayerPrefs.GetInt("maxPedoni").ToString());
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenStore()
    {
        SceneManager.LoadScene("Store");
    }

    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
    }

    public void ToggleMusic()
    {
        if(PlayerPrefs.GetInt("Music") == 1)
        {
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 1);
        }
    }
}
