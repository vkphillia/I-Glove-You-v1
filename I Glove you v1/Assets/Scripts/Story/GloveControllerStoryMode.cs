using UnityEngine;
using System.Collections;

public class GloveControllerStoryMode : MonoBehaviour
{
    public int damageAmount;

    void Start()
    {
        damageAmount = -1;//default amount
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 && !other.gameObject.GetComponent <PlayerControlsUniversal>().hasGlove)
        {
            other.gameObject.GetComponent<PlayerControlsUniversal>().AlterHealth(damageAmount);
        }
        else if (other.gameObject.layer == 10 && !other.gameObject.GetComponent<Enemy>().hasGlove)
        {
            other.gameObject.GetComponent<Enemy>().AlterHealth(damageAmount);
        }
    }
}
