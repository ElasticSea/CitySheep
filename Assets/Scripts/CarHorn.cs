using UnityEngine;

namespace Assets.Scripts
{
    public class CarHorn : MonoBehaviour
    {
        [SerializeField] private AudioSource honkSound;
        [SerializeField] private float honkInterval = 1;
        private float lastHonkTime;

        private void Update()
        {
            var posA = transform.position + Vector3.up * .5f ;
            var posB = posA + transform.forward;

            var itIsTimeToHonk = lastHonkTime + honkInterval < Time.time;
            var isSomethingInFront = Physics.Linecast(posA, posB, LayerMask.GetMask("Player"));

            if (itIsTimeToHonk && isSomethingInFront)
            {
                honkSound.Play();
                lastHonkTime = Time.time;
            }
        }

        private void OnDrawGizmos()
        {
            var posA = transform.position + Vector3.up * .5f;
            var posB = posA + transform.forward;

            var isSomethingInFront = Physics.Linecast(posA, posB);

            Gizmos.color = isSomethingInFront ? Color.blue : Color.red;
            Gizmos.DrawLine(posA, posB);
            Gizmos.DrawSphere(posB, .1f);
        }
    }
}