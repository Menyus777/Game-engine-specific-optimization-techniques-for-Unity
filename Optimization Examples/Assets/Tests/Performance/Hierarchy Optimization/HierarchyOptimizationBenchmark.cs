using System.Collections;
using Tests.Performance.Utilities;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class HierarchyOptimizationBenchmark
    {
        const float _warmupTime = 5f;
        const float _executionTime = 60f;

        [UnityTest, Performance]
        public IEnumerator Hierarchy_HeavyComposition_Benchmark()
        {
            // Loading the relevant scene
            SceneManager.LoadScene("Hierarchy Optimization - Heavy Composition");
            // Wait a frame for scene load - scenes are only loaded in the next frame
            // loading was initiated
            yield return null;

            // Small warmup before measurement starts
            yield return new WaitForSecondsRealtime(_warmupTime);

            using (Measure.Frames().Scope("Frame Time"))
            using (ScopedFPSMeasurement.StartFPSMeasurement("FPS"))
            {
                yield return new WaitForSecondsRealtime(_executionTime);
            }
        }

        [UnityTest, Performance]
        public IEnumerator Hierarchy_OptimizedComposition_Benchmark()
        {
            // Loading the relevant scene
            SceneManager.LoadScene("Hierarchy Optimization - Optimized Composition");
            // Wait a frame for scene load - scenes are only loaded in the next frame
            // loading was initiated
            yield return null;

            // Small warmup before measurement starts
            yield return new WaitForSecondsRealtime(_warmupTime);

            using (Measure.Frames().Scope("Frame Time"))
            using (ScopedFPSMeasurement.StartFPSMeasurement("FPS"))
            {
                yield return new WaitForSecondsRealtime(_executionTime);
            }
        }
    }
}
