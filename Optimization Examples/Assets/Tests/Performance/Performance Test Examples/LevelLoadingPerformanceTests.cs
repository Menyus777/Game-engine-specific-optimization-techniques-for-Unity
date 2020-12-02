using NUnit.Framework;
using System.Collections;
using System.Diagnostics;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.Performance.PerformanceTestExamples
{
    /// <summary>
    /// Performance tests related to level loading
    /// </summary>
	public class LevelLoadingPerformanceTests
    {
        [UnitySetUp]
        public IEnumerator BeforeEach()
        {
            // Loading the relevant scene
            SceneManager.LoadScene("Performance Test Examples - Main Menu");

            // Wait a frame for scene load
            yield return null;
        }

        /// <summary>
        /// The levels that will be tested for loading times
        /// </summary>
        static readonly (string levelButton, string sceneName)[] _levels = 
            new (string, string)[]
            {
                ("Dragon Level - Button", "Performance Test Examples - Dragon Level"),
                ("Empty Level - Button", "Performance Test Examples - Empty Level"),
            };
        [UnityTest, Performance]
        public IEnumerator LevelsLoadUnder2Seconds_RecommendedConfig(
            [ValueSource("_levels")](string levelButton, string sceneName) level)
        {
            // Arrange
            var sampleGroup = new SampleGroup("Load Time", SampleUnit.Millisecond);
            var levelButton = GameObject.Find(level.levelButton).GetComponent<Button>();
            Stopwatch sw = new Stopwatch();
            // Stopping the stopwatch when the scene gets loaded
            SceneManager.sceneLoaded += (scene, loadMode) => sw.Stop();
            // Small idling before measurement starts
            yield return new WaitForSecondsRealtime(2.0f);

            // Act
            sw.Start();
            // Clicking on the appropriate scene button
            levelButton.onClick.Invoke();

            // Waiting till the scene loads
            yield return new WaitUntil(() => !sw.IsRunning);
            Measure.Custom(sampleGroup, sw.ElapsedMilliseconds);

            // Assert
            Assert.Less(sw.ElapsedMilliseconds, 2000,
                "Violation of OG_86650: " +
                "Levels should load under 2 seconds on the recommended configuration " +
                $"but \"{level.sceneName}\" loaded under {sw.ElapsedMilliseconds} " +
                "milliseconds");
        }

    }
}
