using Assets.Core.Extensions;
using UnityEngine;

namespace Assets.Scripts
{
    public class WorldBuilder : MonoBehaviour
    {
        [SerializeField] private ForestLine forestLinePrefab;

        private void Awake()
        {
            for (int i = 0; i < 10; i++)
            {
                var line = transform.InstantiateChild(forestLinePrefab);
                line.transform.position = i * Vector3.forward;
            }
        }
    }
}