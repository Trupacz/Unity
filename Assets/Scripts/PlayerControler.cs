using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {
    Animator state;
    SpriteRenderer spriteface;
    private Rigidbody2D rb2d; 
    private float speed = 1.5f;
    private bool inAir;
    float maxSpeed = 20.0f;
    bool isFacingRight;
    


    private void Start()
    {
        
        isFacingRight = true;
        spriteface = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (!isFacingRight) {
                isFacingRight = true;
                spriteface.flipX = false ;
                    }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isFacingRight)
            {
                isFacingRight = false;
                spriteface.flipX = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.Space) && !inAir)
        {
            inAir = true;
            StartCoroutine(Deley());
        }
    }
    void FixedUpdate () {

        if (rb2d.velocity.magnitude > maxSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
        }
        else {
        }
            float move = Input.GetAxis("Horizontal")*speed;

        Vector2 mm = new Vector2(move, 0);
        rb2d.AddForce(mm, ForceMode2D.Impulse);

            }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            GameManager.GotHit();


        }
        inAir = false;

        if (collision.transform.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;


        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {



        if (collision.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    IEnumerator Deley()
    {
        rb2d.gravityScale = 1.0f;
        
        rb2d.AddForce(new Vector3(0, 100, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);
        rb2d.gravityScale = 5.0f;
       
        
    }


}
