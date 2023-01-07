using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
        public Text maxPedoniText;
        public Text pedoniText;
        public acatBus bus;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        maxPedoniText.text = ("PUNTEGGIO MAX: " + PlayerPrefs.GetInt("maxPedoni").ToString());
        pedoniText.text = ("PUNTEGGIO: " + bus.pedoni.ToString());
    }

    public void Restart(){
        SceneManager.LoadScene("SampleScene");
    }
}
