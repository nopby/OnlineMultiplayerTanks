using UnityEngine;
using DimensionalDeveloper.TankBuilder.Utility;
using Photon.Pun;
using System.Collections.Generic;

public class AmmoController : MonoBehaviourPunCallbacks
{
    public int Owner;
    
    private new Rigidbody2D Rigidbody2D
    {
        get
        {
            if (_rigidbody2D) return _rigidbody2D;

            _rigidbody2D = GetComponent<Rigidbody2D>();
            ExtensionsLibrary.CheckComponent(_rigidbody2D, "Rigidbody Component", name);
            return _rigidbody2D;
        }
    }
    private Rigidbody2D _rigidbody2D;
    public float Damage
    {
        get => _damage;
        set => _damage = Mathf.Max(0.0f, value);
    }
    private float _damage = 20.0f;
    public float Speed
    {
        get => _speed;
        set => _speed = Mathf.Max(20.0f, value);
    }
    private float _speed = 40.0f;
    public string explosionSound;
    public string explosion;
    public LayerMask collisionLayer;
    private Transform ImpactTarget;

    public void InitialiseAmmo(Dictionary<string, object> copy)
    {
        Speed = (float)copy["Speed"];
        Damage = (float)copy["Damage"];
        explosionSound = (string)copy["ExplosionSound"];
        explosion = (string)copy["Explosion"];
        collisionLayer = (int)copy["CollisionLayer"];
        Owner = (int) copy["Owner"];
        Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }

    private void FixedUpdate()
    {
        Move();
        PerformCollisionCheck();
    }
    public void Move()
    {
        Vector2 velocity = transform.up * (Speed * Time.fixedDeltaTime);
        Rigidbody2D.MovePosition(Rigidbody2D.position + velocity);
    }
    
    private void PerformCollisionCheck()
    {
        var hitInfo = Physics2D.Raycast(transform.position, Vector2.up, 0.1f, collisionLayer);

        // Cek benturan
        if (hitInfo.collider == null) return;
        ImpactTarget = hitInfo.collider.transform;
        switch (ImpactTarget.tag)
        {
            case "Mine":
            {
                Explosion("Mine Explosion", "MineExplosion");
                ImpactTarget.gameObject.SetActive(false);
                break;
            }
            case "Player":
            {
                var ImpactViewID = ImpactTarget.gameObject.GetPhotonView().ViewID;
                if (Owner == ImpactViewID) return;

                Explosion();
                ImpactTarget.gameObject.GetComponent<PlayerStats>().TakeDamage(_damage);
                break;
            }
            case "Environment":
            {
                Explosion();
                break;
            }
        }
    }
    private void Explosion()
    {
        gameObject.SetActive(false);
        AssetManager.Instance.SpawnObject(explosion, transform.position, transform.rotation);
        AudioManager.Instance.PlaySound(explosionSound);
    }
    private void Explosion(string assetName, string sound)
    {
        gameObject.SetActive(false);
        AssetManager.Instance.SpawnObject(assetName, transform.position, transform.rotation);
        AudioManager.Instance.PlaySound(sound);
    }


}



