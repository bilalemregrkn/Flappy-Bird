using UnityEngine;

namespace Script
{
	public class FollowController : MonoBehaviour
	{
		[SerializeField] private Transform followObject;

		[SerializeField] private bool horizontal;
		[SerializeField] private bool vertical;

		private Vector2 _offset;

		private void Awake()
		{
			_offset = transform.position - followObject.position;
		}

		private void LateUpdate()
		{
			if (!horizontal && !vertical) return;

			var newPosition = followObject.position;

			var position = transform.position;

			if (horizontal)
			{
				position.x = newPosition.x;
				position.x += _offset.x;
			}

			if (vertical)
			{
				position.y = newPosition.y;
				position.y += _offset.y;
			}

			transform.position = position;
		}
	}
}