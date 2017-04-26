using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public float firstjump;
    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        InvokeRepeating("Jumper", firstjump, 2.0f);

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Jumper()
    {
        rb2d.AddForce(new Vector3(0, 1000, 0), ForceMode2D.Force);
    }


}
