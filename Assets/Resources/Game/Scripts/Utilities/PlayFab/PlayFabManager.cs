using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using TMPro;
using Unity.VisualScripting;
public class PlayFabManager : MonoBehaviour
{
    protected string gameId { get { return PlayFabSettings.TitleId; }}
    public string Log;
    public bool Error;
    protected bool EmailValidation(string email) {
        if (email.Length < 4) {
            Log = "Email must have atleast 4 characters";
            return false;
        }
        if (!email.Contains("@")) {
            Log = "Email does not contain @";
            return false;
        }
        return true;
    }
    protected bool UsernameValidation(string username) {
        if (username.Length < 6) {
            Log = "Username must have atleast 6 characters";
            return false;
        }
        if (username.Length > 24) {
            Log = "Username cannot be more than 24 characters";
            return false;
        }
        return true;
    }
    protected bool PasswordValidation(string password, string confirmPassword) {
        if (password.Length < 8) {
            Log = "Password must have atleast 8 characters";
            return false;
        }
        if (password != confirmPassword) {
            Log = "Password do not match";
            return false;
        }
        return true;
    }
    protected bool PasswordValidation(string password) {
        if (password.Length < 8) {
            Log = "Password must have atleast 8 characters";
            return false;
        }
        return true;
    }
    
}
