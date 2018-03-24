using System;
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
        [SerializeField] private AnimationCurve progression;
        [SerializeField] private AnimationCurve carSpeed;
        [SerializeField] private AnimationCurve carInterval;
        [SerializeField] private Car bigCar;
        [SerializeField] private Car smallCar;

        public int Width { private get; set; }

        private bool current;
        private bool? next;

        private static readonly Color[] carColors = {
            "0xff0000".hexToColor(),
            "0x00deff".hexToColor(),
            "0xffff00".hexToColor(),
            "0x00b533".hexToColor(),
            "0xffffff".hexToColor()
        };

    public GameObject BuildLine(int index)
        {
            if (next == null)
            {
                next = Utils.Probability(progression.Evaluate(index) + Mathf.PerlinNoise(index / 10f, 0) - .5f) || index < 3;
            }
            current = next.Value;
            next = Utils.Probability(progression.Evaluate(index + 1) + Mathf.PerlinNoise(index / 10f, 0) - .5f) || index < 3;

            var isEvenLine = index % 2 == 0;

            var interval = carInterval.Evaluate(index) + carInterval.Evaluate(index)/2f * Random.value;
            var speed = carSpeed.Evaluate(index) + carSpeed.Evaluate(index)/2f * Random.value;
            return current ? CreateForest(isEvenLine) : CreateRoad(next == false, interval, speed);
        }

        private GameObject CreateForest(bool isOdd)
        {
            var line = transform.InstantiateChild(forestLinePrefab);
            line.Length = Width;
            line.GrassColor = isOdd ? "0xb6ff4e".hexToColor() : "0xaef24b".hexToColor();
            return line.gameObject;
        }

        private GameObject CreateRoad(bool multiple, float interval, float speed)
        {
            var road = transform.InstantiateChild(roadLinePrefab);
            var isBig = Utils.RollDice(4);

            if (isBig)
            {
                interval *= 2;
                speed /= 2;
            }

            road.Length = Width;
            road.MinInterval = interval;
            road.MaxInterval = interval;
            road.MinSpeed = speed;
            road.MaxSpeed = speed;
            road.Colors = new[] {carColors.RandomElement()};
            road.Cars = isBig ? new[] {bigCar} : new[] {smallCar};
            road.Direction = Utils.RollDice(2) ? 1 : -1;
            road.SurfaceMarking = multiple;

            return road.gameObject;
        }
    }
}