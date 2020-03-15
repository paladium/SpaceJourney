using UnityEngine;
using System.Collections;
using UnityEngine.UI;

enum PREFIX
{
    KM,
    AU,
    PK
};
[RequireComponent(typeof(SU_CameraFollow))]
public class Distance_Manager : MonoBehaviour
{

    protected float MULT = 3000f;

    public float distance = 0;

    PREFIX unit;

    public Text distance_text;

    void Update_Distance()
    {
        if (unit == PREFIX.AU)
            MULT = 0.01f;
        if (unit == PREFIX.PK)
            MULT = 0.0000001f;
        if ((int)distance == 149597871 && unit == PREFIX.KM)
        {
            distance = 1;
            unit = PREFIX.AU;
            Debug.Log("changed");
        }
        /*if (distance >= 206264.806f && unit == PREFIX.AU)
        {
            distance /= 206264.806f;
            unit = PREFIX.PK;
        }*/
        distance += GetComponent<SU_CameraFollow>().target.GetComponent<Rigidbody>().velocity.magnitude * Time.deltaTime * MULT;
        distance_text.text = System.String.Format("{0:F0} {1}", distance, unit.ToString());
    }
    IEnumerator Update_Dist()
    {
        Update_Distance();
        yield return new WaitForSeconds(1);
    }
    void Update()
    {
        StartCoroutine(Update_Dist());
    }
}
