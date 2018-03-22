using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ui
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private MiniGestureRecognizer gestureRecognizer;

        private Dictionary<KeyCode, Action> mappings;
        private KeyCode saveForLater;

        private void Awake()
        {
            mappings = new Dictionary<KeyCode, Action>
            {
                {KeyCode.LeftArrow, () => { player.Move(-1, 0); }},
                {KeyCode.RightArrow, () => { player.Move(1, 0); }},
                {KeyCode.DownArrow, () => { player.Move(0, -1); }},
                {KeyCode.UpArrow, () => { player.Move(0, 1); }}
            };

            gestureRecognizer.OnSwipe += direction =>
            {
                KeyCode key;
                switch (direction)
                {
                    case MiniGestureRecognizer.SwipeDirection.None:
                    case MiniGestureRecognizer.SwipeDirection.Up:
                        key = KeyCode.UpArrow;
                        break;
                    case MiniGestureRecognizer.SwipeDirection.Down:
                        key = KeyCode.DownArrow;
                        break;
                    case MiniGestureRecognizer.SwipeDirection.Right:
                        key = KeyCode.RightArrow;
                        break;
                    case MiniGestureRecognizer.SwipeDirection.Left:
                        key = KeyCode.LeftArrow;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }

                if (player.IsMoving)
                {
                    saveForLater = key;
                }
                else
                {
                    mappings[key].Invoke();
                }
            };
        }
        private void Update()
        {
            if (player.IsMoving)
            {
                foreach (var mapping in mappings)
                {
                    if (Input.GetKeyDown(mapping.Key))
                    {
                        saveForLater = mapping.Key;
                    }
                }
                return;
            }

            if (saveForLater != KeyCode.None)
            {
                mappings[saveForLater].Invoke();
                saveForLater = KeyCode.None;
                return;
            }

            foreach (var mapping in mappings)
            {
                if (ProcessKey(mapping.Key))
                {
                    return;
                }
            }
        }

        private bool ProcessKey(KeyCode mappingKey)
        {
            if (Input.GetKeyDown(mappingKey))
            {
                mappings[mappingKey].Invoke();
                return true;
            }

            return false;
        }
    }
}