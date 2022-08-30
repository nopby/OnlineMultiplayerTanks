using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Threading.Tasks;
public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject playerPrefabs;
    void Start() {
        var player = PhotonNetwork.Instantiate($"Game/Prefabs/{playerPrefabs.name}", playerPrefabs.transform.position, Quaternion.identity);
    }
    void Update() {

    }
}
