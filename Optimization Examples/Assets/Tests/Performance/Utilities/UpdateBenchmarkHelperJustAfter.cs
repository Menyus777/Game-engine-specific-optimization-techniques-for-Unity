using System;
using System.Diagnostics;
using UnityEngine;

namespace Tests.Performance.Utilities
{
	public class UpdateBenchmarkHelperJustAfter : MonoBehaviour
	{
		public static Stopwatch SW { get; private set; } = new Stopwatch();
		public static Action StopWatchStoppedCallback;

		void Update()
		{
			SW.Stop();
			StopWatchStoppedCallback?.Invoke();
		}
	}
}
