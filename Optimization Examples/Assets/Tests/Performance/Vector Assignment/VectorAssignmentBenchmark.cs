using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;

namespace Tests.Performance.VectorAssignment
{
    public class VectorAssignmentBenchmark
    {
        const int _count = 50000;

        [Test, Performance]
        public void UnityVectorMultiplication_Benchmark()
        {
            Vector3[] input = new Vector3[_count];
            // Generating input vectors
            for (int i = 0; i < _count; i++)
            {
                input[i] = new Vector3(
                    Random.Range(0f, 2500f),
                    Random.Range(0f, 2500f),
                    Random.Range(0f, 2500f)
                    );
            }

            Measure.Method(() => _ = vectorMultiplication())
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(5)
                .Run();

            Vector3[] vectorMultiplication()
            {
                Vector3[] results = new Vector3[_count];

                for (int i = 0; i < _count; i++)
                {
                    results[i] = input[i] * 5f;
                }

                return results;
            }
        }

        [Test, Performance]
        public void CustomVectorMultiplication_Benchmark()
        {
            Vector3[] input = new Vector3[_count];
            // Generating input vectors
            for (int i = 0; i < _count; i++)
            {
                input[i] = new Vector3(
                    Random.Range(0f, 2500f),
                    Random.Range(0f, 2500f),
                    Random.Range(0f, 2500f)
                    );
            }

            Measure.Method(() => _ = vectorMultiplication())
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(5)
                .Run();

            Vector3[] vectorMultiplication()
            {
                Vector3[] results = new Vector3[_count];

                for (int i = 0; i < _count; i++)
                {
                    Vector3 curr = input[i];
                    Vector3 result;
                    result.x = curr.x * 5f;
                    result.y = curr.y * 5f;
                    result.z = curr.z * 5f;
                    results[i] = result;
                }

                return results;
            }
        }

    }
}
