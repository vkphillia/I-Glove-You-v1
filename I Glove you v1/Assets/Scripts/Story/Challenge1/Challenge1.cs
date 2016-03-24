﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//scripts used for this scene
//Challenge1,GameTimer,EnemyHolder,Enemy,PlayerControlsUniversal

//scripts that can be reused
//GameTimer,EnemyHolder,Enemy,PlayerControlsUniversal

//how this scene runs, order wise
//Challenge1--> Start() --> StartRound(): sets timerStarted, player.move

//in GameTimer script when timerStarted is set true then timer runs, it also sets the timer text
//in PlayerControlsUniversal when move is true then player starts moving
//at this point the round is started

//now during the round what happens in background is this
//Challenge1--> Update() keeps checking for noOfEnemyAlive and timerStarted
//case 1:
//as soon as the noOfEnemyAlive gets 0 it calls EnemyHolder.Spawn()
//EnemyHolder.Spawn() takes 1 argument which is the number of enemy to be spawned and then it instantiate an enemy prefab with random sprite and location
//case 2:
//as soon as the timerStarted is set false it calls StopRound()
//StopRound() sets player.move to false, resets the noOfEnemyAlive and shows the UI

//thats all
//I think this wil give you an idea of the data flow here
// I use rightClick on a function to find all its reference and definations in visual studio, helps in understanding the flow, I dont know if you use the same



public class Challenge1 : Challenge
{
    public PlayerControlsUniversal player;
    public EnemyHolder enemyHolder;

    public GameObject UI;
    public Text filler;
    public Text enemyKilled;

    
    private int enemyCount;

    void Start()
    {
        enemyKilled.text = "Enemy killed: 0";

        //all these things are not temporary now
        StartCoroutine(StartRound());
    }

    IEnumerator StartRound()
    {
        filler.text = "Challenge 1";
        yield return new WaitForSeconds(1f);
        filler.text = "3";
        yield return new WaitForSeconds(0.5f);
        filler.text = "2";
        yield return new WaitForSeconds(0.5f);
        filler.text = "1";
        yield return new WaitForSeconds(0.5f);
        filler.text = "";

        player.move = true;//enables player movement

        GameTimer.Instance.timerStarted = true;//starts timer
    }

    void Update()
    {
        // noOfEnemyAlive is set to 0 by Enemy scipt when player triggers enemy
        //timerStarted is set to false by GameTimer when time reaches 0
        if(Challenge.noOfEnemyAlive==0 && GameTimer.Instance.timerStarted)
        {
            enemyKilled.text = "Enemy killed: " + enemyCount;

            enemyHolder.Spawn(1);
            Challenge.noOfEnemyAlive++;//increaing no of enemy available in scene
            enemyCount++;//keeping count of enemy spawned in this scene yet
        }

        //if timer has stopped and player is still moving then call for the round stop
        else if(player.move && !GameTimer.Instance.timerStarted)
        {
            StartCoroutine(StopRound());
        }

    }
    
    //temporay codes just to give an idea
    IEnumerator StopRound()
    {
        player.move = false;
        Challenge.noOfEnemyAlive = 0;//reseting

        filler.text = "You killed " + enemyCount + " enemy";
        UI.SetActive(true);//setting challenge complete buttons to active
        yield return new WaitForSeconds(1f);
        
    }
    
    public void NextChallenge()
    {
        SceneManager.LoadScene("2nd challenge");
    }

    public void BackToTowerMenu()
    {
        SceneManager.LoadScene("story main");
    }

    //public override void SpawnEnemy ()
    //{
    //	base.SpawnEnemy ();
    //	//StoryManager.Instance.myEnemy.GetComponent<SpriteRenderer> ().sprite = myEnemySprite;

    //}

 //   public override void CheckForObjectiveComplete ()
	//{
	//	//Timer controller
	//	roundTimer -= Time.deltaTime;
	//	StoryManager.Instance.story.text = "Time: " + roundTimer.ToString ("N0");

	//	if (roundTimer <= 0)
	//	{
	//		//Times up and round is over
	//		CheckRoundStatus ();
	//	}
	//}
}
