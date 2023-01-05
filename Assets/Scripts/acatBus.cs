using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class acatBus : MonoBehaviour
{
    Rigidbody2D rb;
    public bool moveLeft;
    public bool moveRight;
    public float horizontalMove;
    public float speed = 5;
    public float rotationSpeed = 5;
    public int coins = 0;
    public Text coinsText;
    public int pedoni = 0;
    public Text pedoniText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveLeft = false;
        moveRight = false;
    }
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

    // Update is called once per frame
    void Update()
    {
        Move();
        coinsText.text = coins.ToString();
        pedoniText.text = pedoni.ToString();
    }

    private void Move(){
        if(moveLeft && rb.position.x >= -4.55f){
            rb.rotation = 3;
            horizontalMove = -speed;
        }
        else if(moveRight && rb.position.x <= 4.55f){
            rb.rotation = -3;
            horizontalMove = speed;
        }
        else{
            rb.rotation = 0;
            horizontalMove = 0;
        }
    }

    private void FixedUpdate(){
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Car"){
            Time.timeScale = 0;
        }
         else if(collision.gameObject.tag == "Border"){
            PointerUpLeft();
            PointerUpRight();
        }
        else if(collision.gameObject.tag == "Coin"){
            coins += 1;
            Destroy(collision.gameObject);
        }
         else if(collision.gameObject.tag == "Pedone"){
            pedoni += 1;
            Destroy(collision.gameObject);
        }
    }
}
