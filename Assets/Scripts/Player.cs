using System;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveTime = .2f;
    [SerializeField] private AudioSource boingAudio;

    private Tween breathing;
    private Tween moving;
    private bool dead;
    private Transform stickedTo;

    public bool IsMoving => moving != null && moving.IsPlaying();

    public event Action OnKilled = () => {};

    private void Awake()
    {
        breathing = transform.DOScaleY(.9f, 1).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if(stickedTo != null)
            transform.position = stickedTo.position;
    }

    public void Move(int x, int y)
    {
        if (dead) return;

        var posA = transform.position;
        var posB = posA + new Vector3(x, 0, y);

        transform.LookAt(posB);

        if (Physics.Linecast(posA, posB) == false)
        {
            boingAudio.Play();
            moving = DOTween.Sequence()
                .Insert(0, transform.DOMove(posB, moveTime).SetEase(Ease.OutCubic))
                .Insert(0, transform.DOMoveY(.3f, moveTime / 2).SetEase(Ease.OutCubic).SetLoops(2, LoopType.Yoyo));
        }
    }

    public void Kill(Vector3 killVector, Transform stickedTo)
    {
        this.stickedTo = stickedTo;
        if (dead) return;

        breathing.Kill();
        moving.Kill();

        transform.DOScale(killVector, .1f).SetEase(Ease.InCubic);

        dead = true;

        OnKilled();
    }
}