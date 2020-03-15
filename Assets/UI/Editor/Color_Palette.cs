using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;

public class Color_Palette : EditorWindow
{

    private List<Color32> colors = new List<Color32> { 
        new Color32(26, 188, 156, 255), 
        new Color32(46, 204, 113,255),
        new Color32(52, 152, 219,255),
        new Color32(155, 89, 182,255),
        new Color32(52, 73, 94,255),
        new Color32(22, 160, 133,255),
        new Color32(39, 174, 96,255),
        new Color32(41, 128, 185,255),
        new Color32(142, 68, 173,255),
        new Color32(44, 62, 80,255),
        new Color32(241, 196, 15,255),
        new Color32(230, 126, 34,255),
        new Color32(231, 76, 60,255),
        new Color32(236, 240, 241,255),
        new Color32(149, 165, 166,255),
        new Color32(243, 156, 18,255),
        new Color32(211, 84, 0,255),
        new Color32(192, 57, 43,255),
        new Color32(189, 195, 199,255),
        new Color32(127, 140, 141,255)
    };
    int in_one_row = 5;
    int space = 10;
    int size = 50;
    int top_space = 50;
    int left_space = 20;
    public int selected = 0;
    [MenuItem("UI Tools/Flat Color Palette")]

    static void Init()
    {
        Color_Palette window = (Color_Palette)EditorWindow.GetWindow(typeof(Color_Palette));
        window.title = "Flat Color Palette";
        window.Show();
    }
    void Draw_Colors()
    {

        int k = 0;
        int top = 0;
        for (int i = 0; i < colors.Count; i++)
        {
            Rect r;
            if (top == 0)
            {
                r = new Rect(left_space + k * (size + space), top_space, size, size);
            }
            else
            {
                r = new Rect(left_space + k * (size + space), top * (size + space) + top_space, size, size);
            }
            EditorGUIUtility.DrawColorSwatch(r, colors[i]);
            k++;
            if (k == in_one_row)
            {
                k = 0;
                top++;
            }
        }
    }
    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        string[] opt = new string[] { "TURQUOISE", "EMERALD", "PETER RIVER", "AMETHYST", "WET ASPHALT", "GREEN SEA", "NEPHRITIS", "BELIZE HOLE", "WISTERIA", "MIDNIGHT BLUE", "SUN FLOWER", "CARROT", "ALIZARIN", "CLOUDS", "CONCRETE", "ORANGE", "PUMPKIN", "POMEGRANATE", "SILVER", "ASBESTOS" };
        selected = EditorGUILayout.Popup(selected, opt);
        if (GUILayout.Button("Apply"))
        {
            if (Selection.activeGameObject != null)
            {
                if (Selection.activeGameObject.GetComponent<Image>())
                {
                    Selection.activeGameObject.GetComponent<Image>().color = colors[selected];
                    EditorUtility.SetDirty(Selection.activeGameObject);
                }
                else if (Selection.activeGameObject.GetComponent<Renderer>())
                {
                    Selection.activeGameObject.GetComponent<Renderer>().material.color = colors[selected];
                }
            }
        }
        EditorGUILayout.EndHorizontal();
        Draw_Colors();

    }
}
