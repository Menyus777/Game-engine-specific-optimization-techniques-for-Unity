using OptimizationExamples.ImportanceOfHashing.Shaders;
using System;
using System.Diagnostics;
using UnityEngine;

namespace OptimizationExamples.ImportanceOfHashing
{
	public class StylizedWaterBehaviorUnOptimized : MonoBehaviour
	{
        public static Stopwatch SW { get; private set; } = new Stopwatch();
        public static Action StopWatchStoppedCallback;

        MeshRenderer _waterPlane;

        private void Awake()
        {
            _waterPlane = GetComponent<MeshRenderer>();
        }

        void Update()
        {
            SW.Restart();
            for (int i = 0; i < 250; i++)
            {
                var color = _waterPlane.material.GetColor("_Color");
                _waterPlane.material.SetColor("_Color", color);

                var fogColor = _waterPlane.material.GetColor("_FogColor");
                _waterPlane.material.SetColor("_FogColor", fogColor);

                var interColor = _waterPlane.material.GetColor("_IntersectionColor");
                _waterPlane.material.SetColor("_IntersectionColor", interColor);

                var foamThreshold = _waterPlane.material.GetFloat("_FoamThreshold");
                _waterPlane.material.SetFloat("_FoamThreshold", foamThreshold);
            }
            SW.Stop();
            StopWatchStoppedCallback?.Invoke();
        }
    }
}
