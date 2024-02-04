using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instace; // ������ �Ŵ���
    static Managers Instance { get { Init(); return s_instace; } } // ������ �Ŵ����� ��ȯ

    UIManager   _ui = new UIManager();
    BankManager _bm = new BankManager();

    public static UIManager     UI => Instance?._ui;
    public static BankManager   BM => Instance?._bm;

    [Range(10000, 500000)][SerializeField] int _initCash;
    [Range(10000, 500000)][SerializeField] int _initBalance;

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

            BM.Init(Instance._initCash, Instance._initBalance);
            Util.Instantiate<UI_Main>();            
        }
    }
}
