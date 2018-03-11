using Assets.Core.Extensions;
using UnityEngine;

namespace Assets.Scripts
{
    public class WorldBuilder : MonoBehaviour
    {
        [SerializeField] private ForestLine forestLinePrefab;
        [SerializeField] private Road roadLinePrefab;

        private void Awake()
        {
            createFullForest().transform.position = -4 * Vector3.forward;
            createFullForest().transform.position = -3 * Vector3.forward;
            createFullForest().transform.position = -2 * Vector3.forward;
            createFullForest().transform.position = -1 * Vector3.forward;

            int i = 0;
            transform.InstantiateChild(forestLinePrefab).transform.position = i++ * Vector3.forward;
            transform.InstantiateChild(forestLinePrefab).transform.position = i++ * Vector3.forward;
            transform.InstantiateChild(forestLinePrefab).transform.position = i++ * Vector3.forward;
            transform.InstantiateChild(forestLinePrefab).transform.position = i++ * Vector3.forward;

            var roadA = transform.InstantiateChild(roadLinePrefab);
            roadA.transform.position = i++ * Vector3.forward;
            roadA.Edge = true;

            var roadB = transform.InstantiateChild(roadLinePrefab);
            roadB.transform.position = i++ * Vector3.forward;
            roadB.Edge = false;

            transform.InstantiateChild(forestLinePrefab).transform.position = i++ * Vector3.forward;
            transform.InstantiateChild(forestLinePrefab).transform.position = i++ * Vector3.forward;

            roadA = transform.InstantiateChild(roadLinePrefab);
            roadA.transform.position = i++ * Vector3.forward;
            roadA.Edge = true;

            roadB = transform.InstantiateChild(roadLinePrefab);
            roadB.transform.position = i++ * Vector3.forward;
            roadB.Edge = false;

            transform.InstantiateChild(forestLinePrefab).transform.position = i++ * Vector3.forward;
            transform.InstantiateChild(forestLinePrefab).transform.position = i++ * Vector3.forward;
        }

        private ForestLine createFullForest()
        {
            var forest = transform.InstantiateChild(forestLinePrefab);
            forest.FillChange = AnimationCurve.Constant(0,1, 1f);
            return forest;
        }
    }
}