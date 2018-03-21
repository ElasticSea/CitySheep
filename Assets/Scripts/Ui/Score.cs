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

        public int ScoreValue
        {
            get { return int.Parse(score.text); }
            set { score.text = value.ToString(); }
        }

        private void Awake()
        {
            ScoreValue = 0;
        }

        private void Update()
        {
            var currentScore = Mathf.CeilToInt(player.transform.position.z);
            if (currentScore > ScoreValue)
            {
                ScoreValue = currentScore;
                if(tween != null && tween.IsPlaying()) tween.Complete();

                tween = DOTween.Sequence()
                    .Insert(0, score.transform.DOLocalMoveY(score.transform.localPosition.y + 10, .07f))
//                    .Insert(0, score.transform.DOScale(Vector3.one * 1.1f, .07f))
                    .SetEase(Ease.OutCubic)
                    .SetLoops(2, LoopType.Yoyo);
            }
        }
    }
}