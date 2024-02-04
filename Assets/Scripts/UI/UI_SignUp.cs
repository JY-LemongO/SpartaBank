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
            Managers.UI.ShowPopupUI<UI_AlertPopup>("������ Ȯ�����ּ���.");

            if (DuplicateID(inputTextID))
                alert.text = "�ߺ��� ID �Դϴ�.";
            else if (inputTextID.Length < MinLengthID || !Regex.IsMatch(inputTextID, "^[a-zA-Z0-9]+$"))
                alert.text = "ID�� ����, ���� ȥ�� 3 ~ 10�� �Դϴ�.";            
            else if (inputTextName.Length < MinLengthName)
                alert.text = "�̸��� Ȯ�����ּ���.";
            else if (inputTextPW.Length < MinLengthPW)
                alert.text = "PassWord�� Ȯ�����ּ���.";
            else if (inputTextPW != inputTextPWConfirm)
                alert.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";

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
