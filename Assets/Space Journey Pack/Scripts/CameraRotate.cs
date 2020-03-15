using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Camera))]
public class CameraRotate : MonoBehaviour {

    public Transform target;
    public float speed = 5.0f;

    void Update()
    {
	#if UNITY_EDITOR
	if (Input.GetMouseButton(0))
	{
		transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * speed * Time.deltaTime);
	}
	#endif
#if UNITY_STANDLOANE
        if (Input.GetMouseButton(0))
        {
            //transform.LookAt(target);
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * speed * Time.deltaTime);
        }
#else
        transform.RotateAround(target.position, Vector3.up, -Input.acceleration.x * Time.deltaTime * speed);
#endif
    }
}
