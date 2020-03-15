using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public GameObject main_menu;

    public List<GameObject> Menu_Panels;

    void Start()
    {
        Change_Chapter();
        main_menu.SetActive(true);
    }

    public void To_Menu()
    {
        if (main_menu.activeSelf == false)
        {
            Change_Chapter();
            main_menu.SetActive(true);
        }
    }
    private void Change_Chapter(bool to = false)
    {
        foreach (GameObject o in Menu_Panels)
        {
            if (o.activeSelf == !to)
                o.SetActive(to);
        }
    }
    public void To_Chapter(int num = 0)
    {
        if (num < Menu_Panels.Count)
        {
            if (main_menu.activeSelf == true)
                main_menu.SetActive(false);
            Change_Chapter();
            if (Menu_Panels[num].activeSelf == false)
            {
                Menu_Panels[num].SetActive(true);
            }
        }
    }
    public void Load_Level(int level = 0)
    {
        Application.LoadLevel(level);
    }
    public void Exit()
    {
        Application.Quit();
    }
	public void Show_Help(int id=3)
	{
		To_Chapter(id);
	}
}
