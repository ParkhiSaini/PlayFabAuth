using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class PlayFabManager : MonoBehaviour
{
    [Header ("UI")]
    public TextMeshProUGUI messageText;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    public void RegisterButton()
    {
        if (passwordInput.text.Length<6)
        {
            messageText.text="Password must be at least 6 characters long";
            return;
        }
        var request =new RegisterPlayFabUserRequest(){
            Email=emailInput.text,
            Password=passwordInput.text,
            RequireBothUsernameAndEmail=false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result){
        messageText.text = "Registered and logged in successfully";
    }

    public void LoginButton()
    {
        var request= new LoginWithEmailAddressRequest(){
            Email=emailInput.text,
            Password=passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request,OnLoginSuccess,OnError);
    }


    void OnLoginSuccess(LoginResult result){
        messageText.text ="Logged in successfully";
        Debug.Log("Logged in ");
        // GetCharacters();

    }

    public void ResetPasswordButton(){
        var request = new SendAccountRecoveryEmailRequest(){
            Email=emailInput.text,
            TitleId="3098E"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, onPasswordReset,OnError);
    }

    void onPasswordReset(SendAccountRecoveryEmailResult result){
        messageText.text="Password reset email sent";

    }

     void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;

      
        Debug.Log(error.GenerateErrorReport());
    }

    
}
