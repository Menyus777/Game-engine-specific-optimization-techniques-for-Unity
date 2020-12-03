using UnityEngine;

namespace OptimizationExamples.UpdateManager
{
	public class Spawner : MonoBehaviour
	{
		[SerializeField]
		GameObject _objectToSpawn;

		void Start()
		{
			SpawnObjects(100, true);
		}

		/// <summary>
		/// Will spawn count*count objects in the scene
		/// </summary>
		public void SpawnObjects(int count, bool useUpdateManager = false)
		{
			for(int i = 0; i < count * 2; i = i + 2)
			{
				for (int j = 0; j < count * 2; j = j + 2)
				{
					if (i == 2 && j == 2)
						continue;
					var go = 
						Instantiate(_objectToSpawn,new Vector3(i, 0f, j), Quaternion.identity, transform);
					if (useUpdateManager)
						UpdateManager.AddMover(go.AddComponent<ManagedMover>());
					else
						go.AddComponent<Mover>();
				}
			}
		}
	}
}
