using NUnit.Framework;
using OptimizationExamples.PerformanceTestExamples;
using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.Performance.PerformanceTestExamples
{
    public class FireBallSpawnerPerformanceTests
    {

        [UnitySetUp]
        public IEnumerator BeforeAll()
        {
            SceneManager.LoadScene("Performance Test Examples - Dragon Level");
            // Wait a frame for scene load
            yield return null;
        }

        [UnityTest, Performance]
        public IEnumerator SpawnFireBalls_RecommendedConfig_Min120FPS()
        {
            // Arrange
            var fireBallSpawner = GameObject.Find("Spawner").GetComponent<FireBallSpawner>();
            // Small warmup before measurement starts
            yield return new WaitForSecondsRealtime(2.0f);
            // Simulating user input delay
            var userInputDelayYieldInstruction = new WaitForSecondsRealtime(0.15f);
           
            // Act
            using (Measure.Frames().Scope("Frame Time"))
            using (ScopedFPSMeasurement.StartFPSMeasurement("FPS"))
            {
                for (int i = 0; i < 250; i++)
                {
                    fireBallSpawner.SpawnFireBalls_intern_changes(25);
                    yield return userInputDelayYieldInstruction;
                }
            }

            // Assert
            // Calculating the results for assertions
            PerformanceTest.Active.CalculateStatisticalValues();
            var fpsResults = PerformanceTest.Active.SampleGroups.Find(s => s.Name == "FPS");

            Assert.GreaterOrEqual(fpsResults.Median, 240,
                "Violation of OG_117650: The median FPS should be higher than 60 frames per second.");
        }

    }
}
