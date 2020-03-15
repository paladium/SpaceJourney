using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{

    public float AmbientSpeed = 100.0f;

    public float RotationSpeed = 200.0f;

    public float accel = 5.0f;

    public float speed = 100;


    void FixedUpdate()
    {
        Quaternion AddRot = Quaternion.identity;
        float roll = 0;
        float pitch = 0;
        float yaw = 0;
#if UNITY_STANDLOANE || UNITY_EDITOR
        roll = Input.GetAxis("Roll") * (Time.deltaTime * RotationSpeed);
        pitch = Input.GetAxis("Pitch") * (Time.deltaTime * RotationSpeed);
        yaw = Input.GetAxis("Yaw") * (Time.deltaTime * RotationSpeed);
        AddRot.eulerAngles = new Vector3(-pitch, yaw, -roll);
        GetComponent<Rigidbody>().rotation *= AddRot;
        Vector3 AddPos = Vector3.forward;
        AddPos = GetComponent<Rigidbody>().rotation * AddPos;
        GetComponent<Rigidbody>().velocity = AddPos * (Time.deltaTime * AmbientSpeed);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Rigidbody>().velocity = accel * Time.deltaTime * AmbientSpeed * AddPos;
        }
#else
        GetComponent<Rigidbody>().AddForce(new Vector3(Input.acceleration.x * Time.deltaTime * speed, 0, -Input.acceleration.z * Time.deltaTime * speed));
        roll = Input.acceleration.x * (Time.fixedDeltaTime * RotationSpeed);
        pitch = Input.acceleration.z * (Time.fixedDeltaTime * RotationSpeed / 5);
        yaw = Input.acceleration.y * (Time.fixedDeltaTime * RotationSpeed);
        AddRot.eulerAngles = new Vector3(-pitch, 0, -roll);
        GetComponent<Rigidbody>().rotation *= AddRot;
        Vector3 AddPos = Vector3.forward;
        AddPos = GetComponent<Rigidbody>().rotation * AddPos;

        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                GetComponent<Rigidbody>().AddForce(speed * transform.TransformDirection(Vector3.forward), ForceMode.VelocityChange);
            }
        }

#endif
    }
}
