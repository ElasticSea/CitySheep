using System;
using Assets.Core.Scripts.Camera;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private Player player;

        public event Action OnGameOver = () => { };

        private void Awake()
        {
            Time.timeScale = 1;
            player.OnKilled += () =>
            {
//                Camera.main.GetComponent<Follow>().SmoothFollow = false;
                DOTween.To(() => Camera.main.GetComponent<Follow>().FollowSpeed, value => Camera.main.GetComponent<Follow>().FollowSpeed = value, 5, 0.3f).SetEase(Ease.InQuad).SetUpdate(true);
                DOTween.To(() => Camera.main.GetComponent<Follow>().AfterOffset, value => Camera.main.GetComponent<Follow>().AfterOffset = value, Vector3.zero, 0.3f).SetEase(Ease.InQuad).SetUpdate(true);
                DOTween.To(() => Time.timeScale, value => Time.timeScale = value, 0, 0.3f).SetEase(Ease.InQuad).SetDelay(.5f).OnComplete(() => OnGameOver()).SetUpdate(true);
            };
        }
    }
}