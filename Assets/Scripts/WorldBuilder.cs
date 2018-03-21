using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Core.Extensions;
using Assets.Core.Scripts;
using Assets.Scripts.World;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class WorldBuilder : MonoBehaviour
    {
        [SerializeField] private ForestLine forestLinePrefab;
        [SerializeField] private Road roadLinePrefab;
        [SerializeField] private Car bigCar;
        [SerializeField] private Car smallCar;

        public int Width { private get; set; }

        public List<GameObject> BuildAtIndex(int index)
        {
            var isEvenLine = index % 2 == 0;

            if (index > 150)
            {
                if (Utils.RollDice(3))
                {
                    if (Utils.RollDice(3))
                    {
                        return CreateRoad(12).Concat(CreateForest(isEvenLine)).ToList();
                    }

                    return CreateRoad(8).Concat(CreateForest(isEvenLine)).ToList();
                }

                return CreateForest(isEvenLine);
            }

            if (index > 100)
            {
                if (Utils.RollDice(3))
                {
                    if (Utils.RollDice(3))
                    {
                        return CreateRoad(6).Concat(CreateForest(isEvenLine)).ToList();
                    }

                    return CreateRoad(4).Concat(CreateForest(isEvenLine)).ToList();
                }

                return CreateForest(isEvenLine);
            }

            if (index > 40)
            {
                if (Utils.RollDice(3))
                {
                    if (Utils.RollDice(3))
                    {
                        return CreateRoad(4).Concat(CreateForest(isEvenLine)).ToList();
                    }

                    return CreateRoad(2).Concat(CreateForest(isEvenLine)).ToList();
                }

                return CreateForest(isEvenLine);
            }

            if (index > 10)
            {
                if (Utils.RollDice(6))
                {
                    if (Utils.RollDice(3))
                    {
                        return CreateRoad(2).Concat(CreateForest(isEvenLine)).ToList();
                    }

                    return CreateRoad(1).Concat(CreateForest(isEvenLine)).ToList();
                }

                return CreateForest(isEvenLine);
            }

            return CreateForest(isEvenLine);
        }

        private List<GameObject> CreateForest(bool isOdd)
        {
            var line = transform.InstantiateChild(forestLinePrefab);
            line.Length = Width;
            line.GrassColor = isOdd ? "0xb6ff4e".hexToColor() : "0xaef24b".hexToColor();
            return new List<GameObject> {line.gameObject};
        }

        private List<GameObject> CreateRoad(int cars)
        {
            var roads = new List<GameObject>(cars);

            for (var i = 0; i < cars; i++)
            {
                var road = transform.InstantiateChild(roadLinePrefab);
                road.Length = Width;
                if (Utils.RollDice(4))
                {
                    var interval = 4f + 2f * Random.value;
                    road.MinInterval = interval;
                    road.MaxInterval = interval;
                    var speed = 1f + 1f * Random.value;
                    road.MinSpeed = speed;
                    road.MaxSpeed = speed;
                    road.Cars = new[] {bigCar};
                }
                else
                {
                    var interval = 3f + 1.5f * Random.value;
                    road.MinInterval = interval;
                    road.MaxInterval = interval;
                    var speed = 2f + 2f * Random.value;
                    road.MinSpeed = speed;
                    road.MaxSpeed = speed;
                    road.Cars = new[] {smallCar};
                }

                road.Direction = Utils.RollDice(2) ? 1 : -1;
                road.SurfaceMarking = i < cars - 1;
                roads.Add(road.gameObject);
            }

            return roads;
        }
    }
}