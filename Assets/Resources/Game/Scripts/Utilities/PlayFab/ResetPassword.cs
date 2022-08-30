using PlayFab.ClientModels;
using PlayFab;
using Unity.VisualScripting;
public class ResetPassword : PlayFabManager
{
    SendAccountRecoveryEmailRequest resetRequest;
    void Awake() {
        resetRequest = new SendAccountRecoveryEmailRequest {
            TitleId = base.gameId
        };
    }
    public void AuthResetPassword(string emailConfirmation) {
        resetRequest.Email = emailConfirmation;

        // Mengirim link perubahan kata sandi ke email pengguna
        PlayFabClientAPI.SendAccountRecoveryEmail(resetRequest,

        // Panggilan balik ketika berhasil mengirim link perubahan kata sandi
        resetResult =>
        {
            Log = "Link has been sent to your email";
            CustomEvent.Trigger(gameObject, "OnResetPasswordSuccess");
        },
        // Panggilan balik ketika gagal mengirim link perubahan kata sandi
        OnError);
    }
    public bool Validate(string emailConfirmation) {
        if (!EmailValidation(emailConfirmation)) return false;
        return true;
    }
    void OnError(PlayFabError error) {
        switch (error.Error) {
            case PlayFabErrorCode.EmailAddressNotAvailable:
                Log = "Email address is not available";
                break;
            case PlayFabErrorCode.InvalidEmailAddress:
                Log = "Invalid email address";
                break;
            case PlayFabErrorCode.InvalidParams:
                Log = "Invalid input parameters";
                break;
            default:
                Log = error.GenerateErrorReport();
                break;
        }
        CustomEvent.Trigger(gameObject, "OnResetPasswordError");
    }
}
