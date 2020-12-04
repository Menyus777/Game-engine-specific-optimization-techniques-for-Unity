using UnityEngine;

namespace OptimizationExamples.PerformanceTestExamples
{
    [RequireComponent(typeof(Camera))]
    public class WaterRippleCamera : MonoBehaviour
    {

        Camera cam;
        public MeshRenderer waterPlane;

        void Awake()
        {
            cam = GetComponent<Camera>();
        }

        void Update()
        {
            waterPlane.sharedMaterial.SetVector("_CamPosition", transform.position);
            waterPlane.sharedMaterial.SetFloat("_OrthographicCamSize", cam.orthographicSize);
        }
    }
}
