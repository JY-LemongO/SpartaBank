using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankManager
{
    public Account CurrentAccount { get; private set; }    

    public Action<int, int> OnTransaction;

    public void Init(Account account)
    {        
        CurrentAccount = account;        
    }

    public void Deposit(int value)
    {
        if (CurrentAccount.cash < value || value < 0)
        {
            Managers.UI.ShowPopupUI<UI_AlertPopup>("잔액이 모자랍니다");
            return;
        }

        CurrentAccount.balance += value;
        CurrentAccount.cash -= value;

        OnTransaction?.Invoke(CurrentAccount.cash, CurrentAccount.balance);
        Managers.AM.SaveCurrentAccount();
    }

    public void Withdraw(int value)
    {
        if (CurrentAccount.balance < value || value < 0)
        {
            Managers.UI.ShowPopupUI<UI_AlertPopup>("잔액이 모자랍니다");
            return;
        }

        CurrentAccount.balance -= value;
        CurrentAccount.cash += value;

        OnTransaction?.Invoke(CurrentAccount.cash, CurrentAccount.balance);
        Managers.AM.SaveCurrentAccount();
    }

    public void Logout()
    {
        if (CurrentAccount != null)
            CurrentAccount = null;
    }
}
