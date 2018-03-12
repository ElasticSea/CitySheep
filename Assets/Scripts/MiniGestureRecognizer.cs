using System;
using UnityEngine;

public class MiniGestureRecognizer : MonoBehaviour
{
    [SerializeField] private float swipeTreshold = 10;

    private bool swiping;
    private bool eventSent;
    private Vector2 lastPosition;

    public event Action<SwipeDirection> OnSwipe;

    private void Update()
    {
        if (Input.touchCount == 0)
            return;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            eventSent = false;
            lastPosition = Input.GetTouch(0).position;
            return;
        }

        if (Input.GetTouch(0).phase == TouchPhase.Moved && eventSent == false)
        {
            var currentPosition = Input.GetTouch(0).position;
            var direction = currentPosition - lastPosition;

            if (direction.magnitude > swipeTreshold)
            {
                eventSent = true;
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    OnSwipe(direction.x > 0 ? SwipeDirection.Right : SwipeDirection.Left);
                }
                else
                {
                    OnSwipe(direction.y > 0 ? SwipeDirection.Up : SwipeDirection.Down);
                }
            }
        }
    }

    public enum SwipeDirection
    {
        None,
        Up,
        Down,
        Right,
        Left
    }
}