using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

        private Material material;

        public Color Color
        {
            set { material.color = value; }
        }

        private void Awake()
        {
            material = meshRenderer.materials.First(m => m.name.StartsWith("Red"));
        }
    }
}