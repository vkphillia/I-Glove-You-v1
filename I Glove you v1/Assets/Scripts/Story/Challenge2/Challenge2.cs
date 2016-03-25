using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Challenge2 : Challenge
{
    public PlayerControlsUniversal player;
    public EnemyHolder enemyHolder;

    public GameObject UI;
    public Text filler;
    public Text enemyKilled;

    private int enemyCount;

    void Start()
    {
        enemyKilled.text = "Hit received: 0";

        //all these things are not temporary now
        StartCoroutine(StartRound());
    }

    IEnumerator StartRound()
    {
        filler.text = "Challenge 2";
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
        enemyHolder.Spawn(1);
        Challenge.noOfEnemyAlive++;//increaing no of enemy available in scene

    }
    
    void Update()
    {
        // 
        //timerStarted is set to false by GameTimer when time reaches 0
        if (player.health == 0 && GameTimer.Instance.timerStarted)
        {
            GameTimer.Instance.timerStarted=false;
        }
        else if(player.move == true && !GameTimer.Instance.timerStarted)
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
