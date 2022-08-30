using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Threading.Tasks;
public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject playerPrefabs;
    void Start() {
        var player = PhotonNetwork.Instantiate($"Game/Prefabs/{playerPrefabs.name}", playerPrefabs.transform.position, Quaternion.identity);
        if (player.GetPhotonView().IsMine) {
            
        }
        if (!player.GetPhotonView().IsMine) return;
    }
    void Update() {

    }
}
