using UnityEngine;
using System.Collections;

public class Quality_Settings : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("quality"))
        {
            int quality = PlayerPrefs.GetInt("quality");
            QualitySettings.SetQualityLevel(quality);
        }
	}

    public void Set_Quality_Level(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    public void Save()
    {
        PlayerPrefs.SetInt("quality", QualitySettings.GetQualityLevel());
        Application.LoadLevel(0);
    }
}
