using Assets.Core.Scripts.Extensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ui
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup gameOverScreen;
        [SerializeField] private CanvasGroup retryText;

        private Tween tween;

        private void Awake()
        {
            gameOverScreen.gameObject.SetActive(false);
            gameOverScreen.alpha = 0;
            retryText.alpha = 0;

            tween = DOTween.Sequence()
                .Insert(0, gameOverScreen.DoFade(1, .4f))
                .Insert(0.2f, retryText.DoFade(1, .4f))
                .SetAutoKill(false)
                .SetUpdate(true)
                .Pause();

            retryText.gameObject.AddComponent<EventTrigger>()
                .PointerClick(arg0 => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        }

        public void Show()
        {
            gameOverScreen.gameObject.SetActive(true);
            tween.PlayForward();
        }
    }
}