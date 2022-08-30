using System;
using System.Collections.Generic;
using UnityEngine;
using DimensionalDeveloper.TankBuilder.Utility;
using Photon.Pun;
using Unity.VisualScripting;
public class WeaponController : MonoBehaviourPunCallbacks
{
    PlayerController controller;
    [HideInInspector] public bool[] hideSection = new bool[6];
    public List<Transform> firePoints = new List<Transform>();
    public bool[] cannonInput = new bool[3];
    public bool[] shootCannon = new bool[3];
    public Weapon[] weapons = new Weapon[3];
    public bool[] weaponDropdown = new bool[3];
    public Dictionary<string, object> weaponInfo;
    void Start()
    {
        if (!photonView.IsMine) return;
        weaponInfo = new Dictionary<string, object>
            {
                { "Owner", 0 },
                { "Asset", "" },
                { "ShotSound", "" },
                { "MuzzleFlash", "" },
                { "Speed", 0f },
                { "Damage", 0f },
                { "ExplosionSound", "" },
                { "Explosion", "" },
                { "CollisionLayer", 0 }
            };
        controller = GetComponent<PlayerController>();
    }
    public void Fire()
    {
        // Iterasi titik tembak
        var index = firePoints.Count == 1 ? 0 : 1;
        var end = firePoints.Count == 1 ? 1 : 3;
        for (; index < end; index++)
        {
            // Referensi objek senjata
            ref var weapon = ref weapons[index];
            var shotTimer = weapon.ShotTimer;
            if (shotTimer.y > 0f) shotTimer.y -= Time.deltaTime;
            weapon.ShotTimer = shotTimer;
            // Cek input tembak
            cannonInput[0] = InputManager.Instance.Fire1;
            cannonInput[1] = InputManager.Instance.Fire1;
            cannonInput[2] = InputManager.Instance.Fire2;
            var input = cannonInput[index];

            // Skip proses jika tidak ada input atau waktu tembak kurang dari
            if (!input || !(shotTimer.y < Mathf.Epsilon)) continue;
            shootCannon[index] = false;
            // Inisialisasi letak dan arah tembak
            var firePoint = firePoints[index];
            var firePointPosition = firePoint.position;
            var firePointRotation = firePoint.rotation;
            
            // Jika ada input maka lakukan tembakan
            if (input)
            {
                // Inisialisasi info senjata
                weaponInfo["Asset"] = weapon.Asset;
                weaponInfo["ShotSound"] = weapon.ShotSound;
                weaponInfo["MuzzleFlash"] = weapon.MuzzleFlash;
                weaponInfo["Speed"] = weapon.Speed;
                weaponInfo["Damage"] = weapon.Damage;
                weaponInfo["ExplosionSound"] = weapon.ExplosionSound;
                weaponInfo["Explosion"] = weapon.Explosion;
                weaponInfo["CollisionLayer"] = (int)weapon.CollisionLayer;
                weaponInfo["Owner"] = photonView.ViewID;
                
                // Eksekusi tembakan pada seluruh pemain dalam ruangan
                CustomEvent.Trigger(gameObject, "OnFire", weaponInfo, firePointPosition, firePointRotation);
            }
            // Restart the shot timer.
            weapon.ShotTimer = shotTimer.WithY(shotTimer.x);
        }
    }
    [PunRPC]
    void RPC_Fire(Dictionary<string, object> weaponInfo, Vector3 firePointPosition, Quaternion firePointRotation)
    {
        // Instansiasi proyektil
        var ammo = AssetManager.Instance.SpawnObject((string)weaponInfo["Asset"], firePointPosition, firePointRotation);

        // Inisialisasi peluru
        ammo.GetComponent<AmmoController>().InitialiseAmmo(weaponInfo);

        // Instansiasi suara tembakan
        AudioManager.Instance.PlaySound((string)weaponInfo["ShotSound"]);

        // Instansiasi efek animasi
        AssetManager.Instance.SpawnObject((string)weaponInfo["MuzzleFlash"], firePointPosition, firePointRotation);
        
    }


    


}