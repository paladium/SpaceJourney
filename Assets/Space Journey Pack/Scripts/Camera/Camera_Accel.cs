using UnityEngine;
using System.Collections;

public class Camera_Accel : MonoBehaviour {

    public Transform target;

    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, -Input.acceleration.x * Time.deltaTime * 50);
    }
}
