using SnakeVSBlocks.Snakes;
using TMPro;
using UnityEngine;

namespace SnakeVSBlocks.UI.View
{
	public class SnakeScoreViewer : MonoBehaviour
	{
		[SerializeField] private Snake _snake;
		[SerializeField] private TMP_Text _snakeScoreText;

		private void OnEnable()
		{
			_snake.SnakeSegmentUpdated += OnSnakeScoreChanged;
		}

		private void OnDisable()
		{
			_snake.SnakeSegmentUpdated -= OnSnakeScoreChanged;
		}

		private void OnSnakeScoreChanged(int snakeScore)
		{
			_snakeScoreText.text = snakeScore.ToString();
		}
	}
}
