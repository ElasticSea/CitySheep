using UnityEngine;

namespace Assets.Scripts
{
    public class CarKiller : MonoBehaviour
    {
        [SerializeField] private Vector3 killVector;

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Player>();
            if (player)
            {
                var go = new GameObject("StickedTo");
                go.transform.position = player.transform.position;
                go.transform.SetParent(transform);

                player.Kill(killVector, go.transform);
            }
        }

    }
}