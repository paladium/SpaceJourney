using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(SU_CameraFollow))]
public class Time_Manager : MonoBehaviour
{

    private DateTime time;

    public Text time_text;

    protected const float MULT = 0.001f;

    void Start()
    {
        time = DateTime.Now;
    }
    private void Update_T()
    {
        float speed = GetComponent<SU_CameraFollow>().target.GetComponent<Rigidbody>().velocity.magnitude;
        time = time.AddMinutes(MULT * speed * Time.deltaTime);
        time_text.text = String.Format("{0:d MMMM, yyyy / HH:mm}", time);
    }
    IEnumerator Update_Time()
    {
        Update_T();
        yield return new WaitForSeconds(1);
    }
    void Update()
    {
        StartCoroutine(Update_Time());
        //Update_T();
    }
}
