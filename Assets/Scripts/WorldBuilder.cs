using System;
using System.Collections.Generic;
using Assets.Core.Extensions;
using Assets.Core.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts
{
    public class WorldBuilder : MonoBehaviour
    {
        [SerializeField] private int generatorPreloadBuffer = 3;
        [SerializeField] private ForestLine forestLinePrefab;
        [SerializeField] private Road roadLinePrefab;

        private readonly Queue<Tuple<int, GameObject>> lineQueue = new Queue<Tuple<int, GameObject>>();
        private int lineIndex = -10;

        private void Update()
        {
            var topRight = Camera.main.ViewportPointToGround(new Vector2(1, 1)).Value;
            var bottomLeft = Camera.main.ViewportPointToGround(new Vector2(0,0)).Value;

            var targetIndex = topRight.z > 0 ? Mathf.Ceil(topRight.z): Mathf.Floor(topRight.z);
            while (lineIndex <= targetIndex + generatorPreloadBuffer)
            {
                var line = transform.InstantiateChild(forestLinePrefab);
                line.transform.position = lineIndex * Vector3.forward;
                lineQueue.Enqueue(Tuple.Create(lineIndex, line.gameObject));

                lineIndex++;
            }

            while (true)
            {
                var peek = lineQueue.Peek();
                if (peek.Item1 < bottomLeft.z - generatorPreloadBuffer)
                {
                    Destroy(lineQueue.Dequeue().Item2);
                }
                else
                {
                    break;
                }
            }
        }
    }
}