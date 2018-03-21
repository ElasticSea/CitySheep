using System.Collections;
using Assets.Core.Extensions;
using UnityEngine;

namespace Assets.Scripts.World
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private int length = 20;
        [SerializeField] private Car[] cars;
        [SerializeField] private Color[] colors;
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float minInterval;
        [SerializeField] private float maxInterval;
        [SerializeField] private float minDelay;
        [SerializeField] private float maxDelay;
        [SerializeField] private bool surfaceMarking;
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

        public bool SurfaceMarking
        {
            get { return surfaceMarking; }
            set { surfaceMarking = value; }
        }

        public Car[] Cars
        {
            get { return cars; }
            set { cars = value; }
        }

        public Color[] Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        private void Start()
        {
            var ground = GameObject.CreatePrimitive(PrimitiveType.Quad);
            ground.transform.SetParent(transform);
            ground.transform.localPosition = new Vector3(0, 0, -.5f);
            ground.transform.localScale = new Vector3(Length, 1, 1);
            ground.transform.localRotation = Quaternion.Euler(90,0,0);
            ground.GetComponent<MeshRenderer>().material.color = new Color(.2f, .2f, .2f);

            if (SurfaceMarking)
            {
                for (int i = 0; i < Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        var surfaceMarking = GameObject.CreatePrimitive(PrimitiveType.Quad);
                        surfaceMarking.transform.SetParent(transform);
                        surfaceMarking.transform.localPosition = new Vector3(i - Length/2f, .001f, 0);
                        surfaceMarking.transform.localScale = new Vector3(1, .1f, 1);
                        surfaceMarking.transform.localRotation = Quaternion.Euler(90, 0, 0);
                        surfaceMarking.GetComponent<MeshRenderer>().material.color = new Color(.5f, .5f, .5f);
                    }
                }
            }
            var delay = (MaxDelay - MinDelay) * Random.value + MinDelay;

            var preloadTrafic = delay + Length/minSpeed;
            StartCoroutine(SpawnCar(delay, preloadTrafic));
        }

        private IEnumerator SpawnCar(float delay = 0, float preloadTrafic = 0)
        {
            var speed = (MaxSpeed - MinSpeed) * Random.value + MinSpeed;

            // Substract wait time from preloadTrafic if possible
            if (delay <= preloadTrafic)
            {
                preloadTrafic -= delay;
            }
            else
            {
                yield return new WaitForSeconds(delay - preloadTrafic);
                preloadTrafic = 0;
            }

            var posA = transform.position + Vector3.left * length/2 * Direction + Vector3.back/2;
            var posB = transform.position + Vector3.right * length / 2 * Direction + Vector3.back / 2;
            var duration = posB.Distance(posA) / speed;

            if (preloadTrafic < duration)
            {
                // Adjust starting position according to preloadTrafic
                var journeyProgress = preloadTrafic/duration;
                posA = (posB - posA) * journeyProgress + posA;

                var car = transform.InstantiateChild(Cars.RandomElement());
                car.transform.position = posA;
                car.Destination = posB;
                car.Speed = speed;
                car.Color = Colors.RandomElement();
            }

            var interval = (MaxInterval - MinInterval) * Random.value + MinInterval;
            StartCoroutine(SpawnCar(interval, preloadTrafic));
        }
    }
}