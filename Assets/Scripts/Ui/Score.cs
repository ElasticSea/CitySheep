using Assets.Core.Extensions;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private TextMeshProUGUI score;
        private Tween tween;
        private float initialPosition;

        public int ScoreValue
        {
            get { return int.Parse(score.text); }
            set { score.text = value.ToString(); }
        }

        private void Awake()
        {
            ScoreValue = 0;
        }

        private void OnRectTransformDimensionsChange()
        {
            // tween.Complete() does not always set the final position correctly
            initialPosition = score.transform.localPosition.y;
        }

        private void Update()
        {
            var currentScore = Mathf.CeilToInt(player.transform.position.z);
            if (currentScore > ScoreValue)
            {
                ScoreValue = currentScore;
                if(tween != null && tween.IsPlaying()) tween.Complete();

                score.transform.SetLocalY(initialPosition);

                tween = DOTween.Sequence()
                    .Insert(0, score.transform.DOLocalMoveY(initialPosition + 10, .07f))
                    .SetEase(Ease.OutCubic)
                    .SetLoops(2, LoopType.Yoyo);
            }
        }
    }
}