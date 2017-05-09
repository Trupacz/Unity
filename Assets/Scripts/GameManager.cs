using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    static Text ec;
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
    static float volume = 0.5f;
    public Slider slider;
    static int EnemyCount;
    static int enemyKilled;

	void Start () {
        enemyKilled = 0;
        EnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        csn = currentSceneName;
        nsn = nextLevel;
        if (!firstload) {
            hpi = 3;
            firstload = true;
                }

        poi = 0;
        howMouchCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        ec = GameObject.FindGameObjectWithTag("ec").GetComponent<Text>();
        hp = GameObject.FindGameObjectWithTag("HP").GetComponent<Text>();
        points = GameObject.FindGameObjectWithTag("Points").GetComponent<Text>();
        points.text = "x " + poi;
        hp.text = "x " + hpi;
        ec.text = "x " + enemyKilled;
        slider.value = volume;
        GameObject.Find("Player").GetComponent<AudioSource>().volume = volume;



    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void AddPoints() {
        poi++;
        points.text = "x " + poi.ToString();
        if (poi == howMouchCoins && enemyKilled == EnemyCount) NextLevel();
    }

    public static void AddKilledEnemy() {
        enemyKilled++;
        ec.text = "x " + enemyKilled.ToString();
        if (poi == howMouchCoins && enemyKilled == EnemyCount) NextLevel();
    }

    public static void GotHit()
    {
        hpi--;
        if (hpi < 0) SceneManager.LoadScene("gamover");
        else hp.text = "x " + hpi.ToString();
       
    }

    public void ValueChangeCheck()
    {
        GameObject.Find("Player").GetComponent<AudioSource>().volume = volume = slider.value;
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
