using System;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveTime = .2f;
    [SerializeField] private AudioSource boingAudio;

    private Tween breathingAnim;
    private Tween movingAnim;
    private bool isDead;
    private Transform stickedToObject;

    public bool IsMoving => movingAnim != null && movingAnim.IsPlaying();

    public event Action OnKilled = () => { };

    private void Awake()
    {
        breathingAnim = transform.DOScaleY(.9f, 1).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (stickedToObject != null)
            transform.position = stickedToObject.position;
    }

    public void Move(int x, int y)
    {
        if (isDead) return;

        var posA = transform.position;
        var posB = posA + new Vector3(x, 0, y);

        transform.LookAt(posB);

        if (Physics.Linecast(posA, posB) == false)
        {
            boingAudio.Play();
            movingAnim = DOTween.Sequence()
                .Insert(0, transform.DOMove(posB, moveTime).SetEase(Ease.OutCubic))
                .Insert(0, transform.DOMoveY(.3f, moveTime / 2).SetEase(Ease.OutCubic).SetLoops(2, LoopType.Yoyo));
        }
    }

    public void Kill(Vector3 killVector, Transform stickedToObject)
    {
        if (isDead) return;

        this.stickedToObject = stickedToObject;

        breathingAnim.Kill();
        movingAnim.Kill();

        transform.DOScale(killVector, .1f).SetEase(Ease.InCubic);

        isDead = true;

        OnKilled();
    }
}