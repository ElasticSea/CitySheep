using Assets.Core.Scripts.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ui
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject retryText;

        [SerializeField] private GameplayManager gameplayManager;

        private void Awake()
        {
            gameOverScreen.SetActive(false);

            gameplayManager.OnGameOver += () =>
            {
                gameOverScreen.SetActive(true);
            };

            retryText.AddComponent<EventTrigger>().PointerClick(arg0 => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        }
    }
}