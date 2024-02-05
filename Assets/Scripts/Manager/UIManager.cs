using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
            {
                GameObject go = Resources.Load<GameObject>($"Prefabs/@UI_Root").gameObject;
                root = Object.Instantiate(go);
                root.name = go.name;                
            }                
            return root;
        }
    }

    public void ShowSceneUI<T>(string path = null) where T : UI_Base => Util.Instantiate<T>(path);
    public void ShowMenuUI<T>(string path = "Prefabs/UI") where T : UI_Base => Util.Instantiate<T>(path, Root.transform);
    public void ShowPopupUI<T>(string alert, string path = "Prefabs/UI") where T : UI_Base
    {
        UI_Popup popup = Util.Instantiate<T>(path, Root.transform).GetComponent<UI_Popup>();
        popup.SetupAlert(alert);
    }
    
    public void CloseUI(GameObject go) => Object.Destroy(go);
}
