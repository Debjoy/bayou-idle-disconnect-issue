using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;

public class PlayerScript : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();
        if(InitScript.ins != null && IsOwner)
        InitScript.ins.setClientStatus("Client Status: Connected");
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        if(InitScript.ins != null && IsOwner)
        InitScript.ins.setClientStatus("Client Status: Disconnected");
    
    }

}
