using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(SU_CameraFollow))]

public class Fuel_Manager : MonoBehaviour {

    public float FUEL_LEVEL = 500;

    protected const float MULT = 0.0001f;

    public Text fuel_level_text;

    void Start()
    {
        if (fuel_level_text != null)
        {
            Update_Fuel_Level();
        }
    }
    void Update()
    {
        if (FUEL_LEVEL <= 0)
        {
            GetComponent<SU_CameraFollow>().target.GetComponent<SpaceShip>().enabled = false;
        }
        float speed = GetComponent<SU_CameraFollow>().target.GetComponent<Rigidbody>().velocity.magnitude;
        FUEL_LEVEL -= MULT * speed * Time.deltaTime;
        StartCoroutine(Update_Fuel_Level_Cor());
    }
    IEnumerator Update_Fuel_Level_Cor()
    {
        Update_Fuel_Level();
        yield return new WaitForSeconds(2);
    }
    private void Update_Fuel_Level()
    {
        fuel_level_text.text = String.Format("{0:F2} kg", FUEL_LEVEL);
    }
}
