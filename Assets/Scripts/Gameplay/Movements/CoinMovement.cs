using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public new Transform transform;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
       transform.GetComponent<Transform>();
        speed = 4f;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
    
        if(transform.position.y <= -11){
            Destroy(gameObject);
        }
    }
}
