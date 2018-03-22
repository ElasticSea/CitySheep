using Assets.Core.Extensions;
using Assets.Core.Scripts.Camera;
using UnityEngine;

namespace Assets.Scripts
{
    public class MoveCam : MonoBehaviour
    {
        [SerializeField] private Follow followCam;

        private Vector3 lastPos;

        private void Awake()
        {
            followCam.OnPositionUpdate += () =>
            {
                var distance = lastPos.Distance(followCam.targetPosition);
                var remainingDistance = Mathf.Max(Time.deltaTime - distance, 0);
                followCam.targetPosition += Vector3.forward * remainingDistance;
                lastPos = followCam.targetPosition;
            };
        }
    }
}