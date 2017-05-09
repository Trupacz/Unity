using System;
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
    Animator animator;
    float move;
    public Rigidbody2D bullet;
    public GameObject menu;
    bool isMenuActive;
    public AudioClip throwsound;




    private void Start()
    {
        isMenuActive = false;
        menu = GameObject.FindGameObjectWithTag("Menu");
        menu.SetActive(false);
        animator = GetComponent<Animator>();
        isFacingRight = true;
        spriteface = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
        
    {
        
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {

                if (!isFacingRight)
                {
                    isFacingRight = true;
                    spriteface.flipX = false;
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

        if (Input.GetKeyDown(KeyCode.RightControl)) ThrowKunai();
        if (Input.GetKeyDown(KeyCode.Escape)) MenuActivation();


        if (Input.GetKeyDown(KeyCode.Space) && !inAir)
        {
           
            
            StartCoroutine(Deley());
        }
    }

   

    void FixedUpdate () {
        
        if (rb2d.velocity.magnitude > maxSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
        }
        else {
            move = Input.GetAxis("Horizontal") * speed;
        }
            
        
        if (rb2d.velocity.y == 0) inAir = false;
        else inAir = true;
        animator.SetFloat("speed", Mathf.Abs(move) + 0.09f);
        animator.SetFloat("FSpeed", rb2d.velocity.y + 0.09f);
        animator.SetBool("inAir", inAir);

        Vector2 mm = new Vector2(move, 0);
        rb2d.AddForce(mm, ForceMode2D.Impulse);
        

            }

    private void ThrowKunai() {
        //print("Pew");
        animator.Play("Throwing");
        AudioSource player = GetComponent<AudioSource>();
        player.clip = throwsound;
        player.Play();
         
        
        if (isFacingRight)
        {
            Rigidbody2D bulletInstance = Instantiate(bullet, transform.position + new Vector3(1, 0, 0), Quaternion.Euler(new Vector3(0, 0, -90))) as Rigidbody2D;
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bulletInstance.AddForce(new Vector2(50, 0), ForceMode2D.Impulse);
        }
        else
        {
            Rigidbody2D bulletInstance = Instantiate(bullet, transform.position + new Vector3(-1, 0, 0), Quaternion.Euler(new Vector3(0, 0, 90))) as Rigidbody2D;
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bulletInstance.AddForce(new Vector2(-50, 0), ForceMode2D.Impulse);
        }
        
    }

    private void MenuActivation()
    {
        print("clik");
        if (isMenuActive)
        {
            
            menu.SetActive(false);
            
        }
        else
        {
            menu.SetActive(true);
        }
        isMenuActive = !isMenuActive;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            GameManager.GotHit();


        }
        



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
