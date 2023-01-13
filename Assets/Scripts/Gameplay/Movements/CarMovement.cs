using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public new Transform transform;
    public float SpeedOppositeDirection;
    public float SpeedSameDirection;
    public float YBound = -11f;
    private bool SameDirection;

    void Start()
    {
        transform.GetComponent<Transform>();
        SameDirection = transform.position.x >= 0;
        SpeedOppositeDirection = 6f;
        SpeedSameDirection = 1.5f;
    }

    void Update()
    {

        if(SameDirection)
        {
            transform.position -= new Vector3(0, SpeedSameDirection * Time.deltaTime, 0);
        }
        else
        {
            transform.position -= new Vector3(0, SpeedOppositeDirection * Time.deltaTime, 0);
        }

        if(transform.position.y <= YBound)
        {
            Destroy(gameObject);
        }
    }
}
