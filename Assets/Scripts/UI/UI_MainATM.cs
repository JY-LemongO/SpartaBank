using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainATM : UI_Base
{
    enum Buttons // Button 컴포넌트를 가지고 있는 오브젝트를 찾기 위한 enum. 오브젝트 이름과 일치해야 한다.
    {
        Deposit_Btn,
        Withdraw_Btn,
        Remittance_Btn,
    }

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons)); // 프리팹에 있는 모든 버튼 컴포넌트를 바인드

        GetButton((int)Buttons.Deposit_Btn).onClick.AddListener(OpenDepositWindow);
        GetButton((int)Buttons.Withdraw_Btn).onClick.AddListener(OpenWithdrawWindow);
        GetButton((int)Buttons.Remittance_Btn).onClick.AddListener(OpenRemittanceWindow);

        return true;
    }

    private void OpenDepositWindow()
    {        
        Managers.UI.ShowMenuUI<UI_Deposit>();
        Managers.UI.CloseUI(gameObject);
    }

    private void OpenWithdrawWindow()
    {        
        Managers.UI.ShowMenuUI<UI_Withdraw>();
        Managers.UI.CloseUI(gameObject);
    }

    private void OpenRemittanceWindow()
    {
        Managers.UI.ShowMenuUI<UI_Remittance>();
        Managers.UI.CloseUI(gameObject);
    }
}
