using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermataSpawner : MonoBehaviour
{
    // Oggetti da istanziare
    public GameObject Fermata;
    public GameObject Pedone;

    // Oggetti esterni 
    public BackgroundScroller Strada;
    public GameManager GameManagerObj;
    public GameObject Bus;

    // Dati per gli oggetti da istanziare
    public float BaseSpawnTime;
    private float[] xPositions;

    void Start()
    {
        xPositions = new float[] { -3.5f, 3.5f };
        StartCoroutine(SpawnFermate());
    }

    void Fermate()
    {
        int numberOfPedoniToSpawn = Random.Range(0, 2 + ((int)Mathf.Sqrt(GameManagerObj.Difficulty)));
        float xPosition = xPositions[Random.Range(0, 2)];
        for (float i = 0; i < numberOfPedoniToSpawn; i++)
        {
            GameObject pedoneObj = Instantiate(Pedone);
            float yPosition = transform.position.y + 0.5f + ((i + 1) / 2);
            pedoneObj.GetComponent<PedoneMovement>().SetupPedone(Strada.Speed, xPosition, yPosition, Bus);
        }
        GameObject fermataObj = Instantiate(Fermata, new Vector2(xPosition, transform.position.y), Quaternion.Euler(0, 0, 0));
        fermataObj.GetComponent<FermataMovement>().SetupFermata(Strada.Speed);
    }

    IEnumerator SpawnFermate()
    {
        while (true)
        {
            yield return new WaitForSeconds((float)(BaseSpawnTime / (Mathf.Sqrt(GameManagerObj.Difficulty * 0.5f))));
            Fermate();
        }
    }
}
