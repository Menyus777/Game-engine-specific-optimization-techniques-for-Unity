#define ADD_LOADING_TIME_DELAY
#undef ADD_LOADING_TIME_DELAY
using UnityEngine;

namespace OptimizationExamples.PerformanceTestExamples
{
	/// <summary>
	/// Handles the movement of the dragon
	/// </summary>
	public class DragonMover : MonoBehaviour
	{
		/// <summary>
		/// The speed of the dragon
		/// </summary>
		public float Speed = 7.5f;

		void Awake()
		{
			// Simulated loading time for the first example in section 5.2
#if ADD_LOADING_TIME_DELAY
			// E.g. Imagine that we are doing a blocking network call here thus the delay
			System.Threading.Thread.Sleep(5000);
#endif
		}

		void Update()
		{
			transform.Translate(Vector3.forward * Time.deltaTime * Speed);
		}
	}
}
