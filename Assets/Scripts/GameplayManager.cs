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
            AudioListener.volume = 1;
            Time.timeScale = 1;

            player.OnKilled += () =>
            {
                var follow = Camera.main.GetComponent<Follow>();
                DOTween.Sequence()
                    .Insert(0, DOTween.To(() => follow.FollowSpeed, value => follow.FollowSpeed = value, 5, 0.5f))
                    .Insert(0, DOTween.To(() => follow.AfterOffset, value => follow.AfterOffset = value, Vector3.zero, 0.5f))
                    .Insert(0.5f, DOTween.To(() => Time.timeScale, value => Time.timeScale = value, 0, 0.3f))
                    .Insert(0.5f, DOTween.To(() => AudioListener.volume, value => AudioListener.volume = value, 0, 0.3f))
                    .OnComplete(() => OnGameOver())
                    .SetUpdate(true);
            };
        }
    }
}