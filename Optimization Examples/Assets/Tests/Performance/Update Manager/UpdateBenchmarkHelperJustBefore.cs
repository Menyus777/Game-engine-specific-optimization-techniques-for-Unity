using UnityEngine;

namespace Tests.Performance.UpdateManagerExample
{
	public class UpdateBenchmarkHelperJustBefore : MonoBehaviour
	{
		void Update()
		{
			UpdateBenchmarkHelperJustAfter.SW.Restart();
		}
	}
}
