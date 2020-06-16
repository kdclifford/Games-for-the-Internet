using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject mantis;
    public GameObject maggot;
    public GameObject beetle;
    public GameObject MoveableBlock;
    public GameObject BreakableBlock;

    public GameObject player;
    private Movement playerMove;

    private Text tutText;

    
    private float timer = 0;

    private bool maggotCheck = false;
    private bool mantistCheck = false;
    private bool beetleCheck = false;
    private bool blockCheck = false;

    private bool step1 = false;
    private bool step2 = false;
    private bool step3 = false;
    private bool step4 = false;
    private bool step5 = false;
    private bool step6 = false;
    private bool step7 = false;
    private bool step8 = false;
    private bool step9 = false;
    private bool step10 = false;
    private bool step11 = false;
    private bool step12 = false;
    private bool step13 = false;
    private bool step14 = false;

    private ScoreScript score;
    public GameObject scoreObject;
    // Start is called before the first frame update
    void Start()
    {
        score = scoreObject.GetComponent<ScoreScript>();
        tutText = GetComponent<Text>();
        playerMove = player.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!step11)
        {
            timer += Time.deltaTime;
        }

        if (maggotCheck)
        {
            if (maggot != null)
            {
                
            }
            else
            {
                step2 = true;
                maggotCheck = false;
            }
        }

        if (mantistCheck)
        {
            if (mantis != null)
            {

            }
            else
            {
                step4 = false;
                step5 = true;
                mantistCheck = false;
            }
        }


        if (beetleCheck)
        {
            if (beetle != null)
            {

            }
            else
            {
                step7 = false;
                step8 = true;
                beetleCheck = false;
            }
        }

        if (blockCheck)
        {
            if (BreakableBlock != null)
            {

            }
            else
            {
                step10 = false;
                step11 = true;
                blockCheck = false;
            }
        }

        if (!step1 && timer > 10)
        {
            tutText.text = "Kill the Maggot!";
            maggot.SetActive(true);
            maggotCheck = true;
            step1 = true;
            timer = 0;
        }
       else if(step2)
        {
            tutText.text = "Pick up Maggot power up";
            if(playerMove.shoot)
            {
                step2 = false;
                step3 = true;
            }
        }
        else if (step3)
        {
            tutText.text = "Press J to fire an acid shot at enemies";
            if (Input.GetKey(KeyCode.J))
            {
                step3 = false;
                step4 = true;
            }
        }
       else if (step4)
        {
            tutText.text = "Kill the Mantis!";
            mantis.SetActive(true);
            mantistCheck = true;    
            
        }
        else if (step5)
        {
            tutText.text = "Pick up Mantis power up";
            if (playerMove.block)
            {
                step5 = false;
                step6 = true;
                timer = 0;
            }
        }
        else if (step6)
        {
            tutText.text = "You can now move green blocks";
            MoveableBlock.SetActive(true);
            if(timer > 10)
            {
                step6 = false;
                step7 = true;
            }           
        }
        else if (step7)
        {
            tutText.text = "Kill the Beetle!";
            beetle.SetActive(true);
            beetleCheck = true;            
        }
        else if(step8)
        {
            tutText.text = "Pick up Beetle power up";
            if (playerMove.wings)
            {
                step8 = false;
                step9 = true;
                timer = 0;
            }
        }
        else if (step9)
        {
            tutText.text = "You can now jump higher";
          if(timer > 10)
            {
                step9 = false;
                step10 = true;
                BreakableBlock.SetActive(true);
            }
        }
        else if (step10)
        {
            tutText.text = "Attack and Break the orange block";
            blockCheck = true;
            timer = 10;
        }
        else if (step11)
        {

            tutText.text = "Taking dame will lower your score, time will also lower your score. 0 score = Game Over";
                score.currentScore = 10;
            step11 = false;
            //    timer -= Time.deltaTime;

            //if (timer < 0)
            //{
            //    SceneManager.LoadScene("MainMenu");
            //}
        }
    }
}
