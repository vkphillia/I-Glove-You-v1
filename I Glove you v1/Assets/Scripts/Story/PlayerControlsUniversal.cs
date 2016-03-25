﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControlsUniversal : MonoBehaviour
{
	public float mySpeed;
    public float health;
    public float maxHealth;

    public Text myHealthText_HUD;

    [HideInInspector]
	public bool move;

    [HideInInspector]
    public bool hasGlove;

    void Start()
    {
        myHealthText_HUD.text = "Health: " + health;
    }

    //this is temporary code, to be removed when new assets are added
    public void Initialize ()
	{
		StartCoroutine (FadePlayer ());
	}

	IEnumerator FadePlayer ()
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

    //increase or decreases the health of the player based on the amount
    //this is called by glove, power ups
    public void AlterHealth(int amount)
    {
        Debug.Log("player hit");

        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0)
        {
            //code for checking who wins the round and stops the round
            //OfflineManager.Instance.CheckRoundStatus();
            
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
        myHealthText_HUD.text = "Health: "+health;
    }

    
    void Update ()
	{
		//Read This,
		//this code alone wont give u the hitting effect
		//you need to add code from PlayerHolderController Update functionm
		//where based on hit the movement transform vector and speed varies
        //Read
        //I am adding code piece by piece so it wont get complicated, I will add those codes too when needed
		//move player only when challengeControl tells u to
		if (move)
		{
            KeyboardControls();
            MobileControls();

            transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.75f, 2.75f), Mathf.Clamp (transform.position.y, -4.34f, 4.34f), 0);
			transform.position += transform.up * Time.deltaTime * mySpeed;
		}
	}


	void KeyboardControls ()
	{
        
		if (Input.GetButton ("movez"))
		{
			MoveClockWise ();
		}
		else if (Input.GetButton ("movex"))
		{
			MoveAntiClockWise ();
		}
	}

	void MobileControls ()
	{
		int count = Input.touchCount;
		for (int i = 0; i < count; i++)
		{
			Touch touch = Input.GetTouch (i);

			if (touch.position.x < Screen.width / 2 && touch.position.y < Screen.height / 2)
			{
				MoveClockWise ();
			}

			if (touch.position.x > Screen.width / 2 && touch.position.y < Screen.height / 2)
			{
				MoveAntiClockWise ();
			}
		}
	}


	void MoveClockWise ()
	{
		transform.Rotate (0, 0, 5);
	}

	void MoveAntiClockWise ()
	{
		transform.Rotate (0, 0, -5);
	}
}
