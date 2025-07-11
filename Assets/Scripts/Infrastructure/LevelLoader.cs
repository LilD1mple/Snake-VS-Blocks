using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnakeVSBlocks.Infrastructure
{
	public class LevelLoader : MonoBehaviour
	{
		private const int _mainSceneIndex = 1;

		public void LoadLevel()
		{
			SceneManager.LoadScene(_mainSceneIndex);
		}
	}
}