using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	

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