using NUnit.Framework;
using OptimizationExamples.PerformanceTestExamples;
using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class PerformanceTestExample
    {

        [UnitySetUp]
        public IEnumerator BeforeAll()
        {
            SceneManager.LoadScene("Performance Test Examples");
            // Wait a frame for scene load
            yield return null;

            _fireBallSpawner = GameObject.Find("Spawner").GetComponent<FireBallSpawner>();
        }

        FireBallSpawner _fireBallSpawner;
        [UnityTest, Performance]
        public IEnumerator SpawnFireBalls_Min_60_FPS()
        {
            // Small warmup before measurement starts
            yield return new WaitForSeconds(2.0f);
            // Simulating user input delay
            var wait = new WaitForSeconds(0.15f);
           
            using (Measure.Frames().Scope())
            using (ScopedFPSMeasurement.StartFPSMeasurement("FPS"))
            {
                for (int i = 0; i < 250; i++)
                {
                    _fireBallSpawner.SpawnFireBalls_intern_changes_improved(35);
                    yield return wait;
                }
            }

            PerformanceTest info = PerformanceTest.Active;
            info.CalculateStatisticalValues();
            var fps = info.SampleGroups.Find(s => s.Name == "FPS");

            Assert.GreaterOrEqual(fps.Median, 60, "The median FPS should be higher than 60 frames per second");

        }

    }
}
