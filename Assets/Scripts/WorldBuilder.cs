using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Core.Extensions;
using Assets.Core.Scripts;
using Assets.Core.Scripts.Extensions;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class WorldBuilder : MonoBehaviour
    {
        [SerializeField] private readonly int generatorPreloadBuffer = 3;
        [SerializeField] private ForestLine forestLinePrefab;
        [SerializeField] private Road roadLinePrefab;

        private readonly Queue<Tuple<int, GameObject>> lineQueue = new Queue<Tuple<int, GameObject>>();
        private int lineIndex = -10;

        [SerializeField] private AnimationCurve forestCurve;
        [SerializeField] private AnimationCurve oneWayAuto;
        [SerializeField] private GameObject bigCar;
        [SerializeField] private GameObject smallCar;
        
        private void Update()
        {
            var topRight = Camera.main.ViewportPointToGround(new Vector2(1, 1)).Value;
            var bottomLeft = Camera.main.ViewportPointToGround(new Vector2(0, 0)).Value;

            var targetIndex = topRight.z > 0 ? Mathf.Ceil(topRight.z) : Mathf.Floor(topRight.z);
            while (lineIndex <= targetIndex + generatorPreloadBuffer)
            {
                foreach (var line in create())
                {
                    line.transform.position = lineIndex * Vector3.forward;
                    lineQueue.Enqueue(Tuple.Create(lineIndex, line));
                    lineIndex++;
                }
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

        private List<GameObject> create()
        {
            if (lineIndex > 10)
            {
                if (Utils.RollDice(6))
                {
                    if (Utils.RollDice(3))
                    {
                        return createCar(2).Concat(createForest()).ToList();
                    }
                    return createCar(1).Concat(createForest()).ToList();
                }
                return createForest();
            }
            return createForest();
        }

        private List<GameObject> createForest()
        {
            var line = transform.InstantiateChild(forestLinePrefab);
            line.GrassColor = Mathf.Abs(lineIndex) % 2 == 0 ? "0xb6ff4e".hexToColor() : "0xaef24b".hexToColor();
            return new List<GameObject> {line.gameObject};
        }

        private List<GameObject> createCar(int cars)
        {
            var lines = new List<GameObject>(cars);

            for (var i = 0; i < cars; i++)
            {
                var road = transform.InstantiateChild(roadLinePrefab);
                if (Utils.RollDice(4))
                {
                    road.MinInterval = 5f;
                    road.MaxInterval = 5f;
                    road.MinSpeed = 3f;
                    road.MaxSpeed = 3f;
                    road.Cars = new[] { bigCar  };
                }
                else
                {
                    road.MinInterval = 4f;
                    road.MaxInterval = 4f;
                    road.MinSpeed = 2f;
                    road.MaxSpeed = 2f;
                    road.Cars = new[] { smallCar };
                }
                road.Direction = i % 2 == 0 ? 1 : -1;
                road.Edge = i < cars - 1;
                lines.Add(road.gameObject);
            }
            return lines;
        }
    }
}