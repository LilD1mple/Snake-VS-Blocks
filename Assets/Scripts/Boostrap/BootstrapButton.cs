using UnityEngine;
using UnityEngine.EventSystems;

namespace SnakeVSBlocks.Bootstrap
{
	public class BootstrapButton : MonoBehaviour, IPointerDownHandler
	{
		[SerializeField] private Bootstrap _bootstrap;

		public void OnPointerDown(PointerEventData eventData)
		{
			_bootstrap.OnStartButtonClicked();
		}
	}
}