using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text scoreTex;
    public int scoreMoeda;
    public int scoreAllMoeda;
    public bool animCheckpoint = false;
    public GameObject gameOver;

    public int scoreEnergy;
    public GameObject energy;

    public GameObject collectAllApple;

    public static GameController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    void Update()
    {
        VerificaScoreCoins();
    }
    public void VerificaScoreCoins()
    {
        if(!(scoreMoeda != scoreAllMoeda))
        {
            animCheckpoint = true;
        }
    }
    public void NextLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void CollectAllCoinsOn()
    {
        collectAllApple.SetActive(true);
    }
    public void CollectAllCoinsOff()
    {
        collectAllApple.SetActive(false);
    }
    public void UpdateScoreTex()
    {
        scoreTex.text = scoreMoeda.ToString();   
    }
    public void UpdateEnergyTex()
    {
        energy.GetComponent<Image>().fillAmount += 0.6f;
        scoreEnergy += 3;
    }
    public void GameOverOn()
    {
        gameOver.SetActive(true);
    }
    public void RestartGame(string Namelevel)
    {
        SceneManager.LoadScene(Namelevel);
    }
}

