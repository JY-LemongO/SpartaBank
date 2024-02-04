using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AlertPopup : UI_Popup
{
    enum Texts
    {
        Alert_Text,
    }

    enum Buttons
    {
        OK_Btn,
    }    

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        Get<Button>((int)Buttons.OK_Btn).onClick.AddListener(CloseUI);
        Get<Text>((int)Texts.Alert_Text).text = _alert;

        return true;
    }

    private void CloseUI() => Managers.UI.CloseUI(gameObject);
}
