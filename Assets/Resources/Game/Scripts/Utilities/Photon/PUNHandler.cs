using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Threading.Tasks;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using System.Linq;
using DimensionalDeveloper.TankBuilder.Controllers;
using PlayFab.ClientModels;

public enum SceneState
{
    Menu = 0,
    Game = 1
}

public class PUNHandler : MonoBehaviourPunCallbacks
{
    public GameObject UIManager;
    public GameObject PlayerPrefabs;
    public GameObject FireSpawnerPrefabs;
    public GameObject Player1Spawn;
    public GameObject Player2Spawn;
    public GameObject LoadingScreen;
    public Slider LoadingSlider;
    public PhotonView PlayerPhotonView;
    public bool OnCount;
    public bool OnMatchmaking;
    public bool OnReady;
    public bool OnPreparation;
    public bool OnPlay;
    public int NextTeam;
    public int WaitDuration;
    public byte RoomMaxPlayer;
    public string RoomName;
    public static PUNHandler Instance;
    private AuthenticationValues AuthenticationValues;
    private RoomOptions RoomOptions;
    private void Awake() 
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        UIManager = GameObject.Find("UIManager");
        DontDestroyOnLoad(gameObject);
    }
    private void Start() 
    {
        AuthenticationValues = new AuthenticationValues();
        RoomOptions = new RoomOptions();
        RoomOptions.MaxPlayers = RoomMaxPlayer;
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    # region Client API
    public async void ConnectUsingSettings() 
    {
        Debug.Log("Connecting To Pun");
        
        // Inisialisasi nilai otentikasi menggunakan ID user dan token yang sudah didapatkan 
        AuthenticationValues.AuthType = CustomAuthenticationType.Custom;
        AuthenticationValues.AddAuthParameter("username", PlayerPrefs.GetString("UserID"));
        AuthenticationValues.AddAuthParameter("token", PlayerPrefs.GetString("UserToken"));

        // Masukan nilai otentikasi ke dalam API dan sambungkan koneksi
        PhotonNetwork.AuthValues = AuthenticationValues;
        PhotonNetwork.ConnectUsingSettings();

        // Tunggu user masuk ke dalam lobi lalu lakukan tugas berikutnya
        while (!PhotonNetwork.InLobby)
            await Task.Yield();
    }
    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }
    public void JoinRandomOrCreateRoom() 
    {
        // Tidak boleh menjalankan fungsi ketika:
        // 1. Pemain sudah dalam ruangan
        // 2. Belum memasuki lobi
        // 3. Belum dalam proses persiapan permainan
        if (PhotonNetwork.InRoom && !PhotonNetwork.InLobby && !OnPreparation)
            return;

        // Memasuki ruangan yang tersedia atau buat baru jika ruangan tidak tersedia
        PhotonNetwork.JoinRandomOrCreateRoom(roomName: System.Guid.NewGuid().ToString(), roomOptions: RoomOptions);
    }
    public async void WaitGame() 
    {
        // Inisial pengukur waktu
        float minutes = 0;
        float seconds = 0;

        // Inisial kondisi proses
        OnMatchmaking = true;
        OnCount = true;

        // Inisial UI pengukur waktu
        CustomEvent.Trigger(gameObject, "OnWaitGame", 0, 0);


        // Menunggu permainan selama kondisi berikut terpenuhi
        // 1. Total pemain dalam ruangan kurang dari maksimal pemain dalam room
        // 2. Dalam kondisi perhitungan waktu
        // 3. Pemain dalam ruangan
        while (PhotonNetwork.CurrentRoom.PlayerCount < RoomMaxPlayer && OnCount && PhotonNetwork.InRoom) {
            seconds += Time.deltaTime;
            if (seconds > 59.99) {
                minutes++;
                seconds = 0;
            }

            // Perubahan UI pengukur waktu
            CustomEvent.Trigger(gameObject, "OnWaitGame", minutes, seconds);
            await Task.Yield();
        }

        // Menghentikan kondisi perhitungan waktu
        OnCount = false;

        // Menjalankan fungsi selanjutnya jika kondisi terpenuhi
        if (PhotonNetwork.CurrentRoom.PlayerCount == RoomMaxPlayer)
            CustomEvent.Trigger(gameObject, "MatchIsReady");

        // Menunggu sampai kondisi persiapan permainan
        while (!OnPreparation && PhotonNetwork.InRoom)
            await Task.Yield();

        // Menghentikan kondisi pencarian lawan
        if (OnPreparation || !PhotonNetwork.InRoom)
            OnMatchmaking = false;
    }
    public void LeaveRoom() 
    {
        // Memastikan pemain dalam ruangan
        if (!PhotonNetwork.InRoom) return;

        // Mengembalikan nilai kondisi seperti semula
        OnCount = false;
        OnMatchmaking = false;
        OnReady = false;
        OnPreparation = false;
        OnPlay = false;
        RoomName = "";

        // Keluar dari ruangan
        PhotonNetwork.LeaveRoom();
    }
    
    public void PlayerReady() 
    {
        if (OnReady) return;
        OnReady = true;

        // Mengubah informasi pemain dalam ruangan
        var CustomProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        CustomProperties["Ready"] = true;
        PhotonNetwork.LocalPlayer.SetCustomProperties(CustomProperties);
    }
    public async void WaitPlayerResponse() 
    {
        // Inisial timer, kondisi, dan properti pemain
        float time = 0;
        OnReady = false;
        var CustomProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        CustomProperties["Ready"] = false;
        CustomProperties["Preparation"] = false;
        CustomProperties["Result"] = "";
        PhotonNetwork.SetPlayerCustomProperties(CustomProperties);
        var opponentCount = PhotonNetwork.CurrentRoom.PlayerCount;
        // Menunggu sampai pemain siap
        while (PhotonNetwork.InRoom && time < WaitDuration && !OnReady && opponentCount == RoomMaxPlayer) {
            time += Time.deltaTime;
            if (CustomProperties.ContainsKey("Ready") && CustomProperties.ContainsValue(true))
                OnReady = (bool) CustomProperties["Ready"];
            await Task.Yield();
        }

        // Melakukan fungsi lainnya
        if (OnReady)
            CustomEvent.Trigger(gameObject, "PlayerIsReady");
        else if (PhotonNetwork.CurrentRoom.PlayerCount < RoomMaxPlayer)
            CustomEvent.Trigger(gameObject, "OnOpponentLeftMatchmaking");
        else
            CustomEvent.Trigger(gameObject, "StopMatchmaking");
        
    }
    public void CheckOpponentResponse() 
    {
        // Mengambil data semua pemain dalam room
        var playerList = PhotonNetwork.PlayerList;

        // Cek kesiapan pemain dan lakukan fungsi lainnya
        if (playerList.All(p => p.CustomProperties.ContainsKey("Ready") && (bool) p.CustomProperties["Ready"]))
        {
            CustomEvent.Trigger(gameObject, "StartGame");
        }
            
    }
    public async void LoadScene(SceneState scene)
    {
        LoadingScreen.SetActive(true);

        // Memuat halaman dengan index yang ditentukan
        PhotonNetwork.LoadLevel((int)scene);

        // Menunggu progres memuat halaman
        float progress = 0;
        while (progress < .9f)
        {
            progress = PhotonNetwork.LevelLoadingProgress / .9f;
            LoadingSlider.value = progress;
            await Task.Yield();
        }
        // Setelah selesai, lakukan proses lainnya
        CustomEvent.Trigger(gameObject, "OnLoadSceneFinish");
    }
    public async void WaitInGame()
    {
        if (!OnReady) return;
        OnReady = false;
        // Inisialisasi kondisi dan data pemain dalam room (CustomProperties)
        OnMatchmaking = false;
        var CustomProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        CustomProperties["Preparation"] = OnPreparation = true;
        PhotonNetwork.SetPlayerCustomProperties(CustomProperties);
        var playerList = PhotonNetwork.PlayerList;
        bool allPlayerInGame = false;
        float currentTime = 0f;

        // Menunggu selama kondisi berikut terpenuhi:
        // 1. Dalam persiapan permainan
        // 2. Salah satu pemain belum dalam permainan
        // 3. Lama masuk permainan melebihi waktu yang ditetapkan
        while (OnPreparation && !allPlayerInGame && currentTime < WaitDuration)
        {
            // Melakukan perhitungan waktu masu permainan dan memeriksa apakah pemain sedang dalam persiapan permainan
            currentTime += Time.deltaTime;
            allPlayerInGame = playerList.All(p => p.CustomProperties.ContainsKey("Preparation") && (bool) p.CustomProperties["Preparation"]);
            await Task.Yield();
        }
        CustomEvent.Trigger(gameObject, "OnWaitInGameFinish");
    }

    public async void InstantiatePlayer()
    {
        
        if (!OnPreparation) return;
        OnPreparation = false;
        // Inisialisasi kondisi dan data pemain
        var CustomProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        CustomProperties["Preparation"] = OnPreparation = false;
        OnPlay = true;

        // Menemukan letak munculnya pemain
        while (Player1Spawn == null || Player2Spawn == null)
        {
            Player1Spawn = GameObject.Find("Player 1 Spawn");
            Player2Spawn = GameObject.Find("Player 2 Spawn");
            await Task.Yield();
        }

        // Membuat objek pemain pada letak yang diinginkan
        GameObject player = null;
        if (PhotonNetwork.IsMasterClient)
        {
            player = PhotonNetwork.Instantiate(
                $"Game/Prefabs/{PlayerPrefabs.name}", 
                Player1Spawn.transform.position, 
                Quaternion.identity
            );
        }
        else
        {
            player = PhotonNetwork.Instantiate(
                $"Game/Prefabs/{PlayerPrefabs.name}", 
                Player2Spawn.transform.position, 
                Quaternion.identity
            );
        }
    }
    
    
    # endregion

    # region Callback API
    public override void OnCreatedRoom()
    {
        Debug.Log("You created the room");
    }
    public override void OnConnectedToMaster() 
    {
        Debug.Log("On Connected to Master Server");
        CustomEvent.Trigger(gameObject, "OnConnectedToMaster");
    }
    public override void OnJoinedLobby() 
    {
        Debug.Log("On Joined Lobby Network");
        CustomEvent.Trigger(gameObject, "OnJoinedLobby");
    }
    public override void OnJoinedRoom() 
    {
        var player = PhotonNetwork.PlayerList;
        if (player.Length == 1)
        {
            Debug.Log("You created the room");
        }
        else
        {
            Debug.Log("You joined the room");
        }
        // Menyimpan nama ruangan (opsional)
        RoomName = PhotonNetwork.CurrentRoom.Name;

        CustomEvent.Trigger(gameObject, "OnJoinedRoom");
    }
    public override void OnLeftRoom() 
    {
        Debug.Log("On Left Room");
        if (OnPlay)
        {
            CustomEvent.Trigger(gameObject, "OnSurrender");
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected: {cause}");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer) 
    {
        Debug.Log("New player joined the room");
        Debug.Log("List player: ");
        var players = PhotonNetwork.PlayerList;
        foreach (var player in players)
        {
            Debug.Log(player);
        }

    }
    public override void OnPlayerLeftRoom(Player otherPlayer) 
    {
        Debug.Log($"{otherPlayer.NickName} left the room");
        if (OnMatchmaking)
        {
            Debug.Log("List player: ");
            var players = PhotonNetwork.PlayerList;
            foreach (var player in players)
            {
                Debug.Log(player);
            }
            // Kembali menunggu pertandingan
            CustomEvent.Trigger(gameObject, "OnOpponentLeftMatch");
        }
        if (OnPlay)
        {
            // Pemain menang saat lawan keluar dari ruangan
            CustomEvent.Trigger(gameObject, "OnOtherPlayerLeftMatch");
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (OnMatchmaking)
        {
            if (changedProps.ContainsKey("Ready") && changedProps.ContainsValue(true))
            {
                Debug.Log($"{targetPlayer.NickName} is ready");
            }
            CustomEvent.Trigger(gameObject, "OnMatchmakingPropertiesUpdate");
        }
        if (OnPreparation)
        {
            if (changedProps.ContainsKey("Preparation") && changedProps.ContainsValue(true))
            {
                Debug.Log($"{targetPlayer.NickName} join game");
            }
            CustomEvent.Trigger(gameObject, "InGamePropertiesUpdate");
        }
        
    }

    # endregion


}
