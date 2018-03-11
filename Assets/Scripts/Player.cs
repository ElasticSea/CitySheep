using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Tweener breathing;
    private Tweener moving;
    public bool IsMoving => moving != null && moving.IsPlaying();

    private void Awake()
    {
        breathing = transform.DOScaleY(.9f, 1).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    public void Move(int x, int y)
    {
        var posA = transform.position;
        var posB = posA + new Vector3(x, 0, y);

        transform.LookAt(posB);

        if (Physics.Linecast(posA, posB) == false)
        {
            moving = transform.DOMove(posB, .3f).SetEase(Ease.OutCubic);
            transform.DOMoveY(.3f, .15f).SetEase(Ease.OutCubic).SetLoops(2, LoopType.Yoyo);
        }
    }
}