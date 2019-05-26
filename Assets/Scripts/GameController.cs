using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

namespace PhotonExample
{
    public class GameController : MonoBehaviourPunCallbacks
    {
        public PlayerNetwork PlayerPrefab;
        public PlayerNetwork LocalPlayer;

        private void Awake()
        {
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene("Start");
                return;
            }
        }

        void Start()
        {
            PlayerNetwork.RefreshInstance(ref LocalPlayer, PlayerPrefab);
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            PlayerNetwork.RefreshInstance(ref LocalPlayer, PlayerPrefab);
        }
    }
}