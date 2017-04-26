using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControler : MonoBehaviour {

    public AudioClip sound;
    
	void Start () {
		
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource player = GameObject.Find("Player").GetComponent<AudioSource>();
        player.clip = sound;
        player.Play();

        GameManager.AddPoints();
        Destroy(gameObject);
    }
}
