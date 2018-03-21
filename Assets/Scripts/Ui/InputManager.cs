using System;
using UnityEngine;

namespace Assets.Scripts.Ui
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private MiniGestureRecognizer gestureRecognizer;

        private void Awake()
        {
            gestureRecognizer.OnSwipe += direction =>
            {
                switch (direction)
                {
                    case MiniGestureRecognizer.SwipeDirection.None:
                    case MiniGestureRecognizer.SwipeDirection.Up:
                        player.Move(0, 1);
                        break;
                    case MiniGestureRecognizer.SwipeDirection.Down:
                        player.Move(0, -1);
                        break;
                    case MiniGestureRecognizer.SwipeDirection.Right:
                        player.Move(1, 0);
                        break;
                    case MiniGestureRecognizer.SwipeDirection.Left:
                        player.Move(-1, 0);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
            };
        }
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