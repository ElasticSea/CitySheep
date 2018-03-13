using System;
using System.Linq;
using Assets.Core.Extensions;
using Assets.Core.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class ForestLine : MonoBehaviour
    {
        [SerializeField] private int length = 20;
        [SerializeField] private Color grassColor = "0x6ad341".hexToColor();
        [SerializeField] private int fillSize = 20;
        [SerializeField] private AnimationCurve fillChange = AnimationCurve.Constant(0, 1, .5f);
        [SerializeField] private GameObject[] Prefabs;

        public AnimationCurve FillChange
        {
            get { return fillChange; }
            set { fillChange = value; }
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
            var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            quad.transform.SetParent(transform);
            quad.transform.localPosition = new Vector3(0, 0, -.5f);
            quad.transform.localScale = new Vector3(Length, 1, 1);
            quad.transform.localRotation = Quaternion.Euler(90, 0, 0);
            quad.GetComponent<MeshRenderer>().material.color = GrassColor;

            for (var i = 0; i < Length; i++)
            {
                var offset = (Length - fillSize) / 2;
                var change = i > offset && i < Length - offset 
                    ? FillChange.Evaluate(FillChange.keys.Last().time / fillSize * (i - offset))
                    : 1;

                if (Utils.RollDice(1/change))
                {
                    var instance = transform.InstantiateChild(Prefabs[Random.Range(0, Prefabs.Length)].transform);
                    instance.localPosition = new Vector3(-Length / 2 + i, 0, 0);
                }
            }
        }
    }
}