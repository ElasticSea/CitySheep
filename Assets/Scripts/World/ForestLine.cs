using System;
using System.Linq;
using Assets.Core.Extensions;
using Assets.Core.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.World
{
    public class ForestLine : MonoBehaviour
    {
        [SerializeField] private int length = 20;
        [SerializeField] private Color grassColor = "0x6ad341".hexToColor();
        [SerializeField] private int fillSize = 20;
        [SerializeField] private AnimationCurve fillChance = AnimationCurve.Constant(0, 1, .5f);
        [SerializeField] private GameObject[] forestPrefabs;

        public AnimationCurve FillChance
        {
            get { return fillChance; }
            set { fillChance = value; }
        }

        public Color GrassColor
        {
            get { return grassColor; }
            set { grassColor = value; }
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        private void Start()
        {
            var grassQuad = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
            grassQuad.SetParent(transform);
            grassQuad.localPosition = new Vector3(0, 0, -.5f);
            grassQuad.localScale = new Vector3(Length, 1, 1);
            grassQuad.localRotation = Quaternion.Euler(90, 0, 0);
            grassQuad.GetComponent<MeshRenderer>().material.color = GrassColor;

            for (var i = 0; i < Length; i++)
            {
                var offset = (Length - fillSize) / 2;
                var rollChance = i > offset && i < Length - offset
                    ? FillChance.Evaluate(FillChance.keys.Last().time / fillSize * (i - offset))
                    : 1;

                if (Utils.RollDice(1 / rollChance))
                {
                    var forestPrefab = forestPrefabs[Random.Range(0, forestPrefabs.Length)].transform;
                    var forestObject = transform.InstantiateChild(forestPrefab);
                    forestObject.localPosition = new Vector3(-Length / 2 + i, 0, 0);
                }
            }
        }
    }
}