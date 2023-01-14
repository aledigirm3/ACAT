using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermataMovement : MonoBehaviour
{
    public new Transform transform;
    public float Speed;

    public GameObject[] Pedoni;

    void Start()
    {
        transform.GetComponent<Transform>();
        if (transform.position.x >= 0)
            transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
    }

    public void SetupFermata(float stradaSpeed)
    {
        Speed = stradaSpeed * 20;
    }

    void Update()
    {
        transform.position -= new Vector3(0, Speed * Time.deltaTime, 0);
        if (transform.position.y <= -11)
        {
            Destroy(gameObject);
        }
    }
}
