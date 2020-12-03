using UnityEngine;

namespace OptimizationExamples.UpdateManagerExample
{
	/// <summary>
	/// Moves the <see cref="GameObject"/> that it is attached to
	/// up and down with a random speed value
	/// </summary>
	public class Mover : MonoBehaviour
	{

		float _speed;

		void Awake()
		{
			_speed = Random.Range(1.0f, 1.1f);
		}

		void Update()
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
