using UnityEngine;

namespace Assets.Scripts
{
    public class KillIfPlayerOfBounds : MonoBehaviour
    {
        [SerializeField] private Player player;

        private void Update()
        {
            var screen = Camera.main.WorldToViewportPoint(player.transform.position);
            if (screen.x < 0 || screen.x > 1 || screen.y < 0 || screen.y > 1)
            {
                player.Kill();
            }
        }
    }
}