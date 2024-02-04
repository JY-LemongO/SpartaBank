using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Main : UI_Base
{
    enum Texts
    {        
        RemainCash,
        Balance,
    }

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;        

        BindText(typeof(Texts));
        AccountInfoInit();

        Managers.BM.OnTransaction -= ChangeAccountInfo;
        Managers.BM.OnTransaction += ChangeAccountInfo;

        return true;
    }

    private void ChangeAccountInfo(int cash, int balance)
    {
        GetText((int)Texts.RemainCash).text = $"{cash:N0}";
        GetText((int)Texts.Balance).text = $"{balance:N0}";
    }

    private void AccountInfoInit()
    {
        GetText((int)Texts.RemainCash).text = $"{Managers.BM.Cash:N0}";
        GetText((int)Texts.Balance).text = $"{Managers.BM.Balance:N0}";
    }
}
