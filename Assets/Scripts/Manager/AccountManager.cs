using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Account
{
    public string id;
    public string name;
    public string pw;
    public int cash;
    public int balance;
}

[System.Serializable]
public class AccountDB
{
    public List<Account> _saveAccounts = new List<Account>();
}

public class AccountManager
{
    private Dictionary<string, Account> _accounts = new Dictionary<string, Account>();        
    private AccountDB _db = new AccountDB();

    private readonly int InitCash = 100000;
    private readonly int InitBalance = 50000;

    private const string SAVE_NAME = "Accounts";

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

    public Account SearchAccount(string id)
    {
        Account account = null;
        if(_accounts.TryGetValue(id, out account))
        {
            return account;
        }
        return null;
    }

    public void SaveAccounts(Account account)
    {
        _db._saveAccounts.Add(account);
        string data = JsonUtility.ToJson(_db);        

        PlayerPrefs.SetString(SAVE_NAME, data);
        PlayerPrefs.Save();
    }

    public void SaveAccounts()
    {
        string data = JsonUtility.ToJson(_db);

        PlayerPrefs.SetString(SAVE_NAME, data);
        PlayerPrefs.Save();
    }

    public void LoadAllAccounts()
    {
        if (PlayerPrefs.HasKey(SAVE_NAME))
        {
            string loadedData = PlayerPrefs.GetString(SAVE_NAME);
            _db = JsonUtility.FromJson<AccountDB>(loadedData);   

            foreach(var data in _db._saveAccounts)
                _accounts.Add(data.id, data);
        }
        else
        {
            Debug.Log("저장된 정보가 없습니다.");
        }
    }

    public bool CheckDuplicateID(string id)
    {
        if (_accounts.ContainsKey(id))
            return true;

        return false;
    }
}
