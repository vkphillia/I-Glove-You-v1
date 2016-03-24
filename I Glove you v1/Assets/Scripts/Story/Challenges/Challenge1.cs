using UnityEngine;
using System.Collections;

public class Challenge1 : Challenge
{

	public override void SpawnEnemy ()
	{
		base.SpawnEnemy ();
		StoryManager.Instance.myEnemy.GetComponent<SpriteRenderer> ().sprite = myEnemySprite;
	
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
