using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bus : MonoBehaviour
{
    //MOVIMENTO
    private Rigidbody2D CarRigidbody;
    public bool IsMovingLeft;
    public bool IsMovingRight;
    public float HorizontalSpeed;
    public float Speed;
    public float Rotation;

    //GAMEMANAGER
    public GameManager GameManagerObj;
    public GameObject PerkManagerObj;


    void Start()
    {
        CarRigidbody = GetComponent<Rigidbody2D>();
        IsMovingLeft = false;
        IsMovingRight = false;
    }

    //===============================================================
    public void PointerUpLeft(){
        IsMovingLeft = false;
    }

    public void PointerDownLeft(){
        IsMovingLeft = true;
    }

    public void PointerUpRight(){
        IsMovingRight = false;
    }

    public void PointerDownRight(){
        IsMovingRight = true;
    }
    //===============================================================

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(IsMovingLeft && CarRigidbody.position.x >= -2.81f)
        {
            CarRigidbody.rotation = Rotation;
            HorizontalSpeed = -Speed;
        }
        else if(IsMovingRight && CarRigidbody.position.x <= 2.8f)
        {
            CarRigidbody.rotation = -Rotation;
            HorizontalSpeed = Speed;
        }
        else
        {
            CarRigidbody.rotation = 0;
            HorizontalSpeed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            if (!PerkManagerObj.GetComponent<GhostPerk>().IsActivated)
                GameManagerObj.OnGameOver();
        }
        else if (collision.gameObject.tag == "Border")
        {
            PointerUpLeft();
            PointerUpRight();
        }
        else if (collision.gameObject.tag == "Coin" || collision.gameObject.tag == "Pedone")
        {
            GameManagerObj.FriendlyCollision(collision.gameObject);
        }
    }

    private void FixedUpdate()
    {
        CarRigidbody.velocity = new Vector2(HorizontalSpeed, CarRigidbody.velocity.y);
    }

}
