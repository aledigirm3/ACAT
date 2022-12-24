using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strada : MonoBehaviour
{
    Vector2 posIniziale;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        posIniziale = transform.position;
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= -2.2f)
        transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        else
        transform.position = posIniziale;
    }
}
