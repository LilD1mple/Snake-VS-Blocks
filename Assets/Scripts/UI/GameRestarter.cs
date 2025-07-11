using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnakeVSBlocks.UI
{
	public class GameRestarter : MonoBehaviour
	{
		public void RestartGame()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
