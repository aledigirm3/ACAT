using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermataSpawner : MonoBehaviour
{
    public GameObject fermata;
    public GameObject[] pedoni;
    public float spawnTime;
    public float[] xPositions;
    // Start is called before the first frame update
    void Start()
    {
        xPositions = new float[] { -3.5f, 3.5f };
        spawnTime = 5f;
        StartCoroutine(SpawnFermate());
    }

    void Fermate()
    {
        Instantiate(fermata, new Vector2(xPositions[Random.Range(0, 2)], transform.position.y), Quaternion.Euler(0, 0, 0));
    }

    IEnumerator SpawnFermate()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Fermate();
        }
    }
}
