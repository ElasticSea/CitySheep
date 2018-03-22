using System;
using System.Collections.Generic;
using Assets.Core.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts
{
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField] private WorldBuilder builder;
        [SerializeField] private int generatorBufferLeft = 3;
        [SerializeField] private int generatorBufferRight = 3;
        [SerializeField] private int generatorBufferTop = 3;
        [SerializeField] private int generatorBufferBottom = 3;

        private readonly Queue<Tuple<int, GameObject>> lines = new Queue<Tuple<int, GameObject>>();
        private int lineIndex = -10;

        private void Awake()
        {
            var leftWorldPoint = Camera.main.ViewportPointToGround(new Vector2(0, 1)).Value;
            var rightWorldPoin = Camera.main.ViewportPointToGround(new Vector2(1, 0)).Value;
            var worldWidth = Mathf.CeilToInt(Mathf.Max(Mathf.Abs(leftWorldPoint.x), Mathf.Abs(rightWorldPoin.x)) * 2);
            var safetyBuffer = generatorBufferLeft + generatorBufferRight;
            builder.Width = worldWidth + safetyBuffer;
        }

        private void Update()
        {
            var topWorldPoint = Camera.main.ViewportPointToGround(new Vector2(1, 1)).Value;
            var bottomWorldPoint = Camera.main.ViewportPointToGround(new Vector2(0, 0)).Value;

            var targetIndex = topWorldPoint.z > 0 ? Mathf.Ceil(topWorldPoint.z) : Mathf.Floor(topWorldPoint.z);
            while (lineIndex <= targetIndex + generatorBufferTop)
            {
                var line = builder.BuildLine(lineIndex);
                line.transform.position = lineIndex * Vector3.forward;
                lines.Enqueue(Tuple.Create(lineIndex, line));
                lineIndex++;
            }

            while (true)
            {
                var peek = lines.Peek();
                if (peek.Item1 < bottomWorldPoint.z - generatorBufferBottom)
                {
                    Destroy(lines.Dequeue().Item2);
                }
                else
                {
                    break;
                }
            }
        }
    }
}