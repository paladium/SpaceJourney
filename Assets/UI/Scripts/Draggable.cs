using UnityEngine;
using System.Collections;
using System;


public class Draggable : MonoBehaviour {

    Vector3 screenPoint;
    Vector3 offset;
    float yPos;
    void OnMouseDown()
    {
        Vector3 scanPos = gameObject.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(scanPos);
        //Debug.Log (Input.mousePosition.x);


        offset = scanPos - Camera.main.ScreenToWorldPoint(
        new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        //Debug.Log(curPosition[0]);

        transform.position = curPosition;
    }
}
