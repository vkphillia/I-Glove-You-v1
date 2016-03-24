using UnityEngine;
using System.Collections;

public class Challenge1 : Challenge
{

	public override void SpawnEnemy ()
	{
		base.SpawnEnemy ();
		StoryManager.Instance.myEnemy.GetComponent<SpriteRenderer> ().sprite = myEnemySprite;
	
	}
}
