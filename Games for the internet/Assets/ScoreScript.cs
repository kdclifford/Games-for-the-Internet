using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public int startingScore;
    private int currentScore;
    private float scoreTimer;
    public GameObject score;




    // Start is called before the first frame update
    void Start()
    {
        currentScore = startingScore;
    }

    // Update is called once per frame
    void Update()
    {
        scoreTimer += Time.deltaTime;

        if(scoreTimer > 1)
        {
            currentScore--;
            scoreTimer = 0f;
        }

        GetComponent<Text>().text = "Score:" + currentScore;

    }

    public void AddScore(int amount)
    {
        currentScore += amount;
    }
}
