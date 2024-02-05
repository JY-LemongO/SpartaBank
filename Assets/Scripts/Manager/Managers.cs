using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instace; // ������ �Ŵ���
    static Managers Instance { get { Init(); return s_instace; } } // ������ �Ŵ����� ��ȯ

    UIManager       _ui = new UIManager();
    BankManager     _bm = new BankManager();
    AccountManager  _am = new AccountManager();

    public static UIManager         UI => Instance?._ui;
    public static BankManager       BM => Instance?._bm;
    public static AccountManager    AM => Instance?._am;    

    private void Awake()
    {
        Init();
    }

    private static void Init() // ������ �Ŵ��� s_instance�� ������ "@Managers" �� ã�� ��ȯ, �� ã���� ���ο� �̱��� ����
    {
        if (s_instace == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Managers>();
            }

            s_instace = go.GetComponent<Managers>();
            
            AM.LoadAllAccounts();
            UI.ShowSceneUI<UI_Login>();
        }
    }
}
