using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class acatBus : MonoBehaviour
{
    //MOVIMENTO
    private Rigidbody2D carRigidbody;
    public bool moveLeft;
    public bool moveRight;
    public float horizontalMove;
    public float speed = 5;
    public float rotation = 3;
    public float rotationSpeed = 5;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        carRigidbody = GetComponent<Rigidbody2D>();
        moveLeft = false;
        moveRight = false;
    }

    //===============================================================
    public void PointerUpLeft(){
        moveLeft = false;
    }

    public void PointerDownLeft(){
        moveLeft = true;
    }

    public void PointerUpRight(){
        moveRight = false;
    }

    public void PointerDownRight(){
        moveRight = true;
    }
    //===============================================================

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(moveLeft && carRigidbody.position.x >= -2.81f)
        {
            carRigidbody.rotation = rotation;
            horizontalMove = -speed;
        }
        else if(moveRight && carRigidbody.position.x <= 2.8f)
        {
            carRigidbody.rotation = -rotation;
            horizontalMove = speed;
        }
        else
        {
            carRigidbody.rotation = 0;
            horizontalMove = 0;
        }
    }

    private void FixedUpdate()
    {
        carRigidbody.velocity = new Vector2(horizontalMove, carRigidbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            gameManager.onGameOver();
        }
        else if (collision.gameObject.tag == "Border")
        {
            PointerUpLeft();
            PointerUpRight();
        }
        else if (collision.gameObject.tag == "Coin" || collision.gameObject.tag == "Pedone")
        {
            gameManager.FriendlyCollision(collision.gameObject);
        }
    }

}
