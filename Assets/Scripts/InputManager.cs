using UnityEngine;

namespace Assets.Scripts
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Player player;

        private void Update()
        {
            if (player.IsMoving)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                player.Move(-1, 0);
                return;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                player.Move(1, 0);
                return;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                player.Move(0, -1);
                return;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                player.Move(0, 1);
                return;
            }
        }
    }
}