using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Challenge2 : Challenge
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
        filler.text = "Challenge 2";
        yield return new WaitForSeconds(1f);
        filler.text = "3";
        yield return new WaitForSeconds(0.5f);
        filler.text = "2";
        yield return new WaitForSeconds(0.5f);
        filler.text = "1";
        yield return new WaitForSeconds(0.5f);
        filler.text = "Keep rolling";
        player.move = true;
        yield return new WaitForSeconds(7f);
        player.move = false;
        filler.text = "stop rolling\n Taking you to the another challenge";
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("3rd challenge");
    }


 //   public override void SpawnEnemy ()
	//{
	//	base.SpawnEnemy ();
	//	StoryManager.Instance.myEnemy.GetComponent<SpriteRenderer> ().sprite = myEnemySprite;
	
	//}



	//public override void CheckForObjectiveComplete ()
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
