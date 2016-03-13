using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void RoundOverEvent ();
public delegate void TriggerEvent ();
public class PlayerHolderController : MonoBehaviour
{
	public static event RoundOverEvent OnRoundOver;
	public static event TriggerEvent OnTrigger;

	[HideInInspector]	
	public bool hit;
	[HideInInspector]
	public int myHealth;
	[HideInInspector]
	public int roundWins;
	public Animator myPunchAnim;
	public SpriteRenderer HitEffectSprite;
	public Text myHealthText;
	public GameObject myTrigger;

	private Vector3 myStartPos;
	private Quaternion myStartRot;
	private float mySpeed;
	private Vector3 force;

	
	private Rigidbody2D myRB;

	void Awake ()
	{
		myRB = GetComponent<Rigidbody2D> ();
		
	}

	void Start ()
	{
		myStartPos = transform.position;
		myStartRot = transform.rotation;

		myHealth = OfflineManager.Instance.MaxHealth;
		mySpeed = 5f;
		myHealthText.text = " Health: " + myHealth;
	}

	void Update ()
	{
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.62f, 2.62f), Mathf.Clamp (transform.position.y, -4.5f, 4.5f), 0);

		if (OfflineManager.Instance.currentState == GameState.Playing) {
			if (!hit) {
				transform.position += transform.up * Time.deltaTime * mySpeed;
				//myRB.velocity = transform.up * mySpeed;	
			} else {
				transform.position += transform.up * Time.deltaTime * mySpeed * 2;

			}	
			
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		
		if (this.gameObject.layer == 8 && other.gameObject.layer == 11) {   // this = player1, other= player2
			Debug.Log ("Player 1 gets punched");
			StartCoroutine (HitEffect (other.GetComponentInParent<Rigidbody2D> ()));
			
			
		} 
		if (this.gameObject.layer == 10 && other.gameObject.layer == 9) {
			Debug.Log ("Player 2 gets punched");
			StartCoroutine (HitEffect (other.GetComponentInParent<Rigidbody2D> ()));
			
		}	
	}

	IEnumerator PushPlayer ()
	{
		myRB.AddForce (force * 5, ForceMode2D.Impulse);
		yield return new WaitForSeconds (0.5f);
		myRB.velocity = Vector3.zero;
	}

	public IEnumerator MakeHitFalse ()
	{
		yield return new WaitForSeconds (.3f);
		HitEffectSprite.enabled = false;
		//myAnim.Play ("player_idle");
		hit = false;
	}

	public void ResetPlayer ()
	{
		transform.position = myStartPos;
		transform.rotation = myStartRot;
		hit = false;
		myHealth = OfflineManager.Instance.MaxHealth;
		//GetComponentInChildren<OfflinePlayerController> ().transform.rotation = Quaternion.Euler (0, 0, 0);
		myHealthText.text = " Health: " + myHealth;
		myTrigger.SetActive (false);
		HitEffectSprite.enabled = false;
		Debug.Log ("myStartPos: " + myStartPos.ToString ());
	}

	IEnumerator HitEffect (Rigidbody2D r)
	{
		if (OnTrigger != null) {
			OnTrigger ();
		}
		//StartCoroutine (PlayPunchAnim ());
		
		yield return new WaitForSeconds (0.05f);
		myHealth--;
		myHealthText.text = " Health: " + myHealth;
		if (myHealth <= 0) {
			
			r.GetComponent<PlayerHolderController> ().roundWins++;
			if (roundWins < 2) {
				if (OnRoundOver != null) {
					OnRoundOver ();
				}
			} else {
				OfflineManager.Instance.MenuPanel.SetActive (true);
			}
		}
		hit = true;
		StartCoroutine (MakeHitFalse ());
		HitEffectSprite.transform.SetParent (this.transform);
		HitEffectSprite.gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y, -5);
		HitEffectSprite.enabled = true;
		transform.rotation = r.transform.rotation; 
		//force = (transform.position - other.transform.position).normalized;
		//force.Normalize ();
		//StartCoroutine (PushPlayer ());
		//myRB.AddForce (force * 1000);
		//other.GetComponentInParent<Rigidbody2D> ().AddForce (-force * 1000);
		//other.GetComponentInParent<Rigidbody2D> ().transform.Rotate (0, 0, 15);

	}

	

}