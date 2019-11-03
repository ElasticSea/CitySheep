using UnityEditor;
using UnityEngine;

namespace Assets.Core.Scripts.Editor
{
    public class EnableWebglEmbeddedResources : MonoBehaviour
    {
        [MenuItem("WebGL/Enable Embedded Resources")]
        public static void EnableWebGLErrorMessageTesting()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            PlayerSettings.SetPropertyBool("useEmbeddedResources", true, BuildTargetGroup.WebGL);
#pragma warning restore CS0618 // Type or member is obsolete
        }
        [MenuItem("WebGL/Disnable Embedded Resources")]
        public static void dddnableWebGLErrorMessageTesting()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            PlayerSettings.SetPropertyBool("useEmbeddedResources", false, BuildTargetGroup.WebGL);
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}