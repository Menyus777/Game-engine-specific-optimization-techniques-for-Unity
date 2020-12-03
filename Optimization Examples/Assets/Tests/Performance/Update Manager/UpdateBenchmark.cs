using NUnit.Framework;
using OptimizationExamples.UpdateManager;
using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.Performance.UpdateManager
{
	public class UpdateBenchmark
	{

        [UnityTest, Performance]
        public IEnumerator UpdateManager_Benchmark()
        {
            SceneManager.LoadScene("Update Manager");
            Spawner.UseUpdateManager = true;

            var sampleGroup = new SampleGroup("Load Time", SampleUnit.Millisecond);

            // Wait a frame for scene load
            yield return null;

            

            // Wait a frame for scene load
            yield return null;

            // Small idling before measurement starts
            yield return new WaitForSecondsRealtime(2.0f);         
        }

    }
}
