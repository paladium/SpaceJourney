using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine.UI;

public class Xml_Reader : MonoBehaviour
{

    public GameObject grid;

    public const string PREFIX = "editor_scene_";

    XmlDocument doc;

    public GameObject button;

    private string file_name;
	
	private Dictionary<string, string> level_names = new Dictionary<string, string>();

    public GameObject no_level;

    void Start()
    {
        no_level.SetActive(false);
		//PlayerPrefs.DeleteAll();
        doc = new XmlDocument();
        Load_Xml();
        string res = PlayerPrefs.GetString("xml_to_load");
    }
    private void Load_Xml()
    {
        if (Directory.Exists(get_path()))
        {
            var info = new DirectoryInfo(get_path());
            var fileInfo = info.GetFiles();
            if (fileInfo.Length == 0 || !Directory.Exists(get_path()))
            {
                no_level.SetActive(true);
            }
            for (int i = 0; i < fileInfo.Length; i++)
            {
                if (fileInfo[i].Extension == ".xml")
                {
                    if (fileInfo[i].Name.Substring(0, PREFIX.Length) == PREFIX)
                    {
                        string[] name = fileInfo[i].Name.Split('_');
                        GameObject butt = Instantiate(button) as GameObject;
                        butt.transform.parent = grid.transform;
                        string[] res = name[2].Split('.');
                        file_name = fileInfo[i].FullName;
                        butt.name = "Scene: " + res[0];

                        level_names.Add(butt.name, file_name);

                        butt.transform.GetChild(0).GetComponent<Text>().text = res[0].ToUpper();

                        butt.GetComponent<Button>().onClick.AddListener(
                            delegate { PlayerPrefs.SetString("xml_to_load", level_names[butt.name]); PlayerPrefs.SetString("level_name", res[0].ToUpper()); Application.LoadLevel(2); }
                            );
                    }
                }
            }
        }
        else
        {
            no_level.SetActive(true);
            Directory.CreateDirectory(get_path());
        }
        
    }
    private string get_path()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Data/";
#elif UNITY_ANDROID || UNITY_STANDALONE
        return Application.persistentDataPath + "/" + "Data/";
#endif
    }
}
