using UnityEngine;

namespace Assets.Scripts.World
{
    public class CarContactSide : MonoBehaviour
    {
        [SerializeField] private DeformationType deformation;
        [SerializeField] private AudioSource crashSound;

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Player>();
            if (player)
            {
                crashSound.Play();

                var go = new GameObject("StickedTo");
                go.transform.position = player.transform.position;
                go.transform.SetParent(transform);

                var afterCollisionScale = deformation == DeformationType.RunOver ? new Vector3(1, .1f, 1) : new Vector3(1, 1, .1f);
                player.Kill(afterCollisionScale, go.transform);
            }
        }

        public enum DeformationType
        {
            RunOver,
            CollideWithSide
        }
    }
}