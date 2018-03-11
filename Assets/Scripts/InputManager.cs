using UnityEngine;

namespace Assets.Scripts
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Player player;

        private void Update()
        {
            var axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (player.IsMoving)
            {
                return;
            }

            if (axisInput.x < 0)
            {
                player.Move(-1, 0);
                return;
            }

            if (axisInput.x > 0)
            {
                player.Move(1, 0);
                return;
            }

            if (axisInput.y < 0)
            {
                player.Move(0, -1);
                return;
            }

            if (axisInput.y > 0)
            {
                player.Move(0, 1);
                return;
            }
        }
    }
}