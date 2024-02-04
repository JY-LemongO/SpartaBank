using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_NotEnoughCashOrBalancePopup : UI_Base
{
    enum Buttons
    {
        OK_Btn,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.OK_Btn).onClick.AddListener(CloseUI);

        return true;
    }

    private void CloseUI() => Managers.UI.CloseUI(gameObject);
}
