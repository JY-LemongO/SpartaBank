using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class UI_SignUp : UI_Base
{
    enum Buttons
    {
        Cancel_Btn,
        SignUp_Btn,
    }

    enum InputFields
    {
        ID_IF,
        Name_IF,
        PW_IF,
        PWConfirm_IF,
    }

    enum Texts
    {
        Alert_Text,
    }

    private readonly int MinLengthID    = 3;
    private readonly int MinLengthName  = 2;
    private readonly int MinLengthPW    = 5;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));
        Bind<InputField>(typeof(InputFields));
        Bind<Text>(typeof(Texts));

        Get<Button>((int)Buttons.Cancel_Btn).onClick.AddListener(CloseUI);
        Get<Button>((int)Buttons.SignUp_Btn).onClick.AddListener(SignUp);

        return true;
    }

    private void CloseUI() => Managers.UI.CloseUI(gameObject);

    private void SignUp()
    {
        string inputTextID        = Get<InputField>((int)InputFields.ID_IF).text;
        string inputTextName      = Get<InputField>((int)InputFields.Name_IF).text;
        string inputTextPW        = Get<InputField>((int)InputFields.PW_IF).text;
        string inputTextPWConfirm = Get<InputField>((int)InputFields.PWConfirm_IF).text;

        Text alert = Get<Text>((int)Texts.Alert_Text);

        if(!CheckInfo(inputTextID, inputTextName, inputTextPW, inputTextPWConfirm) || DuplicateID(inputTextID))
        {
            Managers.UI.ShowPopupUI<UI_AlertPopup>("정보를 확인해주세요.");

            if (DuplicateID(inputTextID))
                alert.text = "중복된 ID 입니다.";
            else if (inputTextID.Length < MinLengthID || !Regex.IsMatch(inputTextID, "^[a-zA-Z0-9]+$"))
                alert.text = "ID는 영문, 숫자 혼합 3 ~ 10자 입니다.";            
            else if (inputTextName.Length < MinLengthName)
                alert.text = "이름을 확인해주세요.";
            else if (inputTextPW.Length < MinLengthPW)
                alert.text = "PassWord를 확인해주세요.";
            else if (inputTextPW != inputTextPWConfirm)
                alert.text = "비밀번호가 일치하지 않습니다.";

            return;
        }

        Managers.UI.CloseUI(gameObject);        
        Managers.AM.SignUp(inputTextID, inputTextName, inputTextPW);
    }

    private bool CheckInfo(string id, string name, string pw, string pwConfirm)
    {
        if (id.Length < MinLengthID || !Regex.IsMatch(id, "^[a-zA-Z0-9]+$") || name.Length < MinLengthName || pw.Length < MinLengthPW || pw != pwConfirm)
            return false;

        return true;
    }

    private bool DuplicateID(string id) => Managers.AM.CheckDuplicateID(id);    
}
