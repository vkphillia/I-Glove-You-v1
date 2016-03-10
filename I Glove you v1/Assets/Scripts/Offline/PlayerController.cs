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
		if (OfflineManager.Instance.FirstTouch) {
			transform.position += transform.up * Time.deltaTime * mySpeed;
		}
	}
}
