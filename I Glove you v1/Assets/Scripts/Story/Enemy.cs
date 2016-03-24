using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int health;

    void Start()
    {
        health = 1;//default value, can be changed as required
    }

    //decreases the health if it is get hit by player, deactivates itself if the health reaches 0
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("triggered");

        if(other.gameObject.layer == 8)
        {
            health--;
            if (health == 0)
            {
                Challenge.noOfEnemyAlive--;
                gameObject.SetActive(false);
            }
        }
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

}