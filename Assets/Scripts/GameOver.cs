using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI maxPedoniText;
    public TextMeshProUGUI pedoniText;
    public acatBus bus;

    void Update()
    {
        maxPedoniText.text = ("PUNTEGGIO MAX: " + PlayerPrefs.GetInt("maxPedoni").ToString());
        pedoniText.text = ("PUNTEGGIO: " + bus.pedoni.ToString());
    }

    public void Restart(){
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
