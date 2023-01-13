using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermataSpawner : MonoBehaviour
{
    public GameObject Fermata;
    public GameObject Pedone;
    public float SpawnTime;
    public float[] xPositions;
    public BackgroundScroller backgroundScroller;
    public GameManager GameManagerObj;
    public GameObject Bus;

    
    void Start()
    {
        xPositions = new float[] { -3.5f, 3.5f };
        StartCoroutine(SpawnFermate());
    }

    void Fermate()
    {
        int pedoniCounter = Random.Range(0, 5);
        float xPosition = xPositions[Random.Range(0, 2)];
        for (float i = 0; i < pedoniCounter; i++)
        {
            GameObject pedoneObj = Instantiate(Pedone, Fermata.GetComponent<Transform>());
            pedoneObj.GetComponent<Transform>().position = new Vector3(xPosition, (transform.position.y - 1 - ((i+1)/2)), 0f);
            pedoneObj.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 1f);
            pedoneObj.GetComponent<PedoneMovement>().Bus = Bus;
            pedoneObj.GetComponent<PedoneMovement>().Speed = backgroundScroller.Speed * 20;
        }
        GameObject fermataObj = Instantiate(Fermata, new Vector2(xPosition, transform.position.y), Quaternion.Euler(0, 0, 0));
        fermataObj.GetComponent<FermataMovement>().Bus = Bus;
        fermataObj.GetComponent<FermataMovement>().Speed = backgroundScroller.Speed * 20;
    }

    IEnumerator SpawnFermate()
    {
        float time = (float)(SpawnTime / (Mathf.Sqrt(GameManagerObj.Difficulty * 0.5f)));
        while (true)
        {
            yield return new WaitForSeconds(time);
            Fermate();
        }
    }
}
