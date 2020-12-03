#define USE_UPDATE_MANAGER
#undef USE_UPDATE_MANAGER

using UnityEngine;

namespace OptimizationExamples.UpdateManager
{
	public class Mover : MonoBehaviour
	{

		float _speed;

		void Awake()
		{
			_speed = Random.Range(1.0f, 1.1f);
		}

#if !USE_UPDATE_MANAGER
		void Update()
		{
			moveUpAndDown();
		}
#endif
		public void UpdateManager_Update()
		{
			moveUpAndDown();
		}

		void moveUpAndDown()
		{
			var currPos = transform.position;
			transform.position = new Vector3(currPos.x, Mathf.PingPong(Time.time * _speed, 10f), currPos.z);
		}

	}
}
