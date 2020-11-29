using System.Collections.Generic;
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
				SpawnFireBalls_initial(Random.Range(5, 25));
		}

		/// <summary>
		/// The first implemenation of spawn fire balls method
		/// </summary>
		/// <param name="count"></param>
		public void SpawnFireBalls_initial(int count)
		{
			for(int i = 0; i < count; i++)
			{
				Vector3 generatedPos = Random.onUnitSphere * 2.5f + _spawnPos.position;
				Instantiate(_fireBallPrefab, generatedPos, Quaternion.identity);
			}
		}

		/// <summary>
		/// The changes made by the intern collague
		/// </summary>
		/// <param name="count"></param>
		public void SpawnFireBalls_intern_changes(int count)
		{
			GameObject parent = new GameObject();
			for (int i = 0; i < count; i++)
			{
				Vector3 generatedPos = Random.onUnitSphere * 2.5f + _spawnPos.position;
				Instantiate(_fireBallPrefab, generatedPos, Quaternion.identity, parent.transform);
			}
			Destroy(parent, 3.0f);
		}

		// Ötletek: Fontos a jelent GPU bound
		// Az alap ami most van
		// Destory mindenre egy fos megoldás jött egy új kolléga érkezik aki kijavítja a scriptet jóhiszeműen de hát szar lesz
		// Destroy egy holder Go-ra
		// Objectpooling
	}
}
