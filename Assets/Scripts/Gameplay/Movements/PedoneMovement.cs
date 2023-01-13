using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedoneMovement : MonoBehaviour
{
    public new Transform transform;
    public float Speed;
    public GameManager GameManagerObj;
    public GameObject Bus;

    void Start()
    {
        transform.GetComponent<Transform>();
    }

    void Update()
    {
        print(Vector3.Distance(Bus.GetComponent<Transform>().position, transform.position));
        if (Vector3.Distance(Bus.GetComponent<Transform>().position, transform.position) <= 1.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Bus.GetComponent<Transform>().position, Speed * Time.deltaTime);
        }
        else
            transform.position -= new Vector3(0, Speed * Time.deltaTime, 0);

        if (Vector3.Distance(Bus.GetComponent<Transform>().position, transform.position) <= 0.3f)
        {
            Destroy(transform.root.gameObject);
        }
    }
}
