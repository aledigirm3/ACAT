using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinSpawner : MonoBehaviour
{
    public GameObject coin;
    public float spawnTime = 3;
    public float[] xPositions = new float[] {-2.46f, -1.55f, -0.6f, 0.6f, 1.52f, 2.44f};
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoin());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Coin(){
        int randPosition = Random.Range(0, xPositions.Length);
        Instantiate(coin,new Vector2(xPositions[randPosition], transform.position.y),Quaternion.identity);
    }

    IEnumerator SpawnCoin(){
        while(true){
            yield return new WaitForSeconds(spawnTime);
            Coin();
        }
    }
}
