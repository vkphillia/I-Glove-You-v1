using UnityEngine;
using System.Collections;

public class HealthPU : MonoBehaviour
{

	public int HealthUp;

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.layer == 8 && !OfflineManager.Instance.PlayerHolder1.hasGlove)
		{

            if ((OfflineManager.Instance.PlayerHolder1.myHealth + HealthUp) > OfflineManager.Instance.MaxHealth)
            {
                OfflineManager.Instance.PlayerHolder1.myHealth = OfflineManager.Instance.MaxHealth;
            }
            else
            {
                OfflineManager.Instance.PlayerHolder1.myHealth += HealthUp;
            }

            OfflineManager.Instance.PlayerHolder1.myHealthText_HUD.text = "Health " + OfflineManager.Instance.PlayerHolder1.myHealth;
        }

        else if (other.gameObject.layer == 10 && !OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
            if ((OfflineManager.Instance.PlayerHolder2.myHealth + HealthUp)> OfflineManager.Instance.MaxHealth)
            {
                OfflineManager.Instance.PlayerHolder2.myHealth = OfflineManager.Instance.MaxHealth;
            }
            else
            {
                OfflineManager.Instance.PlayerHolder2.myHealth += HealthUp;
            }

            OfflineManager.Instance.PlayerHolder2.myHealthText_HUD.text = "Health " + OfflineManager.Instance.PlayerHolder2.myHealth;
        }
        OfflineManager.Instance.PUPicked = true;
        gameObject.SetActive (false);
	}

}
