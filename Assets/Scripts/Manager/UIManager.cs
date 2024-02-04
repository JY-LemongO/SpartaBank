using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIManager
{
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = Resources.Load<GameObject>("Prefabs/@UI_Root");
            return root;
        }
    }

    public void ShowUI<T>(string path = null) where T : UI_Base => Util.Instantiate<T>(path, Root.transform);    

    public void CloseUI(GameObject go) => Object.Destroy(go);
}
