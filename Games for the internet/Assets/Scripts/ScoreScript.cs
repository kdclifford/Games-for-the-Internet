using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public int startingScore;
    private int currentScore;
    private float scoreTimer;
    public GameObject time;
    private int unitSecondsTimer = 0;
    private int tensSecondsTimer = 0;
    private int minsTimer = 0;
    public GameObject player;

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
            unitSecondsTimer++;
            if(unitSecondsTimer == 10)
            {
                unitSecondsTimer = 0;
                tensSecondsTimer++;
            }

            if (tensSecondsTimer == 6)
            {
                tensSecondsTimer = 0;
                minsTimer++;
            }

            time.GetComponent<Text>().text = "Time:" + minsTimer + ":" + tensSecondsTimer + "" + unitSecondsTimer;
            currentScore--;
            scoreTimer = 0f;
        }

        GetComponent<Text>().text = "Score:" + currentScore;

        if(currentScore <= 0)
        {
            player.GetComponent<Movement>().DieAnimation();
            GameOver();
        }

    }

    public void AddScore(int amount)
    {
        currentScore += amount;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
