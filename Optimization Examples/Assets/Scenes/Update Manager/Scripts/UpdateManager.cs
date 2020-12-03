using System.Collections.Generic;
using UnityEngine;

namespace OptimizationExamples.UpdateManager
{
    /// <summary>
    /// Updates the list of <see cref="Mover"/> types from the managed code
    /// </summary>
	public class UpdateManager
	{

        static HashSet<ManagedMover> _updatables = new HashSet<ManagedMover>();

        /// <summary>
        /// Adds a <see cref="Mover"/> to the list of updatables
        /// </summary>
        /// <param name="obj">The object that will be added</param>
        public static void AddMover(ManagedMover mover)
        {
            _updatables.Add(mover);
        }

        /// <summary>
        /// Removes a <see cref="Mover"/> from the list of updatables
        /// </summary>
        /// <param name="obj">The object that will be removed</param>
        public static void RemoveMover(ManagedMover mover)
        {
            _updatables.Remove(mover);
        }

        static UpdateManager()
        {
            var gameObject = new GameObject();
            _innerMonoBehaviour = gameObject.AddComponent<InnerMonoBehaviour>();
#if UNITY_EDITOR
        gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
        _innerMonoBehaviour.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
#endif
        }
        static InnerMonoBehaviour _innerMonoBehaviour;

        class InnerMonoBehaviour : MonoBehaviour 
        {
            void Update()
            {
                foreach (var mover in _updatables)
                {
                    mover.UpdateManager_Update();
                }
            }
        }
    }
}
