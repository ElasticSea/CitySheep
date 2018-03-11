using System;
using System.Linq;
using Assets.Core.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class ForestLine : MonoBehaviour
    {
        [SerializeField] private int length = 20;
        [SerializeField] private Color grassColor = "0x6ad341".hexToColor();
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

        private void Start()
        {
            var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            quad.transform.SetParent(transform);
            quad.transform.localPosition = new Vector3(0, 0, -.5f);
            quad.transform.localScale = new Vector3(length, 1, 1);
            quad.transform.localRotation = Quaternion.Euler(90, 0, 0);
            quad.GetComponent<MeshRenderer>().material.color = GrassColor;

            for (var i = 0; i < length; i++)
            {
                var change = FillChange.Evaluate(FillChange.keys.Last().time / length * i);
                if (Random.value / change <= 1)
                {
                    var instance = transform.InstantiateChild(Prefabs[Random.Range(0, Prefabs.Length)].transform);
                    instance.localPosition = new Vector3(-length / 2 + i, 0, 0);
                }
            }
        }
    }
}