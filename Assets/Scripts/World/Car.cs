using System.Linq;
using DG.Tweening;
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

        public Vector3 Destination { private get; set; }
        public float Speed { private get; set; }

        private void Awake()
        {
            material = meshRenderer.materials.First(m => m.name.StartsWith("Red"));
            gameObject.AddComponent<WeirdAudioChatterFix>();
        }

        private void Update()
        {
            transform.LookAt(Destination);
            var direction = (Destination - transform.position);
            if (direction.magnitude > Speed * Time.deltaTime)
            {
                transform.position += direction.normalized * Speed * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}