using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public Sprite[] mySprites;
	private float mySpeed;

	void Start ()
	{
		mySpeed = 4f;
	}

	void Update ()
	{
		transform.position += transform.up * Time.deltaTime * mySpeed;
	}
}
