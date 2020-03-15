using UnityEngine;
using System.Collections;

public class Accelerometer_Test : MonoBehaviour
{

    public float speed = 100;

    public float rot_speed = 100;

    public float AmbientSpeed = 100;

    void FixedUpdate()
    {
        //transform.Translate(new Vector3(Input.acceleration.x * Time.deltaTime * speed, 0, -Input.acceleration.z * Time.deltaTime * speed));
        GetComponent<Rigidbody>().AddForce(new Vector3(Input.acceleration.x * Time.deltaTime * speed, 0, -Input.acceleration.z * Time.deltaTime * speed));

        Quaternion AddRot = Quaternion.identity;
        float roll = 0;
        float pitch = 0;
        float yaw = 0;
        roll = Input.acceleration.x * (Time.fixedDeltaTime * rot_speed);
        pitch = Input.acceleration.z * (Time.fixedDeltaTime * rot_speed / 10);
        yaw = Input.acceleration.y * (Time.fixedDeltaTime * rot_speed);
        AddRot.eulerAngles = new Vector3(-pitch, 0, -roll);
        GetComponent<Rigidbody>().rotation *= AddRot;
        Vector3 AddPos = Vector3.forward;
        AddPos = GetComponent<Rigidbody>().rotation * AddPos;
        //GetComponent<Rigidbody>().velocity = AddPos * (Time.fixedDeltaTime * AmbientSpeed);

        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                //GetComponent<Rigidbody>().velocity += Time.deltaTime * speed * Vector3.forward;
                GetComponent<Rigidbody>().AddForce(speed * transform.TransformDirection(Vector3.forward), ForceMode.VelocityChange);
            }
        }
    }
}
