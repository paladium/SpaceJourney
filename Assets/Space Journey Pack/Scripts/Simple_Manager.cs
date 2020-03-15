using UnityEngine;
using System.Collections;

public class Simple_Manager : MonoBehaviour {

	public void Load_Level(int level = 0)
	{
		Application.LoadLevel(level);
	}
	public void Expand(GameObject target)
	{
		target.SetActive(!target.activeSelf);
	}
}
