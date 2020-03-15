using UnityEngine;

public class Warp_Drive : MonoBehaviour
{

    Color32 Get_Random_Color(int min = 10)
    {
        Color32 c = new Color();
        c.r = (byte)Random.Range(min, 255);
        c.g = (byte)Random.Range(min, 255);
        c.b = (byte)Random.Range(min, 255);
        c.a = (byte)Random.Range(120,160);
        return c;
    }
    void Start()
    {
        if (GetComponent<Renderer>())
        {
            GetComponent<Renderer>().material.SetColor("_Color", Get_Random_Color());
        }
    }
}
