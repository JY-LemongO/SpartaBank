using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Withdraw : UI_Base
{
    enum Buttons
    {
        K10_Btn,
        K30_Btn,
        K50_Btn,
        ManualWithdraw_Btn,
        BackToATM_Btn,
    }

    enum InputFields
    {
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

        GetButton((int)Buttons.K10_Btn).onClick.AddListener(() => Managers.BM.Withdraw(10000));
        GetButton((int)Buttons.K30_Btn).onClick.AddListener(() => Managers.BM.Withdraw(30000));
        GetButton((int)Buttons.K50_Btn).onClick.AddListener(() => Managers.BM.Withdraw(50000));
        GetButton((int)Buttons.ManualWithdraw_Btn).onClick.AddListener(ManualWithdraw);
        GetButton((int)Buttons.BackToATM_Btn).onClick.AddListener(BackToMain);
        GetInputField((int)InputFields.DirectInput_IF);

        return true;
    }

    private void BackToMain()
    {
        Managers.UI.ShowMenuUI<UI_MainATM>();
        Managers.UI.CloseUI(gameObject);
    }

    private void ManualWithdraw()
    {
        InputField inputField = GetInputField((int)InputFields.DirectInput_IF);
        if (int.TryParse(inputField.text, out int value))
            Managers.BM.Withdraw(value);
        else
            Managers.UI.ShowPopupUI<UI_AlertPopup>("출금할 금액을 입력하세요.");
    }
}
