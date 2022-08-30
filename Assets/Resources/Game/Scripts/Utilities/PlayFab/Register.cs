using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Unity.VisualScripting;
public class Register : PlayFabManager
{
    RegisterPlayFabUserRequest registerRequest;
    UpdateUserTitleDisplayNameRequest nameRequest;
    ExecuteCloudScriptRequest executeRequest;
    void Awake() {
        registerRequest = new RegisterPlayFabUserRequest {
            TitleId = gameId
        };
        nameRequest = new UpdateUserTitleDisplayNameRequest();
        executeRequest = new ExecuteCloudScriptRequest();
        executeRequest.FunctionName = "initPlayerStats";
    }
    public void AuthRegister(string username, string email, string password) {
        // Mendaftarkan pemain melalui PlayFab
        registerRequest.Username = username;
        registerRequest.Email = email;
        registerRequest.Password = password;
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest,
            // Panggilan balik ketika pendaftaran berhasil
            registerResult =>
            {
                Log = "User registered";

                // Membuat nama pemain
                nameRequest.DisplayName = username;
                PlayFabClientAPI.UpdateUserTitleDisplayName(nameRequest,
                nameResult =>
                {
                    Debug.Log("Display name updated");

                // Mengeksekusi cloud script untuk menginisialisasi data statistik pemain
                PlayFabClientAPI.ExecuteCloudScript(executeRequest,

                    // Panggilan balik ketika eksekusi berhasil
                    executeResult =>
                    {
                        Debug.Log("Player status initialized");
                    },
                    // Panggilan balik ketika eksekusi gagal 
                    OnError);
                },
                // Panggilan balik ketika gagal membuat nama pemain
                OnError);

                CustomEvent.Trigger(gameObject, "OnRegisterSuccess");
            },
        // Panggilan balik ketika pendaftaran gagal
        OnError);
    }
    public bool Validate(string username, string email, string password, string confirm){
        if (!EmailValidation(email)) return false;
        if (!UsernameValidation(username)) return false;
        if (!PasswordValidation(password, confirm)) return false;
        return true;
    }
    void OnError(PlayFabError error) {
        switch (error.Error) {
            case PlayFabErrorCode.EmailAddressNotAvailable:
                Log = "Email address is not available";
                break;
            case PlayFabErrorCode.UsernameNotAvailable:
                Log = "Username is not available";
                break;
            default:
                Log = error.GenerateErrorReport();
                break;
        }
        CustomEvent.Trigger(gameObject, "OnRegisterError");
    }
}
