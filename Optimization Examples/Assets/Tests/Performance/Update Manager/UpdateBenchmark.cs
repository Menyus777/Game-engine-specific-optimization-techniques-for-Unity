using NUnit.Framework;
using OptimizationExamples.UpdateManagerExample;
using System.Collections;
using Tests.Performance.Utilities;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.Performance.UpdateManagerExample
{
	public class UpdateBenchmark
	{

        const float _warmupTime = 5f;
        const float _executionTime = 60f;

        [UnityTest, Performance]
        public IEnumerator UpdateManager_Benchmark()
        {
            Spawner.UseUpdateManager = true;
            SceneManager.LoadScene("Update Manager");

            var sampleGroup = 
                new SampleGroup("Update Time", SampleUnit.Millisecond);

            // Wait a frame for scene load
            yield return null;

            // Small idling before measurement starts
            yield return new WaitForSecondsRealtime(_warmupTime);

            UpdateManager.StopWatchStoppedCallback += () =>
                Measure.Custom(sampleGroup, UpdateManager.SW.Elapsed.TotalMilliseconds);

            yield return new WaitForSecondsRealtime(_executionTime);

            UpdateManager.StopWatchStoppedCallback = null;
        }

        [UnityTest, Performance]
        public IEnumerator TraditionalUpdate_Benchmark()
        {
            Spawner.UseUpdateManager = false;
            SceneManager.LoadScene("Update Manager");

            var sampleGroup =
                new SampleGroup("Update Time", SampleUnit.Millisecond);

            // Wait a frame for scene load
            yield return null;

            // Add the helper monos
            var go = new GameObject("Temp mono holder");
            go.AddComponent<UpdateBenchmarkHelperJustBefore>();
            go.AddComponent<UpdateBenchmarkHelperJustAfter>();

            // Small idling before measurement starts
            yield return new WaitForSecondsRealtime(_warmupTime);

            UpdateBenchmarkHelperJustAfter.StopWatchStoppedCallback += () =>
                Measure.Custom(
                    sampleGroup,
                    UpdateBenchmarkHelperJustAfter.SW.Elapsed.TotalMilliseconds);

            yield return new WaitForSecondsRealtime(_executionTime);

            UpdateBenchmarkHelperJustAfter.StopWatchStoppedCallback = null;
        }

    }
}
