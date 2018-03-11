using System.Linq;
using Assets.Core.Extensions;
using UnityEngine;

namespace Assets.Scripts
{
    public class ForestLine : MonoBehaviour
    {
        [SerializeField] private int length = 20;
        [SerializeField] private AnimationCurve fillChange = AnimationCurve.Constant(0, 1, .5f);
        [SerializeField] private GameObject[] Prefabs;

        private void Start()
        {
            for (var i = 0; i < length; i++)
            {
                var change = fillChange.Evaluate(fillChange.keys.Last().time / length * i);
                if (Random.value / change <= 1)
                {
                    var instance = transform.InstantiateChild(Prefabs[Random.Range(0, Prefabs.Length)].transform);
                    instance.localPosition = new Vector3(-length / 2 + i, 0, 0);
                }
            }
        }
    }
}