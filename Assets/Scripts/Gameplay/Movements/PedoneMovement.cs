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

    public void SetupPedone(float stradaSpeed, float xPosition, float yPosition, GameObject bus)
    {
        Bus = bus;
        Speed = stradaSpeed * 20;
        transform.position = new Vector3(xPosition, yPosition, 0f);
        transform.localScale = new Vector3(0.1f, 0.1f, 1f);
    }

    void Update()
    {
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
