using SnakeVSBlocks.Save;
using TMPro;
using UnityEngine;

namespace SnakeVSBlocks.UI.View
{
	public class ProgressViewer : MonoBehaviour
	{
		[Header("UI")]
		[SerializeField] private TMP_Text _progressText;

		private const string ProgressString = "LevelConfiguration";

		private const int LevelDefault = 0;

		private void Awake()
		{
			_progressText.text = $"Уровень {SaveUtility.LoadValue(ProgressString, LevelDefault) + 1}";
		}
	}
}
