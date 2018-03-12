using Assets.Core.Scripts.Camera;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    public class Gameplay : MonoBehaviour
    {
        [SerializeField] private Player player;

        private void Awake()
        {
            player.OnKilled += () =>
            {
//                Camera.main.GetComponent<Follow>().SmoothFollow = false;
                DOTween.To(() => Camera.main.GetComponent<Follow>().FollowSpeed, value => Camera.main.GetComponent<Follow>().FollowSpeed = value, 5, 0.3f).SetEase(Ease.InQuad);
                DOTween.To(() => Camera.main.GetComponent<Follow>().AfterOffset, value => Camera.main.GetComponent<Follow>().AfterOffset = value, Vector3.zero, 0.3f).SetEase(Ease.InQuad);
                DOTween.To(() => Time.timeScale, value => Time.timeScale = value, 0, 0.3f).SetEase(Ease.InQuad).SetDelay(.5f);
            };
        }
    }
}