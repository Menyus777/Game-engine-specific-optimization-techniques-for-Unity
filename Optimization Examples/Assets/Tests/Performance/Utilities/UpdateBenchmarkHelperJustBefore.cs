using UnityEngine;

namespace Tests.Performance.Utilities
{
	public class UpdateBenchmarkHelperJustBefore : MonoBehaviour
	{
		void Update()
		{
			UpdateBenchmarkHelperJustAfter.SW.Restart();
		}
	}
}
