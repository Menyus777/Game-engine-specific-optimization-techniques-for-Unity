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

		/// <summary>
		/// The button which loads an empty level that can be loaded fast
		/// </summary>
		public Button EmptyLevelButton { get => _emptyLevelButton; }
		[SerializeField]
		Button _emptyLevelButton;

		void Awake()
		{
			_loadDragonLevelButton.onClick.AddListener(() => loadLevel("Performance Test Examples - Dragon Level"));
			_emptyLevelButton.onClick.AddListener(() => loadLevel("Performance Test Examples - Empty Level"));
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
