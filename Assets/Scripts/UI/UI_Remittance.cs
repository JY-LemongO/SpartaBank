using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Remittance : UI_Base
{
    enum Buttons
    {
        ManualRemittance_Btn,
        BackToATM_Btn
    }

    enum InputFields
    {
        ToAccount_IF,
        DirectInput_IF,
    }

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindInputField(typeof(InputFields));
        
        GetButton((int)Buttons.ManualRemittance_Btn).onClick.AddListener(ManualRemittance);
        GetButton((int)Buttons.BackToATM_Btn).onClick.AddListener(BackToMain);

        return true;
    }

    private void BackToMain()
    {
        Managers.UI.CloseUI(gameObject);
        Managers.UI.ShowMenuUI<UI_MainATM>();
    }

    private void ManualRemittance()
    {
        InputField moneyField = GetInputField((int)InputFields.DirectInput_IF);
        InputField accountField = GetInputField((int)InputFields.ToAccount_IF);

        if (int.TryParse(moneyField.text, out int value))
        {
            Account account = Managers.AM.SearchAccount(accountField.text);
            if (account == null)
                Managers.UI.ShowPopupUI<UI_AlertPopup>("일치하는 계정이 없습니다.");

            Managers.BM.Remittance(value, account);
        }            
        else
            Managers.UI.ShowPopupUI<UI_AlertPopup>("송금할 금액을 입력하세요.");
    }
}
