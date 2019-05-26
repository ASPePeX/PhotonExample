using Photon.Pun;
using UnityEngine;

namespace PhotonExample
{
    public class PlayerNetwork : MonoBehaviourPun, IPunObservable
    {
        public NwDataPlayer NwData;

        private void Awake()
        {
            if (!photonView.IsMine && this.GetComponent<PlayerController>() != null)
                Destroy(this.GetComponent<PlayerController>());
        }

        public static void RefreshInstance(ref PlayerNetwork player, PlayerNetwork Prefab)
        {
            var position = Vector3.zero;
            var rotation = Quaternion.identity;
            if (player != null)
            {
                position = player.transform.position;
                rotation = player.transform.rotation;
                PhotonNetwork.Destroy(player.gameObject);
            }

            player = PhotonNetwork.Instantiate(Prefab.gameObject.name, position, rotation).GetComponent<PlayerNetwork>();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(NwData.NoseHeight);
            }
            else
            {
                NwData.NoseHeight = (float)stream.ReceiveNext();
            }
        }
    }
}