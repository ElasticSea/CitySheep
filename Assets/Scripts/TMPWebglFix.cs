using UnityEngine;

namespace Assets.Scripts
{
    public class TMPWebglFix : MonoBehaviour
    {
        private void Awake()
        {
            // Issue https://issuetracker.unity3d.com/issues/tmp-inputfield-throws-an-invalidoperationexception-on-webgl-with-net-4-dot-6
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                DestroyImmediate(gameObject);
            }
        }
    }
}