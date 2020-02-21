using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public string UserID { get; private set; }
    public string UserPassword;
    public string UserName;
    public string level;
    public string Coins;

    public void SetCredential(string username, string userpassword)
    {
        UserName = username;
        UserPassword = userpassword;
    }
    public void SetID(string id)
    {
        UserID = id;
    }
}
