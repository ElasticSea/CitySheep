using Assets.Core.Scripts.Camera;
using UnityEngine;

namespace Assets.Scripts
{
    public class MoveCam : MonoBehaviour
    {
        [SerializeField] private Follow followCam;

        private void Awake()
        {
            followCam.OnPositionUpdate += () =>
            {
                followCam.targetPosition += Vector3.forward * Time.deltaTime;
            };
        }
    }
}