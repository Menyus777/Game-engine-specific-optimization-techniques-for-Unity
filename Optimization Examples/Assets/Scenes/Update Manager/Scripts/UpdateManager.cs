using System.Collections.Generic;
using UnityEngine;

namespace OptimizationExamples.UpdateManager
{
    /// <summary>
    /// Updates the list of <see cref="ManagedMover"/> types manually
    /// </summary>
	public class UpdateManager
	{

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
                foreach (var mover in _updateables)
                {
                    mover.UpdateManager_Update();
                }
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
