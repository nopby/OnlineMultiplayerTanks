using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using ExitGames.Client.Photon;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class ChatHandler : MonoBehaviour, IChatClientListener
{
    public GameObject UIManager;
    public bool IsConnected;
    public string LobbyName;
    public string RoomName;
    List<string> Channel;
    public Dictionary<string, string> ChatMessage;
    public static ChatHandler Instance;
    public ChatClient ChatClient;
    AuthenticationValues AuthenticationValues;
    void Start() {
        ChatClient = new ChatClient(this);
        AuthenticationValues = new AuthenticationValues();
        ChatMessage = new Dictionary<string, string>() {
            { "Subject", "" },
            { "Body", "" },
            { "Date", "" },
        };
        Channel = new List<string> {
            LobbyName
        };
    }
    void Update() {
        // Menjaga koneksi ke aplikasi chat
        ChatClient.Service();
    }
    # region Client API
    public void Connect() {
        Debug.Log("Connecting to Chat");

        // Inisialisasi nilai otentikasi dengan ID user dan token
        AuthenticationValues.AuthType = CustomAuthenticationType.Custom;
        AuthenticationValues.AddAuthParameter("username", PlayerPrefs.GetString("UserID"));
        AuthenticationValues.AddAuthParameter("token", PlayerPrefs.GetString("UserToken"));

        // Sambungkan koneksi
        ChatClient.Connect(
            PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, 
            PhotonNetwork.AppVersion,
            AuthenticationValues
        );

    }
    public void Disconnect()
    {
        ChatClient.Disconnect();
    }
    public void PublishMessages(string messages) {
        Debug.Log($"Sending message: {messages}");
        ChatMessage["Subject"] = PlayerPrefs.GetString("Nickname");
        ChatMessage["Body"] = messages;
        ChatMessage["Date"] = System.DateTime.Now.ToString("h:mm tt");
        ChatClient.PublishMessage(LobbyName, ChatMessage);
    }
    # endregion

    # region Callback API
    public void OnConnected() {
        IsConnected = true;
        // Melakukan subscribe ke channel lobi
        // ChatClient.Subscribe(Channel.ToArray());
        CustomEvent.Trigger(gameObject, "OnConnected", ChatClient, Channel.ToArray());
    }

    public void OnDisconnected() {
        IsConnected = false;
        Debug.Log("Disconected from chat");
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages) {
        Debug.Log("You got new messages");
        var formatedMessages = messages.ToList();
        CustomEvent.Trigger(gameObject, "OnGetMessages", formatedMessages);
    }
    public void DebugReturn(DebugLevel level, string message) {
        
    }
    public void OnChatStateChange(ChatState state) {
        Debug.Log(state);
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log($"On Subscribed {channels[0]} Channel");

        // menjalankan visual scripting event
        CustomEvent.Trigger(gameObject, "OnSubscribed");
    }

    public void OnUnsubscribed(string[] channels)
    {
        
    }

    public void OnUserSubscribed(string channel, string user)
    {
        
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        
    }
    # endregion
    
}
