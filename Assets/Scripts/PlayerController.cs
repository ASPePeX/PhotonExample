using UnityEngine;

namespace PhotonExample
{
    public class PlayerController : MonoBehaviour
    {
        public float SpeedMove = 1;
        public float SpeedRot = 1;

        void Update()
        {
            this.transform.position += this.transform.forward * (Input.GetAxis("Vertical") * SpeedMove * Time.deltaTime);
            this.transform.rotation *= Quaternion.Euler(Vector3.up * Input.GetAxis("Horizontal") * SpeedRot * Time.deltaTime);
        }
    }
}