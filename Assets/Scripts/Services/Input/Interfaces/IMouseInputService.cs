using UnityEngine;

namespace SnakeVSBlocks.Input
{
	public interface IMouseInputService
	{
		Vector2 GetClickDirection(Vector2 headPosition, Camera camera);
	}
}
