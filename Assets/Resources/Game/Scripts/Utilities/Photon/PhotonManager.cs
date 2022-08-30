using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PhotonManager : MonoBehaviourPunCallbacks
{
    ServerSettings ServerSettings;
    public string AppIDRealtime { get { return ServerSettings.AppSettings.AppIdRealtime; }}
    public string AppIDChat { get {return ServerSettings.AppSettings.AppIdChat; }}
}
