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

		void Update()
		{
			transform.Translate(Vector3.forward * Time.deltaTime * Speed);
		}
	}
}
