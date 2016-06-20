using UnityEngine;
using System.Collections;

public class SmoothPositionMovement : MonoBehaviour
{
    #region Instance
    //Static Singleton Instance
    public static SmoothPositionMovement _Instance = null;

    //property to get instance
    public static SmoothPositionMovement Instance
    {
        get
        {
            //if we do not have Instance yet
            if (_Instance == null)
            {
                _Instance = (SmoothPositionMovement)FindObjectOfType(typeof(SmoothPositionMovement));
            }
            return _Instance;
        }
    }
    #endregion

    //FOR GAMEOBJECTS
    public IEnumerator MoveGameObject(GameObject objectToMove, Vector3 end,float timeTaken)
    {
        //Debug.Log("Moving");
        float t = 0;
        Vector3 dist = end - objectToMove.transform.position;
        
        //Debug.Log("distance=" + dist.magnitude);
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, (dist.magnitude/timeTaken)*Time.deltaTime);
            yield return null;
            t += Time.deltaTime;
        }
        //Debug.Log("Moving Done");
    }

    //FOR CANVAS ELEMENTS
    public IEnumerator MoveCanvasElement(RectTransform objectToMove, Vector3 end, float timeTaken)
    {
        float t = 0;
        Vector3 dist = end - objectToMove.transform.position;
        while (objectToMove.localPosition != end)
        {
            objectToMove.localPosition = Vector3.MoveTowards(objectToMove.localPosition, end, (dist.magnitude/timeTaken) * Time.deltaTime);
            yield return null;
            t += Time.deltaTime;

        }
    }
}
