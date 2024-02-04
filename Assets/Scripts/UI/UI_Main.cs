using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Main : UI_Base
{
    enum Texts
    {        
        RemainCash,
        Balance,
        Name_Text,
    }

    enum Buttons
    {
        Logout_Btn,
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
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.Logout_Btn).onClick.AddListener(LogOut);        

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
        Account currentAccount = Managers.BM.CurrentAccount;

        GetText((int)Texts.RemainCash).text = $"{currentAccount.cash:N0}";
        GetText((int)Texts.Balance).text = $"{currentAccount.balance:N0}";
        GetText((int)Texts.Name_Text).text = $"{currentAccount.name}";
    }

    private void LogOut()
    {
        Managers.BM.Logout();
        Managers.BM.OnTransaction = null;
        Managers.UI.CloseUI(gameObject);
        Managers.UI.ShowSceneUI<UI_Login>();
    }
}
