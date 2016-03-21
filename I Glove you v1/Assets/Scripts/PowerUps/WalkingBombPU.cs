using UnityEngine;
using System.Collections;

public class WalkingBombPU : MonoBehaviour
{
	public int weight;
	public int damageByBlast;
	public float myTime;
	public GameObject myBlastCol;
	public float mySpeed;

	private Vector3 EnemyPos;
	private Vector3 myPos;
	private Vector3 relativePos;
	private float angle;
	private bool active;




	void Update ()
	{
		Debug.Log ("angle " + angle);
		Debug.Log ("myAngle " + transform.rotation);
		if (active)
		{
			//find other player and go towards it
			if (OfflineManager.Instance.currentState == GameState.Playing)
			{
				AIFollow ();
				transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.75f, 2.75f), Mathf.Clamp (transform.position.y, -3.7f, 3.7f), 0);
				transform.position += transform.up * Time.deltaTime * mySpeed;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (!active)
		{
			if (other.gameObject.layer == 8 && !OfflineManager.Instance.PlayerHolder1.hasGlove)
			{
				active = true;
				Debug.Log ("Player1HitBomb");
				StartCoroutine (ActivateBomb (OfflineManager.Instance.PlayerHolder1));
			}
			else if (other.gameObject.layer == 10 && !OfflineManager.Instance.PlayerHolder2.hasGlove)
			{
				active = true;
				Debug.Log ("Player2HitBomb");
				StartCoroutine (ActivateBomb (OfflineManager.Instance.PlayerHolder2));
			}
			else if (other.gameObject.layer == 9)
			{
				Debug.Log ("GloveHitBomb");
				OfflineManager.Instance.PlayerHolder1.Punch ();
				DeactivatePU ();
			}
			else if (other.gameObject.layer == 11)
			{
				Debug.Log ("GloveHitBomb");
				OfflineManager.Instance.PlayerHolder2.Punch ();
				DeactivatePU ();
			}

		}
	}

	public IEnumerator ActivateBomb (PlayerHolderController p)
	{
		myBlastCol.GetComponent<CircleCollider2D> ().enabled = true;
		myBlastCol.GetComponent<CircleCollider2D> ().enabled = true;
		//deactivate its collider to ensure it doesn't get picked again
		GetComponent<CircleCollider2D> ().enabled = false;

		yield return new WaitForSeconds (myTime);
		//blast
		StartCoroutine (BlastNow (p));
	}

	public IEnumerator BlastNow (PlayerHolderController p)
	{
		active = false;
		GetComponent<SpriteRenderer> ().enabled = false;
		myBlastCol.GetComponent<SpriteRenderer> ().enabled = true;
		p.getPunched (this.transform);
		p.AlterHealth (damageByBlast);
		yield return new WaitForSeconds (1f);
		DeactivatePU ();

	}

	void DeactivatePU ()
	{
		transform.rotation = Quaternion.Euler (0, 0, 0);
		active = false;
		//disable sprite and blast collider before disabling gameobject
		myBlastCol.GetComponent<SpriteRenderer> ().enabled = false;
		myBlastCol.GetComponent<CircleCollider2D> ().enabled = false;
		//activate its collider and sprite renderer
		GetComponent<CircleCollider2D> ().enabled = true;
		GetComponent<SpriteRenderer> ().enabled = true;
		OfflineManager.Instance.PUPicked = true;
		gameObject.SetActive (false);
	}

	void AIFollow ()
	{
		//Find playr with GLove and follow
		if (OfflineManager.Instance.PlayerHolder1.hasGlove)
		{
			EnemyPos = Camera.main.WorldToScreenPoint (OfflineManager.Instance.PlayerHolder1.transform.position);
		}
		else if (OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
			EnemyPos = Camera.main.WorldToScreenPoint (OfflineManager.Instance.PlayerHolder2.transform.position);
		}
	
		myPos = Camera.main.WorldToScreenPoint (this.transform.position);
		relativePos = EnemyPos - myPos;

		//rotate towards player with glove
		angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
		this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, (angle - 90)));


	}



}
