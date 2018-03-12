using System.Collections;
using Assets.Core.Extensions;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private int length = 20;
        [SerializeField] private GameObject[] cars;
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float minInterval;
        [SerializeField] private float maxInterval;
        [SerializeField] private float minDelay;
        [SerializeField] private float maxDelay;
        [SerializeField] private bool edge;
        [SerializeField] private int direction;

        public float MinSpeed
        {
            get { return minSpeed; }
            set { minSpeed = value; }
        }

        public float MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        public float MinInterval
        {
            get { return minInterval; }
            set { minInterval = value; }
        }

        public float MaxInterval
        {
            get { return maxInterval; }
            set { maxInterval = value; }
        }

        public float MinDelay
        {
            get { return minDelay; }
            set { minDelay = value; }
        }

        public float MaxDelay
        {
            get { return maxDelay; }
            set { maxDelay = value; }
        }

        public bool Edge
        {
            get { return edge; }
            set { edge = value; }
        }

        public GameObject[] Cars
        {
            get { return cars; }
            set { cars = value; }
        }

        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private void Start()
        {
            var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            quad.transform.SetParent(transform);
            quad.transform.localPosition = new Vector3(0, 0, -.5f);
            quad.transform.localScale = new Vector3(length, 1, 1);
            quad.transform.localRotation = Quaternion.Euler(90,0,0);
            quad.GetComponent<MeshRenderer>().material.color = new Color(.2f, .2f, .2f);

            if (Edge)
            {
                for (int i = 0; i < length; i++)
                {
                    if (i % 2 == 0)
                    {
                        var quad2 = GameObject.CreatePrimitive(PrimitiveType.Quad);
                        quad2.transform.SetParent(transform);
                        quad2.transform.localPosition = new Vector3(i - length/2f, .001f, 0);
                        quad2.transform.localScale = new Vector3(1, .1f, 1);
                        quad2.transform.localRotation = Quaternion.Euler(90, 0, 0);
                        quad2.GetComponent<MeshRenderer>().material.color = new Color(.5f, .5f, .5f);
                    }
                }
            }
            var delay = (MaxDelay - MinDelay) * Random.value + MinDelay;
            StartCoroutine(MoveCar(delay));
        }

        private IEnumerator MoveCar(float delay = 0)
        {
            var speed = (MaxSpeed - MinSpeed) * Random.value + MinSpeed;

            yield return new WaitForSeconds(delay);

            var instance = transform.InstantiateChild(Cars.RandomElement().transform);

            var posA = transform.position + Vector3.left * 10 * Direction + Vector3.back/2;
            var posB = transform.position + Vector3.right * 10 * Direction + Vector3.back / 2;

            instance.position = posA;
            instance.LookAt(posB);
            instance.DOMoveX(posB.x, posB.Distance(posA)/speed).SetEase(Ease.Linear);

            var interval = (MaxInterval - MinInterval) * Random.value + MinInterval;
            StartCoroutine(MoveCar(interval));
        }
    }
}