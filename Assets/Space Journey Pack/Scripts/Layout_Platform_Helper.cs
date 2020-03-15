using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]

public class Layout_Platform_Helper : MonoBehaviour {

#if UNITY_ANDROID
    public List<GameObject> only_android_layout;
#endif
    private void To(List<GameObject> arr, bool to = false)
    {
        foreach (GameObject o in arr)
            o.SetActive(to);
    }

    void Start()
    {
#if UNITY_ANDROID
        To(only_android_layout, true);
#elif UNITY_STANDLOANE
        To(only_android_layout, false);
#endif
    }
}
