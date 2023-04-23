using UnityEngine;
using FishNet.Transporting.Bayou;
using FishNet.Transporting.Multipass;
using FishNet.Transporting.Tugboat;
using UnityEngine.UI;
using FishNet;
using FishNet.Transporting;
using FishNet.Connection;
using System.Collections.Generic;


public class InitScript : MonoBehaviour
{
    [SerializeField] private Multipass mp;
    [SerializeField] private Text serverStatus;
    [SerializeField] private Text clientsConnected;
    [SerializeField] private Text clientStatus;
    public static InitScript ins;

    void Awake()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        mp.SetClientTransport<Bayou>();
        #else
        mp.SetClientTransport<Tugboat>();
        #endif

        if (ins == null)
        {
            ins = this;
        }
    }

    void Start(){
        InstanceFinder.ServerManager.OnServerConnectionState += ((ServerConnectionStateArgs args)=>{
            if(args.ConnectionState == LocalConnectionState.Started){
                serverStatus.text = "Server Status: Online";
            }
            if(args.ConnectionState == LocalConnectionState.Stopped){
                serverStatus.text = "Server Status: Offline";
            }
        });
        InstanceFinder.ServerManager.OnRemoteConnectionState += ((NetworkConnection conn, RemoteConnectionStateArgs args)=>{
            string connectedClients = "";
            foreach (KeyValuePair<int, NetworkConnection> client in InstanceFinder.ServerManager.Clients)
            {
                connectedClients += "Client #"+client.Key+", ";
            }
            clientsConnected.text = connectedClients;
        });
    }

    public void setClientStatus(string msg)
    {
        clientStatus.text = msg;
    }
}
