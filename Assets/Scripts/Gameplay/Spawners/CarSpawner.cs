using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] car;
    public GameManager GameManagerObj;
    public float spawnTime = 3;
    public float[] xPositions = new float[] {-2.76f, -1.67f, -0.59f, 0.6f, 1.7f, 2.78f};
    public float SpeedOppositeDirection;
    public float SpeedSameDirection;

    void Start()
    {
        SpeedOppositeDirection = 3f;
        SpeedSameDirection = 1f;
        StartCoroutine(SpawnCars());
    }

    void Cars()
    {
        int randCar = Random.Range(0,car.Length);
        int randPosition = Random.Range(0, xPositions.Length);
        float xPosition = xPositions[randPosition];
        GameObject carObj = Instantiate(car[randCar], new Vector2(xPosition, transform.position.y), Quaternion.Euler(0,0, (xPosition > 0 ? 90 : -90)));
        if (xPosition > 0)
        {
            carObj.GetComponent<CarMovement>().Speed = SpeedSameDirection * ((float)Mathf.Sqrt(GameManagerObj.Difficulty));
        }
        else
            carObj.GetComponent<CarMovement>().Speed = SpeedOppositeDirection * ((float)Mathf.Sqrt(GameManagerObj.Difficulty/2));
    }

    IEnumerator SpawnCars()
    {
        while(true)
        {
            float time = (float)(spawnTime / (Mathf.Sqrt(GameManagerObj.Difficulty * 0.5f)));
            yield return new WaitForSeconds(time);
            Cars();
        }
    }
}
