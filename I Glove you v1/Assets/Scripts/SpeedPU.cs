using UnityEngine;
using System.Collections;

public class SpeedPU : MonoBehaviour
{

	public float myTime;
	public float SpeedUp;

	
	void OnCollisionEnter2D (Collision2D other)
	{
        if (other.gameObject.layer == 8 && !OfflineManager.Instance.PlayerHolder1.hasGlove)
        {
            StartCoroutine(SpeedBoost(OfflineManager.Instance.PlayerHolder1));
        }

        else if (other.gameObject.layer == 10 && !OfflineManager.Instance.PlayerHolder2.hasGlove)
        {
            StartCoroutine(SpeedBoost(OfflineManager.Instance.PlayerHolder2));
        }

        else //we cannot deactivate gameObject in all case as coroutine will not function properly
        {
            OfflineManager.Instance.PUPicked = true;
            gameObject.SetActive(false);
        }
	}

	IEnumerator SpeedBoost (PlayerHolderController p)
	{
        //cannot deactivate gameobject as it will kill this coroutine so we disabled sprite and collider
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        p.mySpeed += SpeedUp;
		yield return new WaitForSeconds (myTime);
		p.mySpeed = OfflineManager.Instance.MaxSpeed;

        //re-enabling th sprite and collider before deactivating gameObject
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        OfflineManager.Instance.PUPicked = true;
        gameObject.SetActive(false);
    }


}
