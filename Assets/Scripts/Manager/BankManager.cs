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
        Managers.UI.ShowPopupUI<UI_AlertPopup>($"{value}원을 입금했습니다.");
        Managers.AM.SaveAccounts();
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
        Managers.UI.ShowPopupUI<UI_AlertPopup>($"{value}원을 출금했습니다.");
        Managers.AM.SaveAccounts();
    }

    public void Remittance(int value, Account account)
    {
        if (CurrentAccount.balance < value || value < 0)
        {
            Managers.UI.ShowPopupUI<UI_AlertPopup>("잔액이 모자랍니다");
            return;
        }

        CurrentAccount.balance -= value;
        account.balance += value;
        

        OnTransaction?.Invoke(CurrentAccount.cash, CurrentAccount.balance);
        Managers.UI.ShowPopupUI<UI_AlertPopup>($"{value}원을 {account.id}님께 송금했습니다.");
        Managers.AM.SaveAccounts();
    }

    public void Logout()
    {
        if (CurrentAccount != null)
            CurrentAccount = null;
    }
}
