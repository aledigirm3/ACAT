using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMovement : MonoBehaviour
{
    public Transform transform;
    public float speedSx = 5f;
    public float speedDx = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
       transform.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < 0){
            transform.eulerAngles = new Vector3(0, 0, -90);
            transform.position -= new Vector3(0, speedSx * Time.deltaTime, 0);
        }
        else{
        transform.position -= new Vector3(0, speedDx * Time.deltaTime, 0);
        }
        if(transform.position.y <= -11){
            Destroy(gameObject);
        }
    }
}
