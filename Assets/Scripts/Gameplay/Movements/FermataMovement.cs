using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermataMovement : MonoBehaviour
{
    public new Transform transform;
    public float speed;

    void Start()
    {
        transform.GetComponent<Transform>();
        speed = 4f;
        if (transform.position.x >= 0)
            transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
    }

    void FixedUpdate()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        if (transform.position.y <= -11)
        {
            Destroy(gameObject);
        }
    }
}
