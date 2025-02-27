using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public new Transform transform;
    public float Speed;
    public GameManager GameManagerObj;

    void Start()
    {
        transform.GetComponent<Transform>();
    }


    void Update()
    {
        transform.position -= new Vector3(0, Speed * Time.deltaTime, 0);
        if(transform.position.y <= -11){
            Destroy(gameObject);
        }
    }
}
