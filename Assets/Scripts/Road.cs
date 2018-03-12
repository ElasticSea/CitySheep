﻿using System.Collections;
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
            var ground = GameObject.CreatePrimitive(PrimitiveType.Quad);
            ground.transform.SetParent(transform);
            ground.transform.localPosition = new Vector3(0, 0, -.5f);
            ground.transform.localScale = new Vector3(length, 1, 1);
            ground.transform.localRotation = Quaternion.Euler(90,0,0);
            ground.GetComponent<MeshRenderer>().material.color = new Color(.2f, .2f, .2f);

            if (SurfaceMarking)
            {
                for (int i = 0; i < length; i++)
                {
                    if (i % 2 == 0)
                    {
                        var surfaceMarking = GameObject.CreatePrimitive(PrimitiveType.Quad);
                        surfaceMarking.transform.SetParent(transform);
                        surfaceMarking.transform.localPosition = new Vector3(i - length/2f, .001f, 0);
                        surfaceMarking.transform.localScale = new Vector3(1, .1f, 1);
                        surfaceMarking.transform.localRotation = Quaternion.Euler(90, 0, 0);
                        surfaceMarking.GetComponent<MeshRenderer>().material.color = new Color(.5f, .5f, .5f);
                    }
                }
            }
            var delay = (MaxDelay - MinDelay) * Random.value + MinDelay;

            var preloadTrafic = delay + length/minSpeed;
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

            var posA = transform.position + Vector3.left * 10 * Direction + Vector3.back/2;
            var posB = transform.position + Vector3.right * 10 * Direction + Vector3.back / 2;
            var duration = posB.Distance(posA) / speed;

            if (preloadTrafic < duration)
            {
                // Adjust starting position according to preloadTrafic
                var journeyProgress = preloadTrafic/duration;
                posA = (posB - posA) * journeyProgress + posA;

                var instance = transform.InstantiateChild(Cars.RandomElement().transform);
                instance.position = posA;
                instance.LookAt(posB);
                instance.DOMoveX(posB.x, posB.Distance(posA) / speed).SetEase(Ease.Linear).OnComplete(() => Destroy(instance.gameObject));
            }

            var interval = (MaxInterval - MinInterval) * Random.value + MinInterval;
            StartCoroutine(SpawnCar(interval, preloadTrafic));
        }
    }
}