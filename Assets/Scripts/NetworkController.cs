using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

namespace PhotonExample
{
    public class NetworkController : MonoBehaviourPunCallbacks
    {
        public Button ButtonConnect;
        public Button ButtonJoin;

        private bool _connectingToMaster;
        private bool _joiningRoom;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            _connectingToMaster = false;
            _joiningRoom = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (ButtonConnect != null)
                ButtonConnect.gameObject.SetActive(!PhotonNetwork.IsConnected && !_connectingToMaster);

            if (ButtonJoin != null)
                ButtonJoin.gameObject.SetActive(PhotonNetwork.IsConnected && !_connectingToMaster && !_joiningRoom);
        }

        public void Button_Connect()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public void Button_JoinRoom()
        {
            if (!PhotonNetwork.IsConnected)
                return;

            _joiningRoom = true;
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            _connectingToMaster = false;
            Debug.Log("Connected to Master!");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            _connectingToMaster = false;
            _joiningRoom = false;
            Debug.Log(cause);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 20 });
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            Debug.Log(message);
            base.OnCreateRoomFailed(returnCode, message);
            _joiningRoom = false;
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            _joiningRoom = false;
            Debug.Log("Room: " + PhotonNetwork.CurrentRoom.Name + " - Players: " + PhotonNetwork.CurrentRoom.PlayerCount + " - Master: " + PhotonNetwork.IsMasterClient);
            SceneManager.LoadScene("Network");
        }
    }
}