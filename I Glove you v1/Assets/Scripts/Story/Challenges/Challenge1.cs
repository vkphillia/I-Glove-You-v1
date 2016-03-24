﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Challenge1 : Challenge
{
    public PlayerControlsUniversal player;
    public Text filler;

    void Start()
    {
        //all these things are temporary
        
        StartCoroutine(stopRound());
    }
    
    //temporay codes just to give an idea
    IEnumerator stopRound()
    {
        filler.text = "Challenge 1";
        yield return new WaitForSeconds(1f);
        filler.text = "3";
        yield return new WaitForSeconds(0.5f);
        filler.text = "2";
        yield return new WaitForSeconds(0.5f);
        filler.text = "1";
        yield return new WaitForSeconds(0.5f);
        filler.text = "Keep moving";
        player.move = true;
        yield return new WaitForSeconds(8f);
        player.move = false;
        filler.text = "stop Moving\n Taking you to the another challenge";
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("2nd challenge");

    }

	public override void SpawnEnemy ()
	{
		base.SpawnEnemy ();
		//StoryManager.Instance.myEnemy.GetComponent<SpriteRenderer> ().sprite = myEnemySprite;
	
	}



	public override void CheckForObjectiveComplete ()
	{
		//Timer controller
		roundTimer -= Time.deltaTime;
		StoryManager.Instance.story.text = "Time: " + roundTimer.ToString ("N0");

		if (roundTimer <= 0)
		{
			//Times up and round is over
			CheckRoundStatus ();
		}
	}
}
