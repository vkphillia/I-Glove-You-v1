using UnityEngine;
using System.Collections;

public class EnemyHolder : MonoBehaviour
{
	public GameObject enemy;
	public Sprite[] enemySprites;


	public void Spawn (int noOfEnemy)
	{
		for (int i = 0; i < noOfEnemy; i++)
		{
			GameObject enemyCopy = Instantiate (enemy);
			enemyCopy.transform.SetParent (transform);
			enemyCopy.transform.position = new Vector3 (Random.Range (-2.5f, 2.5f), Random.Range (-4.3f, 3.5f));
			enemyCopy.GetComponent<SpriteRenderer> ().sprite = enemySprites [Mathf.FloorToInt (Random.Range (0, enemySprites.Length))];
			enemyCopy.SetActive (true);
			//Debug.Log("Spawn");
		}
       
	}


}
