using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Selector_UI : MonoBehaviour {

    public List<GameObject> ui = new List<GameObject>();

    void Start()
    {
        Change_To(0);
    }
    public void Next()
    {
        Change_To(GetComponent<Selector_Manager>().current);
    }
    public void Prev()
    {
        Change_To(GetComponent<Selector_Manager>().current);
    }
    void Change_To(int to = 0)
    {
        if (GetComponent<Selector_Manager>())
        {
            Selector_Manager s = GetComponent<Selector_Manager>();
            ui[0].GetComponent<Text>().text = s.spaceships[s.current].GetComponent<Spaceship_Object>().name;
            ui[1].GetComponent<Text>().text = "Max speed " + s.spaceships[s.current].GetComponent<Spaceship_Object>().speed.ToString() + " velocity of light";
            ui[2].GetComponent<Text>().text = "Mass " + s.spaceships[s.current].GetComponent<Spaceship_Object>().mass.ToString() + " kg";
        }
    }
}
