using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace OptimizationExamples.UpdateManagerExample
{
    /// <summary>
    /// Updates the list of <see cref="ManagedMover"/> types manually
    /// </summary>
	public static class UpdateManager
	{
        public static Stopwatch SW { get; private set; } = new Stopwatch();
        public static Action StopWatchStoppedCallback;

        static HashSet<ManagedMover> _updateables = new HashSet<ManagedMover>();

        /// <summary>
        /// Adds a <see cref="Mover"/> to the list of updateables
        /// </summary>
        /// <param name="obj">The object that will be added</param>
        public static void Add(ManagedMover mover)
        {
            _updateables.Add(mover);
        }

        /// <summary>
        /// Removes a <see cref="Mover"/> from the list of updateables
        /// </summary>
        /// <param name="obj">The object that will be removed</param>
        public static void Remove(ManagedMover mover)
        {
            _updateables.Remove(mover);
        }

        class UpdateManagerInnerMonoBehaviour : MonoBehaviour
        {
            void Update()
            {
                SW.Restart();
                foreach (var mover in _updateables)
                {
                    mover.UpdateManager_Update();
                }
                SW.Stop();
                StopWatchStoppedCallback?.Invoke();
            }
        }

        #region Static constructor and field for inner MonoBehaviour

        static UpdateManager()
        {
            var gameObject = new GameObject();
            _innerMonoBehaviour = gameObject.AddComponent<UpdateManagerInnerMonoBehaviour>();
#if UNITY_EDITOR
        gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
        _innerMonoBehaviour.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
#endif
        }
        static UpdateManagerInnerMonoBehaviour _innerMonoBehaviour;

        #endregion
    }
}
