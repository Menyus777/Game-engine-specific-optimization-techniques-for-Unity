using NUnit.Framework;
using OptimizationExamples.UpdateManagerExample;
using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.Performance.UpdateManagerExample
{
	public class UpdateBenchmark
	{

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
            yield return new WaitForSecondsRealtime(5.0f);

            UpdateManager.StopWatchStoppedCallback += () =>
                Measure.Custom(sampleGroup, UpdateManager.SW.Elapsed.TotalMilliseconds);

            yield return new WaitForSecondsRealtime(60.0f);

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

            // Small idling before measurement starts
            yield return new WaitForSecondsRealtime(5.0f);

            UpdateManager.StopWatchStoppedCallback += () =>
                Measure.Custom(sampleGroup, UpdateManager.SW.Elapsed.TotalMilliseconds);

            yield return new WaitForSecondsRealtime(60.0f);

            UpdateManager.StopWatchStoppedCallback = null;
        }

    }
}
