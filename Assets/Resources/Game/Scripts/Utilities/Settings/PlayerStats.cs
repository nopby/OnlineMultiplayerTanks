using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Unity.VisualScripting;
public class PlayerStats : MonoBehaviourPunCallbacks
{
    public Slider HealthSlider;
    public PlayerController controller;
    public float maxHP = 100;
    public float currentHP;
    public bool isDefeated;
    void Start()
    {
        if (!photonView.IsMine) return;
        AssignHealthBar();
        controller = GetComponent<PlayerController>();
    }
    public void AssignHealthBar()
    {
        HealthSlider = GameObject.FindObjectOfType<Slider>();
        HealthSlider.value = currentHP = maxHP;
    }
    public void TakeDamage(float damage)
    {
        CustomEvent.Trigger(gameObject, "OnPerformCollision", damage);
    }
    [PunRPC]
    public void RPC_TakeDamage(float damage)
    {
        // Dieksekusi oleh klien yang terkena dampak
        if (!photonView.IsMine) return;
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        HealthSlider.value = currentHP * 0.01f;

        if (currentHP <= 0)
        {
            isDefeated = true;
            CustomEvent.Trigger(gameObject, "OnDefeated");
            photonView.RPC("RPC_ShowResult", RpcTarget.All);
        }
    }
    [PunRPC]
    public void RPC_ShowResult()
    {
        if (!photonView.IsMine) return;
        // Menampilkan ledakan obyek pemain
        AssetManager.Instance.SpawnObject("Tank Explosion", transform.position, transform.rotation);
        AudioManager.Instance.PlaySound("Tank Explosion");
        if (isDefeated)
        {
            CustomEvent.Trigger(PUNHandler.Instance.gameObject, "OnPlayerDefeated");
        }
        else
        {
            CustomEvent.Trigger(PUNHandler.Instance.gameObject, "OnPlayerVictory");
        }
    }
    public void UpdateHealth()
    {
        HealthSlider.value = currentHP * 0.01f;
    }

}
