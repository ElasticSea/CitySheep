using Assets.Core.Extensions;
using UnityEngine;

namespace Assets.Scripts
{
    public class ForestLine : MonoBehaviour
    {
        private const int LINE_LENGTH = 20;

        [SerializeField] private GameObject[] Prefabs;

        private void Awake()
        {
            var emptyChance = .2f;
            for (int i = 0; i < LINE_LENGTH; i++)
            {
                if(Random.value / emptyChance <= 1) continue;

                var instance = transform.InstantiateChild(Prefabs[Random.Range(0, Prefabs.Length)].transform);
                instance.localPosition = new Vector3(-LINE_LENGTH/2 + i, 0, 0);
            }
        }
    }
}