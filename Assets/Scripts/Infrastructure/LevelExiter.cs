using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnakeVSBlocks.Infrastructure
{
	public class LevelExiter : MonoBehaviour
	{
		private const int MenuIndex = 0;

		public void LoadMenu()
		{
			SceneManager.LoadScene(MenuIndex);
		}
	}
}