using UnityEngine;
using System.Collections;

public class Orto_Zoom : MonoBehaviour
{

    public float distance = 50;
    public float S = 50;
    public float damping = 10;
    public float min_D = 5;
    public float max_D = 15;

    void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			if(GetComponent<Camera>().orthographic)
			{
				distance = deltaMagnitudeDiff * S;
				distance = Mathf.Clamp(distance, min_D + 2, max_D - 2);
				GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, distance, Time.deltaTime * damping);
				//GetComponent<Camera>().orthographicSize += distance * Time.deltaTime;
			}
        }
        
        
#else
        if (GetComponent<Camera>().orthographic)
        {
            distance -= Input.GetAxis("Mouse ScrollWheel") * S;
            distance = Mathf.Clamp(distance, min_D, max_D);
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, distance, Time.deltaTime * damping);
        }

#endif
    }
}
