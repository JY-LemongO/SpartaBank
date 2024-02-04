using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Deposit : UI_Base
{
    enum Buttons
    {
        K10_Btn,
        K30_Btn,
        K50_Btn,
        ManualDeposit_Btn,
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

        GetButton((int)Buttons.K10_Btn).onClick.AddListener(() => Managers.BM.Deposit(10000));
        GetButton((int)Buttons.K30_Btn).onClick.AddListener(() => Managers.BM.Deposit(30000)); 
        GetButton((int)Buttons.K50_Btn).onClick.AddListener(() => Managers.BM.Deposit(50000));
        GetButton((int)Buttons.ManualDeposit_Btn).onClick.AddListener(ManualDeposit);
        GetButton((int)Buttons.BackToATM_Btn).onClick.AddListener(BackToMain);        

        return true;
    }

    private void BackToMain()
    {
        Managers.UI.CloseUI(gameObject);
        Managers.UI.ShowMenuUI<UI_MainATM>();        
    }

    private void ManualDeposit()
    {
        InputField inputField = GetInputField((int)InputFields.DirectInput_IF);
        if (int.TryParse(inputField.text, out int value))
            Managers.BM.Deposit(value);
        else
            Debug.Log("입력 값에 숫자가 포함되어 있지 않습니다.");
    }
}
