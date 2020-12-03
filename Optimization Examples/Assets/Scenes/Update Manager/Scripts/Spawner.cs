using UnityEngine;

namespace OptimizationExamples.UpdateManager
{
	public class Spawner : MonoBehaviour
	{
		[SerializeField]
		GameObject _objectToSpawn;

		void Start()
		{
			SpawnObjects(100);
		}

		/// <summary>
		/// Will spawn count*count objects in the scene
		/// </summary>
		public void SpawnObjects(int count)
		{
			for(int i = 0; i < count * 2; i = i + 2)
			{
				for (int j = 0; j < count * 2; j = j + 2)
				{
					if (i == 2 && j == 2)
						continue;
					Instantiate(_objectToSpawn, new Vector3(i, 0f, j), Quaternion.identity);
				}
			}
		}

	}
}
