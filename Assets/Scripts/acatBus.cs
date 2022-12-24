using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class acatBus : MonoBehaviour
{
    Rigidbody2D rb;
    public bool moveLeft;
    public bool moveRight;
    public float horizontalMove;
    public float speed = 5;

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
    }

    private void Move(){
        if(moveLeft){
            horizontalMove = -speed;
        }
        else if(moveRight){
            horizontalMove = speed;
        }
        else{
            horizontalMove = 0;
        }
    }

    private void FixedUpdate(){
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }
}
