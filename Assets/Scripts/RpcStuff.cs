using Photon.Pun;
using UnityEngine;

public class RpcStuff : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NwRpcSend("Space was pressed");
        }
    }

    public void NwRpcSend(string blub)
    {
        //Remote Procedure call
        photonView.RPC("NwRpcReceive", RpcTarget.All, blub);
    }

    [PunRPC]
    void NwRpcReceive(string blub, PhotonMessageInfo info)
    {
        Debug.Log(blub);
    }
}
