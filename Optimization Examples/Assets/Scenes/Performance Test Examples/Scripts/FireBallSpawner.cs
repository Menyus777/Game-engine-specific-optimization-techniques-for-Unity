using System.Collections.Generic;
using UnityEngine;

namespace OptimizationExamples.PerformanceTestExamples
{
	public class FireBallSpawner : MonoBehaviour
	{
		[SerializeField]
		Transform _spawnPos;

		public GameObject _fireBallPrefab;

		void Update()
		{
			if (Input.GetMouseButtonUp(0))
				SpawnFireBalls(Random.Range(5, 25));
		}

		public void SpawnFireBalls(int count)
		{
			for(int i = 0; i < count; i++)
			{
				Vector3 generatedPos = Random.onUnitSphere * 2.5f + _spawnPos.position;
				Instantiate(_fireBallPrefab, generatedPos, Quaternion.identity);
				//Destroy(Instantiate(_fireBallPrefab, generatedPos, Quaternion.identity), 2.0f);
			}
		}
	}
}
