using UnityEngine;
using System.Collections;

public class SpaceShip_2 : MonoBehaviour {

    public float m_RotSpeed = 0.5f;
    public float m_RollSpeed = 0.5f;

    public float m_Accel = 0.15f;
    public float m_AfterBurn = 3.0f;

    // ---

    Rigidbody camBody;


    void Start()
    {
        camBody = gameObject.GetComponent<Rigidbody>();

        if (camBody == null)
        {
            camBody = gameObject.AddComponent<Rigidbody>();
        }
        camBody.useGravity = false;
        camBody.angularDrag = 5;
        camBody.drag = 1;

        if (gameObject.GetComponent<Collider>() == null)
        {
            SphereCollider sph = gameObject.AddComponent<SphereCollider>();
            sph.radius = 2;
        }
    }


    void LateUpdate()
    {
        //
        // rotations
        //

        float dx = Input.GetAxis("Mouse X");
        float dy = Input.GetAxis("Mouse Y");

        if (Input.GetKey(KeyCode.Q))
        {
            camBody.AddTorque(transform.forward * m_RollSpeed, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            camBody.AddTorque(-transform.forward * m_RollSpeed, ForceMode.Acceleration);
        }

        camBody.AddTorque(-transform.up * (-dx * m_RotSpeed), ForceMode.Acceleration);
        camBody.AddTorque(-transform.right * (dy * m_RotSpeed), ForceMode.Acceleration);

        //
        // translations
        //

        float accel = m_Accel;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            accel *= m_AfterBurn;
        }

        if (Input.GetKey(KeyCode.W))
        {
            camBody.AddForce(transform.forward * accel);
        }

        if (Input.GetKey(KeyCode.S))
        {
            camBody.AddForce(-transform.forward * accel);
        }

        if (Input.GetKey(KeyCode.A))
        {
            camBody.AddForce(-transform.right * accel);
        }

        if (Input.GetKey(KeyCode.D))
        {
            camBody.AddForce(transform.right * accel);
        }
    }
}
