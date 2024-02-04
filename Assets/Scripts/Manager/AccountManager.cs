using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account
{
    public string id;
    public string name;
    public string pw;
    public int cash;
    public int balance;
}

public class AccountManager
{
    private Dictionary<string, Account> _accounts = new Dictionary<string, Account>();
    private List<Account> _saveAccount = new List<Account>();

    private readonly int InitCash = 100000;
    private readonly int InitBalance = 50000;

    public Account Login(string id, string pw)
    {
        if (!_accounts.ContainsKey(id))
        {
            Managers.UI.ShowPopupUI<UI_AlertPopup>("존재하지 않는 ID 입니다.");
            return null;
        }

        Account account = _accounts[id];
        if (account.pw != pw)
        {
            Managers.UI.ShowPopupUI<UI_AlertPopup>("비밀번호가 일치하지 않습니다.");
            return null;
        }
        
        return account;
    }

    public void SignUp(string id, string name, string pw)
    {
        Account account = new Account() { id = id, name = name, pw = pw, cash = InitCash, balance = InitBalance };
        _accounts.Add(id, account);
        SaveAccounts(account);

        Managers.UI.ShowPopupUI<UI_AlertPopup>("계정이 생성되었습니다.");
    }

    private void SaveAccounts(Account account)
    {
        _saveAccount.Add(account);

        string json = JsonUtility.ToJson(_saveAccount);
        PlayerPrefs.SetString("Accounts", json);
        PlayerPrefs.Save();
    }

    public void LoadAllAccounts()
    {
        string json = PlayerPrefs.GetString("Accounts");
        if (string.IsNullOrEmpty(json))
            return;

        _saveAccount = JsonUtility.FromJson<List<Account>>(json);

        foreach (Account account in _saveAccount)
            _accounts.Add(account.id, account);
    }

    public bool CheckDuplicateID(string id)
    {
        if (_accounts.ContainsKey(id))
            return true;

        return false;
    }
}
