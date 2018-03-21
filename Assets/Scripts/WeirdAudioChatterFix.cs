using Assets.Core.Extensions;
using UnityEngine;

namespace Assets.Scripts
{
    // When instance with AudioSource is spawn, for a brief moment the click is played on position Vector3.Zero even though the instance has different position.
    public class WeirdAudioChatterFix : MonoBehaviour
    {
        private void Awake()
        {
            GetComponentsInChildren<AudioSource>().ForEach(s => s.enabled = false);
        }

        private void Start()
        {
            GetComponentsInChildren<AudioSource>().ForEach(s => s.enabled = true);
        }
    }
}