using NUnit.Framework;
using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;

namespace Tests
{
    public class PerformanceTestExample
    {
        // A Test behaves as an ordinary method
        [Test, Performance]
        public void PerformanceTestExampleSimplePasses()
        {
            Measure.Method(() => { }).Run();
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PerformanceTestExampleWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
