using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankManager
{
    public int Cash     { get; private set; }
    public int Balance  { get; private set; }

    public Action<int, int> OnTransaction;

    public void Init(int initCash, int initBalance)
    {
        Cash    = initCash;
        Balance = initBalance;
    }

    public void Deposit(int value)
    {
        if (Cash < value || value < 0)
        {
            Managers.UI.ShowUI<UI_NotEnoughCashOrBalancePopup>(Util.PATH);
            return;
        }            

        Balance += value;
        Cash    -= value;

        OnTransaction?.Invoke(Cash, Balance);
    }

    public void Withdraw(int value)
    {
        if (Balance < value || value < 0)
        {
            Managers.UI.ShowUI<UI_NotEnoughCashOrBalancePopup>(Util.PATH);
            return;
        }            

        Balance -= value;
        Cash    += value;

        OnTransaction?.Invoke(Cash, Balance);
    }
}
