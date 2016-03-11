﻿using UnityEngine;
using System.Collections;

public class PlayerHolderController : MonoBehaviour
{
	[HideInInspector]	
	public bool hit;

	private float mySpeed;
	public Animator myAnim;
	private Vector3 force;

	void Awake ()
	{
		//myAnim = GetComponentInChildren<Animator> ();
	}

	void Start ()
	{
		mySpeed = 5f;
	}

	void Update ()
	{
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.62f, 2.62f), Mathf.Clamp (transform.position.y, -4.5f, 4.5f), 0);


		if (!hit) {
			GetComponent<Rigidbody2D> ().velocity = transform.up * mySpeed;

		} else {
			//gameObject.GetComponent<Rigidbody2D> ().AddForce (force * 3000 * Time.deltaTime);
		}
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (this.gameObject.layer == 8 && other.gameObject.layer == 11) { // this = player1, other= player2
			Debug.Log ("Player 1 gets punched");
			//hit = true;
			//StartCoroutine (MakeHitFalse ());
			//force = transform.position - other.transform.position;
			//	force.Normalize ();
			
			myAnim.Play ("player_hit");
			//transform.Rotate (0, 0, 15);
			//other.GetComponentInParent<Rigidbody2D> ().AddForce (force * 1000);
			//other.GetComponentInParent<Rigidbody2D> ().transform.Rotate (0, 0, 15);
		} 
		if (this.gameObject.layer == 10 && other.gameObject.layer == 9) {
			Debug.Log ("Player 2 gets punched");
			//hit = true;
			//StartCoroutine (MakeHitFalse ());
			//force = transform.position - other.transform.position;
			//force.Normalize ();

			//myAnim.Play ("player_hit");
			//transform.Rotate (0, 0, 15);
			//other.GetComponentInParent<Rigidbody2D> ().AddForce (force * 1000);
			//other.GetComponentInParent<Rigidbody2D> ().transform.Rotate (0, 0, 15);
		}
	}

	public IEnumerator MakeHitFalse ()
	{
		yield return new WaitForSeconds (.3f);
		myAnim.Play ("player_idle");
		hit = false;
	}
}
