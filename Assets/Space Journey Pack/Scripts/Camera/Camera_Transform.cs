using UnityEngine;
using System.Collections;

public class Camera_Transform : MonoBehaviour
{

    public float Min_Distance = 2.0f;
    public float Max_Distance = 10.0f;
    float distance;
    public float smoothness;
    #region height
    float height;
    public float Min_Height = 2.0f;
    public float Max_Height = 10.0f;
    #endregion

    #region rotate
    public float speed = 100;
    #endregion
    void Start()
    {
        distance = GetComponent<Smooth_Follow>().distance;
    }
    void Update()
    {
        #region zoom
        distance -= Input.GetAxis("Mouse ScrollWheel") * smoothness;
        distance = Mathf.Clamp(distance, Min_Distance, Max_Distance);
        GetComponent<Smooth_Follow>().distance = Mathf.Lerp(GetComponent<Smooth_Follow>().distance, distance, Time.deltaTime * smoothness);
        #endregion

        #region height
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            height -= Input.GetAxis("Mouse ScrollWheel") * smoothness;
            height = Mathf.Clamp(height, Min_Height, Max_Height);
            GetComponent<Smooth_Follow>().height = Mathf.Lerp(GetComponent<Smooth_Follow>().height, height, Time.deltaTime * smoothness);
        }
        #endregion

        #region rotate
        var building = GetComponent<Smooth_Follow>().target;
        if (Input.GetMouseButton(0))
        {
            if (Input.GetKey(KeyCode.A))
                transform.RotateAround(building.position, Vector3.up, Time.deltaTime * speed);
            else if (Input.GetKey(KeyCode.D))
                transform.RotateAround(building.position, Vector3.down, Time.deltaTime * speed);
            GetComponent<Smooth_Follow>().enabled = false;
        }
        else
            GetComponent<Smooth_Follow>().enabled = true;
        #endregion
        #region change_target
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Atom")
                {
                    GetComponent<Smooth_Follow>().target = hit.collider.transform;
                }
            }
        }
        #endregion
        #region reset_target
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<Smooth_Follow>().target = GameObject.FindGameObjectWithTag("Reaction").transform;
        }
        #endregion
    }
}
