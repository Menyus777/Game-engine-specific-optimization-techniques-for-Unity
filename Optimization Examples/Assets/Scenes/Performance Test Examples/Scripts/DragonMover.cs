using UnityEngine;

namespace OptimizationExamples.PerformanceTestExamples
{
	public class DragonMover : MonoBehaviour
	{
		public float Speed = 7.5f;

		void Update()
		{
			transform.Translate(Vector3.forward * Time.deltaTime * Speed);
		}
	}
}
