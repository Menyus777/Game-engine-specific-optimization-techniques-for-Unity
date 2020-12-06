using UnityEngine;

namespace OptimizationExamples.HierarchyOptimization
{
	public class NPCMover : MonoBehaviour
	{
		float _speed = 2f;

		void Update()
		{
			transform.Translate(Vector3.forward * Time.deltaTime * _speed);
		}
	}
}
