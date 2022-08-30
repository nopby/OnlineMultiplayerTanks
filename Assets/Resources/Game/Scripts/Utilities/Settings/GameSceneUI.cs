using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameSceneUI : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider LoadingSlider;
    public PlayerStats playerStats;
    void Awake()
    {
        InitializeSingletonUIObject();
    }
    public void InitializeSingletonUIObject()
    {
        PUNHandler.Instance.UIManager = gameObject;
        PUNHandler.Instance.LoadingScreen = LoadingScreen;
        PUNHandler.Instance.LoadingSlider = LoadingSlider;
    }
    public void Surrender()
    {
        playerStats.TakeDamage(100f);
    }
}
