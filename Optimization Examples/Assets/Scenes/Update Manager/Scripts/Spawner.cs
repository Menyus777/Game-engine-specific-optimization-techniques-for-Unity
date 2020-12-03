using UnityEngine;

namespace OptimizationExamples.UpdateManager
{
	/// <summary>
	/// Spawns the objects for UpdateManager scene
	/// </summary>
	public class Spawner : MonoBehaviour
	{
		/// <summary>
		/// Use this flags to change updating logic from manual to Unity
		/// </summary>
		public static bool UseUpdateManager { get; set; } = false;

		/// <summary>
		/// The prefab type that this script spawns
		/// </summary>
		[SerializeField]
		GameObject _objectToSpawn;

		void Start()
		{
			SpawnObjects(100);
		}

		/// <summary>
		/// Will spawn count * count - 1 objects in the scene
		/// </summary>
		public void SpawnObjects(int count)
		{
			for(int i = 0; i < count * 2; i = i + 2)
			{
				for (int j = 0; j < count * 2; j = j + 2)
				{
					if (i == 2 && j == 2)
						continue;
					var go = 
						Instantiate(_objectToSpawn,new Vector3(i, 0f, j), Quaternion.identity, transform);
					if (UseUpdateManager)
						go.AddComponent<ManagedMover>();
					else
						go.AddComponent<Mover>();
				}
			}
		}
	}
}
