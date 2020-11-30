using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OptimizationExamples.PerformanceTestExamples.UI
{
	/// <summary>
	/// Controllers the Main Menu
	/// </summary>
	public class MainMenuController : MonoBehaviour
	{
		/// <summary>
		/// The button which loads the infamous dragon level
		/// </summary>
		public Button LoadDragonLevelButton { get => _loadDragonLevelButton; }
		[SerializeField]
		Button _loadDragonLevelButton;

		void Awake()
		{
			// Initializing the Load Dragon Button
			_loadDragonLevelButton.onClick.AddListener(() => loadLevel("Performance Test Examples - Dragon Level"));
		}

		/// <summary>
		/// Loads the specified scene
		/// </summary>
		/// <param name="levelName">The name of the scene that will be loaded</param>
		void loadLevel(string levelName)
		{
			SceneManager.LoadScene(levelName, LoadSceneMode.Single);
		}
		
	}
}
