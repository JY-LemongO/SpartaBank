using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public static class Util // �̹� ������ Util Ŭ���� ���� �ʿ������ ���� �ؾ��ϴϱ� ���뼺 Util static Ŭ���� ����
{
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if(go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if(component != null)
                        return component;
                }                    
            }
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static GameObject Instantiate<T>(string path = null, Transform parent = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(path))
            path = "Prefabs";        

        string fileName = typeof(T).Name;

        GameObject prefab = Resources.Load<T>($"{path}/{fileName}").gameObject;
        
        GameObject go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;

        return go;
    }
}
