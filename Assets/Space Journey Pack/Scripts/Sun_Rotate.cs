using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Sun_Rotate : MonoBehaviour
{

    public float speed = 10.0f;
    public bool is_rotate = true;
    public Toggle is_rotate_toggle;
    void Update()
    {
        if(is_rotate)
            transform.Rotate(speed * Time.deltaTime, speed * Time.deltaTime, speed * Time.deltaTime);
    }
    public void Is_Rotate()
    {
        is_rotate = is_rotate_toggle.isOn;
    }
}
