using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedoneMovement : MonoBehaviour
{
    public new Transform transform;
    public float speed = 2f;
    public GameManager GameManagerObj;

    void Start()
    {
        transform.GetComponent<Transform>();
        speed = 2f;
    }

    void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        if (transform.position.y <= -11){
            Destroy(gameObject);
        }
    }
}
