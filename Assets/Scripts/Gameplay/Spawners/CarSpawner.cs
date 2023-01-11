using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] car;
    public float spawnTime = 3;
    public float[] xPositions = new float[] {-2.76f, -1.67f, -0.59f, 0.6f, 1.7f, 2.78f};

    void Start()
    {
        StartCoroutine(SpawnCars());
    }

    void Cars(){
        int randCar = Random.Range(0,car.Length);
        int randPosition = Random.Range(0, xPositions.Length);
        float xPosition = xPositions[randPosition];
        Instantiate(car[randCar], new Vector2(xPosition, transform.position.y), Quaternion.Euler(0,0, (xPosition > 0 ? 90 : -90)));
    }

    IEnumerator SpawnCars(){
        while(true){
            yield return new WaitForSeconds(spawnTime);
            Cars();
        }
    }
}
