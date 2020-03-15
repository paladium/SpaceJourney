using UnityEngine;
using System.Collections;

public class Camera_Free_Orbit : MonoBehaviour
{

    private Quaternion target_rot;
    public float rot_speed = 2500.0f;
    public float rot_damp = 10.0f;
    public float target_distance = 20.0f;
    public float distance_min = 20.0f;
    public float distance_max = 20.0f;
    public float distance_speed = 3.0f;
    public float distance_damp = 10.0f;


    public float DragX
    {
        get
        {

#if UNITY_IPHONE == true || UNITY_ANDROID == true
            var touches = Input.touches;

            if (touches != null && Input.touchCount == 1)
            {
                var touch = touches[0];

                if (touch.phase == TouchPhase.Moved)
                {
                    return touch.deltaPosition.x * touch.deltaTime / Mathf.Min(Screen.width, Screen.height);
                }
            }

            return 0.0f;
#else
			return UnityEngine.Input.GetAxis("Mouse X") / Mathf.Min(Screen.width, Screen.height);
#endif
        }
    }
    public float DragY
    {
        get
        {

#if UNITY_IPHONE == true || UNITY_ANDROID == true
            var touches = Input.touches;

            if (touches != null && Input.touchCount == 1)
            {
                var touch = touches[0];

                if (touch.phase == TouchPhase.Moved)
                {
                    return touch.deltaPosition.y * touch.deltaTime / Mathf.Min(Screen.width, Screen.height);
                }
            }

            return 0.0f;
#else
			return UnityEngine.Input.GetAxis("Mouse Y") / Mathf.Min(Screen.width, Screen.height);
#endif
        }
    }
    public float Zoom
    {
        get
        {
#if UNITY_IPHONE == true || UNITY_ANDROID == true
            return 0.0f;
#else
			return Input.GetAxis("Mouse ScrollWheel");
#endif
        }
    }
    public float DampenFactor(float dampening, float elapsed)
    {
        return 1.0f - Mathf.Pow((float)System.Math.E, -dampening * elapsed);
    }
    public void SetLocalRotation(Transform t, Quaternion q)
    {
        if (t != null)
        {
#if UNITY_EDITOR == true
            if (Application.isPlaying == false && t.localRotation == q) return;
#endif
            t.localRotation = q;
        }
    }
    public void SetLocalPosition(Transform t, Vector3 v)
	{
		if (t != null)
		{
#if UNITY_EDITOR == true
			if (Application.isPlaying == false && t.localPosition == v) return;
#endif
			t.localPosition = v;
		}
	}
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            var x = DragY * -rot_speed;
            var y = DragX * rot_speed;
            target_rot *= Quaternion.Euler(x, y, 0);
        }
        target_distance -= Zoom * (1.0f + target_distance - distance_min) * distance_speed;
        target_distance = Mathf.Clamp(target_distance, distance_min, distance_max);
        var current_dist = transform.localPosition.magnitude;
        var current_rot = transform.localRotation;
        if (Application.isPlaying == true)
		{
			var rotationDampenFactor = DampenFactor(rot_damp, Time.deltaTime);
			var distanceDampenFactor = DampenFactor(distance_damp, Time.deltaTime);
			
			current_rot = Quaternion.Slerp(current_rot, target_rot, rotationDampenFactor);
			current_dist = Mathf.Lerp(current_dist, target_distance, distanceDampenFactor);
		}
		else
		{
			current_rot = target_rot;
			current_dist = target_distance;
		}
        SetLocalRotation(transform, current_rot);
        SetLocalPosition(transform, current_rot * new Vector3(0,0, - current_dist));
    }

}
