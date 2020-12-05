using OptimizationExamples.ImportanceOfHashing.Shaders;
using System;
using System.Diagnostics;
using UnityEngine;

namespace OptimizationExamples.ImportanceOfHashing
{
    public class StylizedWaterBehavior : MonoBehaviour
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
            for(int i = 0; i < 250; i++)
            {
                var color = _waterPlane.material.GetColor(StylizedWater._Color);
                _waterPlane.material.SetColor(StylizedWater._Color, color);

                var fogColor = _waterPlane.material.GetColor(StylizedWater._FogColor);
                _waterPlane.material.SetColor(StylizedWater._FogColor, fogColor);

                var interColor = _waterPlane.material.GetColor(StylizedWater._IntersectionColor);
                _waterPlane.material.SetColor(StylizedWater._IntersectionColor, interColor);

                var foamThreshold = _waterPlane.material.GetFloat(StylizedWater._FoamThreshold);
                _waterPlane.material.SetFloat(StylizedWater._FoamThreshold, foamThreshold);
            }
            SW.Stop();
            StopWatchStoppedCallback?.Invoke();
        }
    }

}
