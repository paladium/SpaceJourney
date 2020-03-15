using UnityEngine;
using System.Collections;

public class Custom_Level_UI : MonoBehaviour {

    void Start()
    {
        if (GetComponent<Selector_Manager>())
        {
            Selector_Manager s = GetComponent<Selector_Manager>();
            GameObject manager = GameObject.FindGameObjectWithTag("Custom_Level_Manager");
            manager.GetComponent<Custom_Level>().spaceship = s.spaceships[s.current];
        }
    }
}
