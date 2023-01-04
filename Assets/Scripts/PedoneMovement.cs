using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedoneMovement : MonoBehaviour
{
    public Transform transform;
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
       transform.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
    
        if(transform.position.y <= -6){
            Destroy(gameObject);
        }
    }
}
