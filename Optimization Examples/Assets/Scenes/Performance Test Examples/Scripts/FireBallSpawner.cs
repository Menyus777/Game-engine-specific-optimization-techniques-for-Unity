using UnityEngine;

namespace OptimizationExamples.PerformanceTestExamples
{
	/// <summary>
	/// Shoots fireballs on the specified spawn position
	/// </summary>
	public class FireBallSpawner : MonoBehaviour
	{
		[SerializeField]
		Transform _spawnPos;

		[SerializeField]
		GameObject _fireBallPrefab;

		void Update()
		{
			if (Input.GetMouseButtonUp(0))
				SpawnFireBalls(Random.Range(5, 25));
		}

		/// <summary>
		/// The first implemenation of spawn fire balls method
		/// </summary>
		/// <param name="count"></param>
		public void SpawnFireBalls(int count)
		{
			for(int i = 0; i < count; i++)
			{
				Vector3 generatedPos = Random.onUnitSphere * 2.5f + _spawnPos.position;
				Destroy(Instantiate(_fireBallPrefab, generatedPos, Quaternion.identity), 5f);
			}
		}

	}
}
