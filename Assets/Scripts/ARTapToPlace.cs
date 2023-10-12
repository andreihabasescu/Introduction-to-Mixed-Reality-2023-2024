using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlace : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject objectToPlace;

    private static List<GameObject> gameObjs = new List<GameObject>();

    public Camera arCamera;

    private static Vector3 center;

    private static float radius;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private static GameObject circleObj;

    private void Awake()
    {
        radius = 0.0055f;
        center = Vector3.zero;
        circleObj = gameObject;
    }

    private void Update()
    {
        Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.touchCount > 0)
        {
            transform.Rotate(Vector3.up);
            if (raycastManager.Raycast(ray, hits, TrackableType.Planes))
            {
                Pose hitPose = hits[0].pose;

                GameObject newObj = Instantiate(objectToPlace, hitPose.position, hitPose.rotation, transform);
                gameObjs.Add(newObj);

                if (gameObjs.Count == 1)
                {
                    center = gameObjs[0].transform.position + new Vector3(0, 0, 0.005f);
                    gameObject.transform.position = center;

                    gameObjs[0].AddComponent<MoveTowards>();
                    gameObjs[0].GetComponent<MoveTowards>().setNumber(1);
                }
                else
                {
                    radius = radius + 0.005f;
                    gameObjs[gameObjs.Count - 1].AddComponent<MoveTowards>();
                    gameObjs[gameObjs.Count - 1].GetComponent<MoveTowards>().setNumber(gameObjs.Count);
                }
            }
        }
    }

    public static GameObject getCircle()
    {
        return circleObj;
    }

    public static int getObjCount()
    {
        return gameObjs.Count;
    }

    public static Vector3 getCenter()
    {
        return center;
    }

    public static float getRadius() { return radius; }
}
