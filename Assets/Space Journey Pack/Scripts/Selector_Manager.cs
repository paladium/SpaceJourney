using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector_Manager : MonoBehaviour
{

    public List<GameObject> spaceships = new List<GameObject>();

    public int current = 0;

    public bool is_custom_level = false;

    void Awake()
    {
        if (is_custom_level)
        {
            if (PlayerPrefs.HasKey("current"))
            {
                current = PlayerPrefs.GetInt("current");
            }
        }
    }
    void Start()
    {
        if (is_custom_level)
        {
            if (GetComponent<SU_CameraFollow>())
            {
                GetComponent<SU_CameraFollow>().target = spaceships[current].transform;
            }
        }
        Show_Current(current);
    }
    public void Next()
    {
        if (current < spaceships.Count - 1)
        {
            current++;
            Show_Current(current);
        }
    }
    public void Prev()
    {
        if (current > 0)
        {
            current--;
            Show_Current(current);
        }
    }
    public void Save()
    {
        PlayerPrefs.SetInt("current", current);
        Application.LoadLevel(0);
    }
    private void Show_Current(int curr = 0)
    {
        foreach (GameObject o in spaceships)
        {
            if (o.activeSelf)
                o.SetActive(false);
        }
        spaceships[curr].SetActive(true);
    }
}
