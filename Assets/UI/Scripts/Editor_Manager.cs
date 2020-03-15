using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine.UI;


public class Editor_Manager : MonoBehaviour
{

    public const string PREFIX = "editor_scene_";

    XmlDocument xml = new XmlDocument();

    public InputField project_name;

    public GameObject left_menu;
    public GameObject objects_panel;


    public GameObject panel_to_button_add;
    public GameObject button_prefab;
    public GameObject add_panel;


    public Button add_button;

    private bool show_left_menu = true;
    private bool show_objects_list = false;
    private bool show_add_objects = false;


    private int number_of_objects = 0;

    [System.Serializable]
    public struct Space_Object
    {
        public GameObject obj;
        public GameObject ui_button;

        public Space_Object(GameObject obj_, GameObject ui_)
        {
            obj = obj_;
            ui_button = ui_;
        }
    };

    public Dictionary<int, Space_Object> objects_list = new Dictionary<int, Space_Object>();

    void Start()
    {
        project_name.keyboardType = TouchScreenKeyboardType.Default;
        project_name.inputType = InputField.InputType.Standard;
        project_name.validation = InputField.Validation.Username;
#if UNITY_EDITOR
        if (!Directory.Exists(Application.dataPath + "/Data/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Data/");
        }

#elif UNITY_ANDROID || UNITY_STANDALONE
        if (!Directory.Exists(Application.persistentDataPath + "/Data/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Data/");
        }
#endif
	//Debug.Log(Application.persistentDataPath + "/Data/" + PREFIX + project_name.value);
    }
    public void Left_Menu()
    {
        show_left_menu = !show_left_menu;
        left_menu.SetActive(show_left_menu);
    }
    public void Exit()
    {
        Application.LoadLevel(0);
    }
    public void Save()
    {
        List<GameObject> objects_to_save = new List<GameObject>();
        if (objects_list.Count != 0 && project_name.value != "" && project_name.value != "Scene Name")
        {
            XmlNode root = xml.CreateElement("Space_Objects");
            xml.AppendChild(root);
            xml.Save(get_path() + ".xml");
            foreach (Space_Object o in objects_list.Values)
            {
                add_xml_node(o.obj);
            }
            Application.LoadLevel(0);
        }
    }
    private string get_path()
    {
        string fileName = project_name.value;
#if UNITY_EDITOR
        return Application.dataPath + "/Data/" + PREFIX + fileName;
#elif UNITY_ANDROID || UNITY_STANDALONE
        return Application.persistentDataPath + "/Data/" + PREFIX + fileName;
#endif
    }
    private void add_xml_node(GameObject ob)
    {
        XmlNode node = xml.SelectSingleNode("Space_Objects");
        Star_Objects_List.TYPE t = ob.GetComponent<Star_Objects_List>().t;
        XmlElement element = xml.CreateElement("Object");
        element.SetAttribute("type", t.ToString());
        element.AppendChild(createNodeByName("transform.x", ob.transform.position.x.ToString()));
        element.AppendChild(createNodeByName("transform.y", ob.transform.position.y.ToString()));
        node.AppendChild(element);
        xml.Save(get_path() + ".xml");
    }
    private XmlNode createNodeByName(string name, string value)
    {
        XmlNode node = xml.CreateElement(name);
        node.InnerText = value;
        return node;
    }
    public void Objects_List()
    {
        show_objects_list = !show_objects_list;
        objects_panel.SetActive(show_objects_list);
        add_button.interactable = show_objects_list;
    }
    public void Close_Objects_List()
    {
        show_objects_list = false;
        objects_panel.SetActive(show_objects_list);
        add_button.interactable = show_objects_list;
    }
    public void Show_Add_Panel()
    {
        show_add_objects = !show_add_objects;
        add_panel.SetActive(show_add_objects);
    }
    public void Close_Add_Panel()
    {
        show_add_objects = false;
        add_panel.SetActive(show_add_objects);
    }

    private string get_name(int lenght = 3)
    {
        string[] first = new string[] { "b", "c", "d", "f", "g", "h", "i", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };
        string[] second = new string[] { "a", "e", "o", "u" };
        string[] third = new string[] { "br", "cr", "dr", "fr", "gr", "pr", "str", "tr", "bl", "cl", "fl", "gl", "pl", "sl", "sc", "sk", "sm", "sn", "sp", "st", "sw", "ch", "sh", "th", "wh" };
        string[] fourth = new string[] { "ae", "ai", "ao", "au", "a", "ay", "ea", "ei", "eo", "eu", "e", "ey", "ua", "ue", "ui", "uo", "u", "uy", "ia", "ie", "iu", "io", "iy", "oa", "oe", "ou", "oi", "o", "oy" };
        string[] fifth = new string[] { "turn", "ter", "nus", "rus", "tania", "hiri", "hines", "gawa", "nides", "carro", "rilia", "stea", "lia", "lea", "ria", "nov", "phus", "mia", "nerth", "wei", "ruta", "tov", "zuno", "vis", "lara", "nia", "liv", "tera", "gantu", "yama", "tune", "ter", "nus", "cury", "bos", "pra", "thea", "nope", "tis", "clite" };
        string[] sixth = new string[] { "una", "ion", "iea", "iri", "illes", "ides", "agua", "olla", "inda", "eshan", "oria", "ilia", "erth", "arth", "orth", "oth", "illon", "ichi", "ov", "arvis", "ara", "ars", "yke", "yria", "onoe", "ippe", "osie", "one", "ore", "ade", "adus", "urn", "ypso", "ora", "iuq", "orix", "apus", "ion", "eon", "eron", "ao", "omia" };
        string s = "";
        List<string[]> words = new List<string[]>();
        words.Add(first);
        words.Add(second);
        words.Add(third);
        words.Add(fourth);
        words.Add(fifth);
        words.Add(sixth);
        for (int i = 0; i < lenght; i++)
        {
            int index = Random.Range(0, words.Count);
            if (i == 0)
                s += words[index][Random.Range(0, words[index].Length)].ToUpper();
            else s += words[index][Random.Range(0, words[index].Length)];
        }


        return s;
    }

    private void Create_Object_And_Button(GameObject ob)
    {
        Vector3 pos = new Vector3(Random.Range(-5, 15), Random.Range(-5, 5), 0);
        GameObject cub = Instantiate(ob, pos, Quaternion.Euler(270,0,0)) as GameObject;
        cub.transform.position = pos;
        cub.transform.tag = "DeepSpace";
        GameObject button = Instantiate(button_prefab);
        button.transform.parent = panel_to_button_add.transform;

        button.GetComponentInChildren<Text>().text = get_name(Random.Range(3, 5));



        Space_Object obj_ = new Space_Object(cub, button);

        objects_list.Add(number_of_objects, obj_);

        int key = number_of_objects;
        button.GetComponentInChildren<Button>().onClick.AddListener(
            delegate { objects_list.Remove(key); Destroy(button); Destroy(cub); }
            );
    }

    public void Add_Object(GameObject sp_obj)
    {
        if (objects_list.Count == 0)
            number_of_objects = 0;
        if (number_of_objects < 6)
        {
            Create_Object_And_Button(sp_obj);
            number_of_objects++;
        }
        Debug.Log(number_of_objects);

    }
}
