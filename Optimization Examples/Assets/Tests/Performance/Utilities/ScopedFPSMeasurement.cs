using System;
using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;

/// <summary>
/// An FPS Counter for inline yield instructions
/// </summary>
public class ScopedFPSMeasurement : IDisposable
{
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

    public static ScopedFPSMeasurement StartFPSMeasurement(string measurementScope)
    {
        ScopedFPSMeasurement scopedFpsMeasurement = new ScopedFPSMeasurement();
        scopedFpsMeasurement.IsCounting = true;
        _innerMonoBehaviour.StartCoroutine(FPSCounter());

        return scopedFpsMeasurement;

        IEnumerator FPSCounter()
        {
            float t = 0f;
            int ellapsedFrames = 0;
            while(scopedFpsMeasurement.IsCounting)
            {
                if(t > 1f)
                {
                    Measure.Custom(measurementScope, ellapsedFrames);
                    ellapsedFrames = 0;
                    t = 0f;
                }

                ellapsedFrames++;
                t += Time.deltaTime;
                yield return null;
            }
        }
    }

    public void StopFPSMeasurement()
    {
        IsCounting = false;
    }

    public void Dispose()
    {
        StopFPSMeasurement();
    }
}
