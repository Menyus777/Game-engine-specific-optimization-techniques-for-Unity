using NUnit.Framework;
using OptimizationExamples.ImportanceOfHashing;
using System;
using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.Performance.ImportanceOfHashing
{
	public class ShaderHashBenchmark
	{

        const float _warmupTime = 5f;
        const float _executionTime = 60f;

        [UnitySetUp]
        public IEnumerator BeforeAll()
        {
            // Loading the relevant scene
            SceneManager.LoadScene("Importance of Hashing");
            // Wait a frame for scene load - scenes are only loaded in the next frame
            // loading was initiated
            yield return null;
        }

        [UnityTest, Performance]
        public IEnumerator GetSetShaderPropertiesWithHash_Benchmark()
        {
            var sampleGroup =
                new SampleGroup("Execution Time", SampleUnit.Microsecond);

            var stylizedWater = 
                GameObject.Find("Water").AddComponent<StylizedWaterBehavior>();

            // Small warmup before measurement starts
            yield return new WaitForSecondsRealtime(_warmupTime);

            StylizedWaterBehavior.StopWatchStoppedCallback += () =>
                Measure.Custom(sampleGroup, StylizedWaterBehavior.SW.Elapsed.TotalMilliseconds * 1000);

            yield return new WaitForSecondsRealtime(_executionTime);

            StylizedWaterBehavior.StopWatchStoppedCallback = null;
        }

        [UnityTest, Performance]
        public IEnumerator GetSetShaderPropertiesWithString_Benchmark()
        {
            var sampleGroup =
                new SampleGroup("Execution Time", SampleUnit.Microsecond);

            var stylizedWater =
                GameObject.Find("Water").AddComponent<StylizedWaterBehaviorUnOptimized>();

            // Small warmup before measurement starts
            yield return new WaitForSecondsRealtime(_warmupTime);

            StylizedWaterBehaviorUnOptimized.StopWatchStoppedCallback += () =>
                Measure.Custom(sampleGroup, StylizedWaterBehaviorUnOptimized.SW.Elapsed.TotalMilliseconds * 1000);

            yield return new WaitForSecondsRealtime(_executionTime);

            StylizedWaterBehaviorUnOptimized.StopWatchStoppedCallback = null;
        }
    }
}
