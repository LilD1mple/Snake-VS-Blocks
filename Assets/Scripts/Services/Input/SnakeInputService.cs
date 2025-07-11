using UnityEngine;

namespace SnakeVSBlocks.Input
{
	public sealed class SnakeInputService : IMouseInputService
	{
		public Vector2 GetClickDirection(Vector2 headPosition, Camera camera)
		{
			Vector3 mousePosition = camera.ScreenToViewportPoint(UnityEngine.Input.mousePosition);

			mousePosition.y = 1;

			mousePosition = camera.ViewportToWorldPoint(mousePosition);

			Vector2 direction = new(mousePosition.x - headPosition.x, mousePosition.y - headPosition.y);

			return direction;
		}
	}
}
