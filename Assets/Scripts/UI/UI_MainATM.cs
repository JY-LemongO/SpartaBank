using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainATM : UI_Base
{
    enum Buttons // Button ������Ʈ�� ������ �ִ� ������Ʈ�� ã�� ���� enum. ������Ʈ �̸��� ��ġ�ؾ� �Ѵ�.
    {
        Deposit_Btn,
        Withdraw_Btn,
    }

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons)); // �����տ� �ִ� ��� ��ư ������Ʈ�� ���ε�

        GetButton((int)Buttons.Deposit_Btn).onClick.AddListener(OpenDepositWindow);
        GetButton((int)Buttons.Withdraw_Btn).onClick.AddListener(OpenWithdrawWindow);

        return true;
    }

    private void OpenDepositWindow()
    {        
        Managers.UI.ShowUI<UI_Deposit>(Util.PATH);
        Managers.UI.CloseUI(gameObject);
    }

    private void OpenWithdrawWindow()
    {        
        Managers.UI.ShowUI<UI_Withdraw>(Util.PATH);
        Managers.UI.CloseUI(gameObject);
    }
}
