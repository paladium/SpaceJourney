using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using UnityEngine.UI;

[RequireComponent(typeof(Space_Objects_Manager))]
public class Custom_Level : MonoBehaviour
{

    private XmlDocument xml;
    public Text level_name;
    public Text speed;
    private float speed_ = 0f;
    public GameObject spaceship;

    private Camera cam;

    private int MULT_CONST = 100;

    IEnumerator Update_Speed()
    {
        speed_ = spaceship.GetComponent<Rigidbody>().velocity.magnitude;
        speed.text = System.String.Format("{0:F2}", speed_ * MULT_CONST) + " km/h";
        yield return new WaitForSeconds(1);
    }
    void Update()
    {
        StartCoroutine(Update_Speed());
    }
    void Start()
    {
        cam = Camera.main;
        if (cam.GetComponent<Selector_Manager>())
        {
            spaceship = cam.GetComponent<Selector_Manager>().spaceships[cam.GetComponent<Selector_Manager>().current];
        }
        MULT_CONST = Random.Range(90, 120);
        xml = new XmlDocument();
        if (PlayerPrefs.HasKey("xml_to_load"))
        {
            Parse_Xml(PlayerPrefs.GetString("xml_to_load"));
        }
        if (PlayerPrefs.HasKey("level_name"))
        {
            level_name.text = PlayerPrefs.GetString("level_name");
        }
    }
    private void Parse_Xml(string url)
    {
        if (File.Exists(url))
        {
            xml.Load(url);

            foreach (XmlElement node in xml.SelectNodes("Space_Objects/Object"))
            {
                string t = node.GetAttribute("type");

                Vector3 pos = new Vector3();
                pos.x = float.Parse(node.SelectSingleNode("transform.x").InnerText);
                pos.y = float.Parse(node.SelectSingleNode("transform.y").InnerText);
                pos *= 20;
                if (t == Star_Objects_List.TYPE.Earth.ToString())
                {
                    GetComponent<Space_Objects_Manager>().Create_Object("Earth", pos);
                }
                else if (t == Star_Objects_List.TYPE.Mars.ToString())
                {
                    GetComponent<Space_Objects_Manager>().Create_Object("Mars", pos);
                }
                else if (t == Star_Objects_List.TYPE.Saturn.ToString())
                {
                    GetComponent<Space_Objects_Manager>().Create_Object("Saturn", pos);
                }
                else if (t == Star_Objects_List.TYPE.Jupiter.ToString())
                {
                    GetComponent<Space_Objects_Manager>().Create_Object("Jupiter", pos);
                }
				else if (t == Star_Objects_List.TYPE.Neptune.ToString())
                {
                    GetComponent<Space_Objects_Manager>().Create_Object("Neptune", pos);
                }
				else if (t == Star_Objects_List.TYPE.Earth_Like.ToString())
                {
                    GetComponent<Space_Objects_Manager>().Create_Object("Earth_Like", pos);
                }
            }
        }
    }
}
