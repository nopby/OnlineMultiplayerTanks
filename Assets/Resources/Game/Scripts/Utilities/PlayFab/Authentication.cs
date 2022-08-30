using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using Unity.VisualScripting;
using Photon.Pun;
using System.Collections.Generic;

// Sebagai penanda data statistik yang dimiliki
public enum Statistic
{
    Match,
    Win,
    WinRate
}

// Kelas ini digunakan untuk otentikasi login dengan PlayFab sekaligus pemegang tiket sesi
public class Authentication : PlayFabManager
{
    // Deklarasi variabel yang dibutuhkan
    
    // Variabel sebagai referensi objek UI Manager yang akan digunakan
    // oleh visual scripting dalam menangani interaksi antarmuka pengguna.
    // Variabel ini juga diinisialisasi oleh kelas UI Manager itu sendiri
    // karena kelas ini tidak akan menjalankan fungsi Awake ataupun Start
    // untuk kedua kalinya setelah kelas ini menjadi singleton.
    public GameObject UIManager;
    public string DisplayName;
    LoginWithPlayFabRequest loginRequest;
    GetPhotonAuthenticationTokenRequest tokenRequest;
    PlayerProfileViewConstraints constraints;
    GetPlayerCombinedInfoRequestParams requestParams;
    public bool Authenticated;
    void Start() {
        // Pembuatan objek yang dibutuhkan dalam otentikasi login atau masuk
        constraints = new PlayerProfileViewConstraints() {
            ShowDisplayName = true
        };
        requestParams = new GetPlayerCombinedInfoRequestParams() {
            GetPlayerStatistics = true,
            GetPlayerProfile = true,
            ProfileConstraints = constraints
        };
        loginRequest = new LoginWithPlayFabRequest() {
            TitleId = gameId,
            InfoRequestParameters = requestParams
        };
        tokenRequest = new GetPhotonAuthenticationTokenRequest() {
            PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime
        };
    }

    public bool Validate(string username, string password) {
        if (!UsernameValidation(username)) return false;
        if (!PasswordValidation(password)) return false;
        return true;
    }
    public void AuthLogin(string username, string password) {
        // Otentikasi login atau masuk melalui PlayFab
        loginRequest.Username = username;
        loginRequest.Password = password;
        PlayFabClientAPI.LoginWithPlayFab(
            loginRequest, 
            loginResult => {
                Debug.Log("Authenticated with PlayFab");
                DisplayName = loginResult.InfoResultPayload.PlayerProfile.DisplayName;
                PlayerPrefs.SetString("UserID", loginResult.PlayFabId);
                PlayerPrefs.SetString("Nickname", DisplayName);
                Debug.Log($"ID: {loginResult.PlayFabId}");
                Debug.Log($"Session Tiket: {loginResult.SessionTicket}");

                // Otentikasi permintaan token dengan Photon
                PlayFabClientAPI.GetPhotonAuthenticationToken(tokenRequest, tokenResult => {
                    PlayerPrefs.SetString("UserToken", tokenResult.PhotonCustomAuthenticationToken);
                    Authenticated = true;
                    Debug.Log($"Token: {tokenResult.PhotonCustomAuthenticationToken}");
                    CustomEvent.Trigger(gameObject, "OnLoginSuccess");
                },
                // Panggilan balik saat terjadi kegagalan otentikasi permintaan token dengan Photon
                OnError);
            },
            // Panggilan balik saat terjadi kegagalan otentikasi masuk dengan PlayFab
            OnError);
    }
    void OnError(PlayFabError error) {
        // Notifikasi jika ada error saat proses otentikasi
        switch (error.Error) {
            case PlayFabErrorCode.AccountNotFound:
                Log = "Account Not Found";
                break;
            case PlayFabErrorCode.AccountAlreadyLinked:
                Log = "Account Already Linked";
                break;
            case PlayFabErrorCode.InvalidUsernameOrPassword:
                Log = "Invalid Username Or Password";
                break;
            case PlayFabErrorCode.NameNotAvailable:
                Log = "Name Not Available";
                break;
            default:
                Log = error.GenerateErrorReport();
                break;
        }
        CustomEvent.Trigger(gameObject, "OnLoginError", Log);
    }
    public void Logout()
    {
        // Membersihkan semua objek otentikasi ketika keluar dari akun
        PlayFabClientAPI.ForgetAllCredentials();
        PlayerPrefs.DeleteAll();
        Authenticated = false;
        DisplayName = "";
        Log = "";
        Error = false;
    }
    public void UpdateMatchTotal()
    {
        // Memperbarui jumlah pertandingan
        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest()
            {
                Statistics = new List<StatisticUpdate>()
                {
                    new StatisticUpdate()
                    {
                        StatisticName = Statistic.Match.ToString(),
                        Value = 1
                    }
                }
            },
            // Panggilan balik ketika berhasil memperbarui jumlah pertandingan
            result => {
                Debug.Log("Update match statistic");
            },
            // Panggilan balik ketika gagal memperbarui jumlah pertandingan
            OnError
        );
    }
    public void UpdateWinTotal()
    {
        // Memperbarui jumlah kemenangan
        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest()
            {
                Statistics = new List<StatisticUpdate>()
                {
                    new StatisticUpdate()
                    {
                        StatisticName = Statistic.Win.ToString(),
                        Value = 1
                    }
                }
            },
            // Panggilan balik ketika berhasil memperbarui jumlah kemenangan
            result => {
                UpdateWinRateTotal();
            },
            // Panggilan balik ketika gagal memperbarui jumlah kemenangan
            OnError
        );
    }
    public void UpdateWinRateTotal()
    {
        // Mendapatkan jumlah kemenangan
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest()
            {
                StatisticNames = new List<string>(){
                    Statistic.Win.ToString(),
                    Statistic.Match.ToString()
                }
            },
            // Panggilan balik ketika berhasil mendapatkan jumlah kemenangan
            statisticResult =>
            {
                // Memperbarui jumlah rata-rata kemenangan
                float totalMatch = statisticResult.Statistics[1].Value;
                float totalWin = statisticResult.Statistics[0].Value;
                float winrate = totalWin / totalMatch * 100;
                PlayFabClientAPI.UpdatePlayerStatistics(
                    new UpdatePlayerStatisticsRequest()
                    {
                        Statistics = new List<StatisticUpdate>()
                        {
                            new StatisticUpdate()
                            {
                                StatisticName = Statistic.WinRate.ToString(),
                                Value = ((int)winrate)
                            }
                     }
                },
            // Panggilan balik ketika berhasil memperbarui jumlah rata-rata kemenangan
            result =>
            {
                Debug.Log("Update win rate statistic");
            },
            // Panggilan balik ketika gagal memperbarui jumlah rata-rata kemenangan
            OnError
        );
            },
            // Panggilan balik ketika gagal mendapatkan jumlah kemenangan
            error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            }
        );
    }
}
