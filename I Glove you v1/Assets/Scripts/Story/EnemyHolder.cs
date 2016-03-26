﻿using UnityEngine;
using System.Collections;

public class EnemyHolder : MonoBehaviour
{
	public GameObject enemy;
	public Sprite[] enemySprites;


	public void Spawn (int enemyHealth,bool aI,bool hasGlove)
	{
        GameObject enemyCopy = Instantiate(enemy);
        enemyCopy.transform.SetParent(transform);
        enemyCopy.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-4.3f, 3.5f));
        enemyCopy.GetComponent<SpriteRenderer>().sprite = enemySprites[Mathf.FloorToInt(Random.Range(0, enemySprites.Length))];
        enemyCopy.GetComponent<Enemy>().health = enemyHealth;
        enemyCopy.GetComponent<Enemy>().AIOn = aI;
        enemyCopy.GetComponent<Enemy>().hasGlove = hasGlove;

        enemyCopy.SetActive(true);
    }


}