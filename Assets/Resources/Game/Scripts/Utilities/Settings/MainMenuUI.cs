using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.UI;
using Photon.Pun;
using System.Threading.Tasks;
using Unity.VisualScripting;
public class MainMenuUI : MonoBehaviour
{
    [Header("Input Manager")]
    public string InputManagerObjName = "Input Manager";
    private InputManager inputManager;
    [Header("Login UI")]
    public string LoginHandlerObjName = "Login Handler";
    public TMP_InputField LoginUsername;
    public TMP_InputField LoginPassword;
    public string LoginRawUsername
    {
        get
        {
            return LoginUsername.text;
        }
    }
    public string LoginRawPassword
    {
        get
        {
            return LoginPassword.text;
        }
    }
    [Header("Register UI")]
    public TMP_InputField RegisterEmail;
    public TMP_InputField RegisterUsername;
    public TMP_InputField RegisterPassword;
    public TMP_InputField RegisterPasswordConfirmation;
    public string RegisterRawEmail
    {
        get
        {
            return RegisterEmail.text;
        }
    }
    public string RegisterRawUsername
    {
        get
        {
            return RegisterUsername.text;
        }
    }
    public string RegisterRawPassword
    {
        get
        {
            return RegisterPassword.text;
        }
    }
    public string RegisterRawPasswordConfirmation
    {
        get
        {
            return RegisterPasswordConfirmation.text;
        }
    }
    [Header("Reset Password UI")]
    public TMP_InputField ResetPasswordEmail;
    public string ResetPasswordRawEmail
    {
        get
        {
            return ResetPasswordEmail.text;
        }
    }
    [Header("Messages UI")]
    public TextMeshProUGUI MessagesUI;
    [Header("Profile UI")]
    public TextMeshProUGUI ProfileName;
    public TextMeshProUGUI ProfileStats;
    public TextMeshProUGUI ProfileStatsValue;
    [Header("Leaderboard UI")]
    public Transform LeaderboardContent;
    public GameObject LeaderboardListPrefabs;
    public List<GameObject> LeaderboardList;
    private GetPlayerStatisticsRequest StatisticsRequest;
    private GetLeaderboardRequest LeaderboardRequest;
    [Header("Chat UI")]
    public Transform ChatContent;
    public GameObject ChatPrefabs;
    public TMP_InputField ChatInput;
    public Queue<GameObject> ChatQueue;
    public string ChatRawText
    {
        get
        {
            return ChatInput.text;
        }
    }
    [Header("Find Match UI")]
    public TextMeshProUGUI FindMatchTimer;
    [Header("Load Scene UI")]
    public Slider LoadingSlider;
    public GameObject LoadingScreen;
    [Header("Photon Handler")]
    public string ChatHandlerObjName = "Photon Chat";
    public string RealtimeHandlerObjName = "Photon Realtime";
    private void Awake()
    {
        StatisticsRequest = new GetPlayerStatisticsRequest();
        LeaderboardRequest = new GetLeaderboardRequest();
        LeaderboardRequest.StartPosition = 0;
        LeaderboardRequest.MaxResultsCount = 10;
        LeaderboardRequest.StatisticName = "WinRate";
        LeaderboardList = new List<GameObject>();
        ChatQueue = new Queue<GameObject>();
    }
    public void SetMessages(string log)
    {
        MessagesUI.text = log;
    }
    public void InitializeSingletonUIObject()
    {
        PUNHandler.Instance.UIManager = gameObject;
        PUNHandler.Instance.LoadingSlider = LoadingSlider;
        PUNHandler.Instance.LoadingScreen = LoadingScreen;
    }
    public void InitializePlayerProfile()
    {
        Debug.Log("Initialize player profile");
        ProfileName.text = PlayerPrefs.GetString("Nickname");
        PlayFabClientAPI.GetPlayerStatistics(
            StatisticsRequest,
            statisticResult =>
            {
                string statsKey = "";
                string statsVal = "";
                foreach (var eachStat in statisticResult.Statistics)
                {
                    PlayerPrefs.SetInt(eachStat.StatisticName, eachStat.Value);
                    statsKey += $"{eachStat.StatisticName}\n";
                    if (eachStat.StatisticName == "WinRate")
                        statsVal += $"{eachStat.Value}%\n";
                    else
                        statsVal += $"{eachStat.Value}\n";
                }
                ProfileStats.text = statsKey;
                ProfileStatsValue.text = statsVal;
            },
            error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            }
        );
    }
    public void ClearLeaderboard()
    {
        foreach (var item in LeaderboardList)
        {
            Destroy(item);
        }
        LeaderboardList.Clear();
    }
    public void InitializeLeaderboard()
    {
        Debug.Log("Initialize leaderboard");
        PlayFabClientAPI.GetLeaderboard(LeaderboardRequest,
        leaderboardResult =>
        {
            foreach (var eachPlayer in leaderboardResult.Leaderboard)
            {
                var name = LeaderboardListPrefabs.GetComponentsInChildren<TextMeshProUGUI>();
                name[0].text = (eachPlayer.Position + 1).ToString();
                name[1].text = eachPlayer.DisplayName;
                var item = Instantiate(LeaderboardListPrefabs, LeaderboardContent);
                LeaderboardList.Add(item);
            }
        },
        error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }
    public void ClearChat()
    {
        foreach (var item in ChatQueue)
        {
            Destroy(item);
        }
        ChatQueue.Clear();
    }
    public void InitializeNewChatMessage(List<Dictionary<string, string>> messageObj)
    {
        Debug.Log("Initialize new chat messages");
        foreach (var message in messageObj)
        {
            var text = ChatPrefabs.GetComponentsInChildren<TextMeshProUGUI>();
            text[0].text = $"{message["Subject"]} {message["Date"]}";
            text[1].text = message["Body"];
            var item = Instantiate(ChatPrefabs, ChatContent);
            ChatQueue.Enqueue(item);
            if (ChatQueue.Count > 20)
            {
                var dequeueItem = ChatQueue.Dequeue();
                Destroy(dequeueItem);
            }
        }
    }
    public void SetTimerText(float minutes, float seconds)
    {
        FindMatchTimer.text = $"{Mathf.Round(minutes).ToString().PadLeft(2, '0')}:{Mathf.Round(seconds).ToString().PadLeft(2, '0')}";
    }

}
