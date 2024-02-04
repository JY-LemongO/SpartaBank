using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    protected string _alert;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;        

        return true;
    }

    public void SetupAlert(string alert) => _alert = alert;
}
