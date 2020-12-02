using System;
using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;

/// <summary>
/// An scoped fps counter for yielding performance tests
/// </summary>
public class ScopedFPSMeasurement : IDisposable
{
    /// <summary>
    /// Tells if the current scope is counting the fps or not
    /// </summary>
    public bool IsCounting { get; private set; }

    static ScopedFPSMeasurement()
    {
        var gameObject = new GameObject();
        _innerMonoBehaviour = gameObject.AddComponent<InnerMonoBehaviour>();
#if !UNITY_EDITOR
        gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
        _innerMonoBehaviour.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
#endif
    }
    static InnerMonoBehaviour _innerMonoBehaviour;
    class InnerMonoBehaviour : MonoBehaviour { } 

    /// <summary>
    /// Starts a new scope for fps measurement
    /// </summary>
    /// <param name="measurementScope">The name of the scope</param>
    /// <returns>The fps measurement scope</returns>
    public static ScopedFPSMeasurement StartFPSMeasurement(string measurementScope)
    {
        ScopedFPSMeasurement scopedFpsMeasurement = new ScopedFPSMeasurement();
        scopedFpsMeasurement.IsCounting = true;
        _innerMonoBehaviour.StartCoroutine(FPSCounterCoroutine());

        return scopedFpsMeasurement;

        IEnumerator FPSCounterCoroutine()
        {
            var sampleGroup = new SampleGroup(measurementScope, SampleUnit.Undefined, true);
            float t = 0f;
            int ellapsedFrames = 0;
            while(scopedFpsMeasurement.IsCounting)
            {
                if(t > 1f)
                {
                    Measure.Custom(sampleGroup, ellapsedFrames);
                    ellapsedFrames = 0;
                    t = 0f;
                }

                ellapsedFrames++;
                t += Time.deltaTime;
                yield return null;
            }
        }
    }

    /// <summary>
    /// Stops the fps measurement
    /// </summary>
    public void StopFPSMeasurement()
    {
        IsCounting = false;
    }

    /// <summary>
    /// Closes the scope and stops the fps measurement
    /// </summary>
    public void Dispose()
    {
        StopFPSMeasurement();
    }
}
