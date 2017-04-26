using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    static Text hp;
    static Text points;
    static int poi;
    static int hpi;
    static bool firstload;
    public string currentSceneName;
    public string nextLevel;
    static string nsn;
    static string csn;
    static int howMouchCoins;

	void Start () {
        csn = currentSceneName;
        nsn = nextLevel;
        if (!firstload) {
            hpi = 3;
            firstload = true;
                }

        poi = 0;
        howMouchCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        
        hp = GameObject.FindGameObjectWithTag("HP").GetComponent<Text>();
        points = GameObject.FindGameObjectWithTag("Points").GetComponent<Text>();
        points.text = "x " + poi;
        hp.text = "x " + hpi;


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void AddPoints() {
        poi++;
        points.text = "x " + poi.ToString();
        if (poi == howMouchCoins) NextLevel();
    }

    public static void GotHit()
    {
        hpi--;
        if (hpi < 0) SceneManager.LoadScene("gamover");
        else hp.text = "x " + hpi.ToString();
       
    }

    public void Exit() {
        Application.Quit();
    }
    public void NewGame() {
        firstload = false;
        SceneManager.LoadScene("level1");
    }
    private static void NextLevel() {
        SceneManager.LoadScene(nsn);
    }
    public static void Restart() {
        
       SceneManager.LoadScene(csn);

    }
}
