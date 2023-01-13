using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermataSpawner : MonoBehaviour
{
    public GameObject fermata;
    public GameObject[] pedoni;
    public float spawnTime;
    public float[] xPositions;
    public BackgroundScroller backgroundScroller;
    public GameManager GameManagerObj;
    // Start is called before the first frame update
    void Start()
    {
        xPositions = new float[] { -3.5f, 3.5f };
        StartCoroutine(SpawnFermate());
    }

    void Fermate()
    {
        GameObject fermataObj = Instantiate(fermata, new Vector2(xPositions[Random.Range(0, 2)], transform.position.y), Quaternion.Euler(0, 0, 0));
        fermataObj.GetComponent<FermataMovement>().Speed = backgroundScroller.Speed * 20;
    }

    IEnumerator SpawnFermate()
    {
        float time = (float)(spawnTime / (Mathf.Sqrt(GameManagerObj.Difficulty * 0.5f)));
        while (true)
        {
            yield return new WaitForSeconds(time);
            Fermate();
        }
    }
}
