using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Login : UI_Base
{
    enum Buttons
    {
        Login_Btn,
        SignUp_Btn,
    }

    enum InputFields
    {
        ID_Input,
        PW_Input,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindInputField(typeof(InputFields));

        GetButton((int)Buttons.Login_Btn).onClick.AddListener(Login);
        GetButton((int)Buttons.SignUp_Btn).onClick.AddListener(SignUp);
        GetInputField((int)InputFields.ID_Input).ForceLabelUpdate();
        GetInputField((int)InputFields.PW_Input).ForceLabelUpdate();

        return true;
    }

    private void Login()
    {
        string id = Get<InputField>((int)InputFields.ID_Input).text;
        string pw = Get<InputField>((int)InputFields.PW_Input).text;

        Account loginAccount = Managers.AM.Login(id, pw);        
        if (loginAccount == null)
            return;

        Managers.BM.Init(loginAccount);

        Managers.UI.CloseUI(gameObject);
        Managers.UI.ShowSceneUI<UI_Main>();        
    }

    private void SignUp() => Managers.UI.ShowMenuUI<UI_SignUp>();
}
