using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Space_Objects_Manager : MonoBehaviour {

    [System.Serializable]
    public struct Space_Object
    {
        public string name;
        public GameObject prefab;
    };

    public const float SPACE_MUL = 1;

    public List<Space_Object> space_objects = new List<Space_Object>();

    public void Create_Object(string name, Vector3 pos)
    {
        if (space_objects.Exists(item => item.name == name))
        {
            GameObject space = space_objects.Find(item => item.name == name).prefab;
            
            GameObject new_obj = Instantiate(space, pos, Quaternion.identity) as GameObject;
            new_obj.transform.localScale *= SPACE_MUL;
        }
    }
}
