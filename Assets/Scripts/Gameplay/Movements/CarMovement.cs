using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public new Transform transform;
    public float Speed;
    public float YBound = -11f;
    private bool SameDirection;
    public GameManager GameManagerObj;

    void Start()
    {
        transform.GetComponent<Transform>();
        SameDirection = transform.position.x >= 0;
    }

    void Update()
    {
        transform.position -= new Vector3(0, Speed * Time.deltaTime, 0);

        if(transform.position.y <= YBound)
        {
            Destroy(gameObject);
        }
    }
}
