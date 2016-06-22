using UnityEngine;
using System.Collections;

public class DragGameObject : MonoBehaviour
{
    bool dragging;
    bool threshold;

    void OnMouseDrag()
    {
        dragging = true;

        if (!threshold)
        {
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            objectPosition.z = 0f;
            objectPosition.y = transform.position.y;
            transform.position = objectPosition;
        }
    }

    void OnMouseUp()
    {
        dragging = false;
        
        //Invoke("Reset", 0.5f);
        
    }

    IEnumerator Reset()
    {
        transform.position = new Vector3(0, transform.position.y, 0);
        Debug.Log("reset started");
        while (dragging)
        {
            yield return null;
        }
        Debug.Log("reset done");
        threshold = false;
    }

    void Update()
    {
        if(dragging&&!threshold)
        {
            if (transform.position.x > 0.3)
            {
                Debug.Log("Right");
                threshold = true;
                StartCoroutine(Reset());
                //OnMouseUp();
                //Invoke("OnMouseUp",0.2f);
                gameObject.GetComponentInParent<SwipePlayer>().MoveRight();
            }
            else if (transform.position.x < -0.3)
            {
                Debug.Log("Left");
                threshold = true;
                StartCoroutine(Reset());
                //OnMouseUp();
                //Invoke("OnMouseUp", 0.2f);
                gameObject.GetComponentInParent<SwipePlayer>().MoveLeft();
            }
        }
    }


}
