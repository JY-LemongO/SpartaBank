using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

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

    public void ShowMenuUI<T>(string path = null) where T : UI_Base => Util.Instantiate<T>(path, Root.transform);


    public void ShowPopupUI()
    {

    }

    public void ClosePopupUI()
    {

    }
}
