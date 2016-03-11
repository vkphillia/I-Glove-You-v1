using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHolderController : MonoBehaviour
{
	[HideInInspector]	
	public bool hit;
	public bool gloved;
	public Animator myAnim;
	public SpriteRenderer HitEffectSprite;
	public Text myHealthText;

	private float mySpeed;
	private Vector3 force;
	private int myHealth;


	
	void Start ()
	{
		mySpeed = 5f;
		myHealth = 20;
		myHealthText.text = " Health: " + myHealth;
	}

	void Update ()
	{
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.62f, 2.62f), Mathf.Clamp (transform.position.y, -4.5f, 4.5f), 0);
		GetComponent<Rigidbody2D> ().velocity = transform.up * mySpeed;		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		
		if (this.gameObject.layer == 8 && other.gameObject.layer == 11)
        {   // this = player1, other= player2
			Debug.Log ("Player 1 gets punched");
			myHealth--;
			myHealthText.text = " Health: " + myHealth;
			hit = true;
			StartCoroutine (MakeHitFalse ());
			force = transform.position - other.transform.position;
			force.Normalize ();
			gameObject.GetComponent<Rigidbody2D> ().AddForce (force * 1000);
			HitEffectSprite.gameObject.transform.position = this.transform.position;
			HitEffectSprite.enabled = true;
			myAnim.Play ("player_hit");
			//transform.Rotate (0, 0, 15);
			other.GetComponentInParent<Rigidbody2D> ().AddForce (-force * 1000);
			other.GetComponentInParent<Rigidbody2D> ().transform.Rotate (0, 0, 15);
		} 
		if (this.gameObject.layer == 10 && other.gameObject.layer == 9)
        {
			Debug.Log ("Player 2 gets punched");
			myHealth--;
			myHealthText.text = " Health: " + myHealth;
			hit = true;
			StartCoroutine (MakeHitFalse ());
			force = transform.position - other.transform.position;
			force.Normalize ();
			gameObject.GetComponent<Rigidbody2D> ().AddForce (force * 1000);
			HitEffectSprite.gameObject.transform.position = this.transform.position;
			HitEffectSprite.enabled = true;
			myAnim.Play ("player_hit");
			//transform.Rotate (0, 0, 15);
			other.GetComponentInParent<Rigidbody2D> ().AddForce (-force * 1000);
			other.GetComponentInParent<Rigidbody2D> ().transform.Rotate (0, 0, 15);
		}	
	}

	public IEnumerator MakeHitFalse ()
	{
		yield return new WaitForSeconds (.3f);
		HitEffectSprite.enabled = false;
		myAnim.Play ("player_idle");
		hit = false;
	}
}
