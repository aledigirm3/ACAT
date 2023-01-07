using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedoniSpawner : MonoBehaviour
{
    public GameObject[] pedoni;
    public float spawnTime = 5;
    public float[] xPositions = new float[] {-2.76f, -1.67f, -0.59f, 0.6f, 1.7f, 2.78f};
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPedoni());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Pedoni(){
        int randpedoni = Random.Range(0,pedoni.Length);
        int randPosition = Random.Range(0, xPositions.Length);
        Instantiate(pedoni[randpedoni],new Vector2(xPositions[randPosition], transform.position.y),Quaternion.Euler(0,0,0));
    }

    IEnumerator SpawnPedoni(){
        while(true){
            yield return new WaitForSeconds(spawnTime);
            Pedoni();
        }
    }
}
