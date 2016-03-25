using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	public int health;
	public float enemySpeed;

	//AI stuff
	//temporary variable, this should be linked to the challenges I guess
	public bool AIOn;
	public Transform myPlayer;
	//temporary variable, get reference from where ever PU is spawning
	public Transform PUOnScreen;

	private Vector3 EnemyPos;
	private Vector3 PlayerPos;
	//temporary variable, get reference from where ever PU is spawning
	private bool PUReady;
	private Vector3 PUPos;
	private Vector3 relativePos;
	private bool destReached;
	private Vector3 randPos;
	private float angle;

	private bool PUInRange;
	private bool hasGlove;

	void Start ()
	{
		health = 1;//default value, can be changed as required
		destReached = true;
	}

	//decreases the health if it is get hit by player, deactivates itself if the health reaches 0
	void OnTriggerEnter2D (Collider2D other)
	{
		//Debug.Log("triggered");

		if (other.gameObject.layer == 13)
		{
			health--;
			if (health == 0)
			{
				
			}
		}
	}

    //increase or decreases the health of the player based on the amount
    public void AlterHealth(int amount)
    {
        health += amount; 

        if (health > OfflineManager.Instance.MaxHealth)
        {
            health = OfflineManager.Instance.MaxHealth;
        }
        else if (health <= 0)
        {
            //code for checking who wins the round and stops the round
            //OfflineManager.Instance.CheckRoundStatus();
            Challenge.noOfEnemyAlive--;
            gameObject.SetActive(false);
        }
        else
        {
            //only play sound when adding health
            if (amount > 0)
            {
                SoundsController.Instance.PlaySoundFX("HealthUp", 1.0f);
                //StartCoroutine(ChangeColor(Color.green));
            }
            else
            {
                //StartCoroutine(ChangeColor(Color.red));
            }
        }
        //myHealthText_HUD.text = myHealth.ToString();
    }


    public void Initialize ()
	{
		StartCoroutine (FadeEnemy ());
	}

	IEnumerator FadeEnemy ()
	{
		Color tempColor = GetComponent<SpriteRenderer> ().color;
		tempColor.a = 0;
		GetComponent<SpriteRenderer> ().color = tempColor;
		yield return new WaitForSeconds (1f);
		while (tempColor.a <= 1)
		{
			GetComponent<SpriteRenderer> ().color = tempColor;
			tempColor.a += 0.1f;
			yield return new WaitForSeconds (.1f);
		}
	}

	void Update ()
	{
		

		if (AIOn)
		{
			//this makes it move
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.59f, 2.59f), Mathf.Clamp (transform.position.y, -4.5f, 3.8f), 0);
			transform.position += transform.up * Time.deltaTime * enemySpeed;

			if (hasGlove)
			{
				AIFollowOpponent ();
			}
			else
			{
				AIAvoidOpponent ();
			}
		}
	}

	//Avoids opponent and tries finding power ups
	void AIAvoidOpponent ()
	{
		EnemyPos = Camera.main.WorldToScreenPoint (this.transform.position);

		//find PU
		if (PUReady)
		{
			if (destReached)
			{
				//Find PU and rotate towards that point
				destReached = false;//until it reaches the PU
				PUPos = Camera.main.WorldToScreenPoint (PUOnScreen.transform.position);
				relativePos = PUPos - EnemyPos;
				angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
				this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, (angle - 90) * Time.deltaTime * enemySpeed));
			}
			if (EnemyPos == PUPos)
			{
				destReached = true;//make this true if opponent is in range using a bigger trigger
			}

		}
		else
		{
			//Find random point away from player and rotate towards that point
			if (destReached)
			{
				destReached = false;//until it reaches the below new destination
				randPos = Camera.main.WorldToScreenPoint (new Vector3 (Random.Range (-2f, 2f), Random.Range (-3f, 3f), 0));
				Debug.Log (randPos);
				relativePos = randPos - EnemyPos;
				angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
				this.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, (angle - 90) * Time.deltaTime * enemySpeed));
				Debug.Log (angle);
			}
			if (EnemyPos == randPos)
			{
				destReached = true;//make this true if opponent is in range using a bigger trigger
				Debug.Log ("destReached");
			}
		}


	}

	//follow opponent and destroy all possible PU on the way
	//later we will add code to avoid the bombs and air strikes
	void AIFollowOpponent ()
	{
		EnemyPos = Camera.main.WorldToScreenPoint (this.transform.position);

		if (PUReady && PUInRange) //PUInRange will be determined by a larger Trigger area around the enemy
		{
			PUPos = Camera.main.WorldToScreenPoint (PUOnScreen.transform.position);
			relativePos = PUPos - EnemyPos;
		}
		else
		{
			PlayerPos = Camera.main.WorldToScreenPoint (myPlayer.transform.position);
			relativePos = PlayerPos - EnemyPos;
		}
		angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
		this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, (angle - 90)));
	}
}