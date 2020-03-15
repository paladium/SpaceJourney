using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Gallery : MonoBehaviour
{

    [System.Serializable]
    public struct Gallery_Image
    {
        public Texture2D img;
        public string name;
    };

    public List<Gallery_Image> images;
    public RawImage gallery;
    public Text title;

    public int current = 0;
	
	private List<E> ShuffleList<E>(List<E> inputList)
	{
		 List<E> randomList = new List<E>();

		 System.Random r = new System.Random();
		 int randomIndex = 0;
		 while (inputList.Count > 0)
		 {
			  randomIndex = r.Next(0, inputList.Count);
			  randomList.Add(inputList[randomIndex]);
			  inputList.RemoveAt(randomIndex);
		 }

		 return randomList;
	}
	
    void Start()
    {
		images = ShuffleList(images);
        Change_To();
    }
    private void Change_To(int to = 0)
    {
        gallery.texture = images[to].img;
        title.text = images[to].name;
        gallery.SetNativeSize();
    }
    public void Next()
    {
        if (current < images.Count - 1)
        {
            current++;
            Change_To(current);
        }
    }
    public void Prev()
    {
        if (current > 0)
        {
            current--;
            Change_To(current);
        }
    }
    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Next();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Prev();
#endif
    }
}
