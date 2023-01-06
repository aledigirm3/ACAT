using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSpawner : MonoBehaviour
{
    public GameObject[] car;
    public float spawnTime = 3;
    public float[] xPositions = new float[] {-2.76f, -1.67f, -0.59f, 0.6f, 1.7f, 2.78f};
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCars());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Cars(){
        int randCar = Random.Range(0,car.Length);
        int randPosition = Random.Range(0, xPositions.Length);
        Instantiate(car[randCar],new Vector2(xPositions[randPosition], transform.position.y),Quaternion.Euler(0,0,90));
    }

    IEnumerator SpawnCars(){
        while(true){
            yield return new WaitForSeconds(spawnTime);
            Cars();
        }
    }
}
