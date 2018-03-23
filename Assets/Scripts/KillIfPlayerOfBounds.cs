using UnityEngine;

namespace Assets.Scripts
{
    public class KillIfPlayerOfBounds : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private float margin = .1f;

        private void Update()
        {
            var screen = Camera.main.WorldToViewportPoint(player.transform.position);
            if (screen.x < 0-margin || screen.x > 1+ margin || screen.y < 0 - margin || screen.y > 1 + margin)
            {
                player.Kill();
            }
        }
    }
}