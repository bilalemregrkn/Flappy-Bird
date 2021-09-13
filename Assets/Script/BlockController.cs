using UnityEngine;

public class BlockController : MonoBehaviour
{
	[SerializeField] private Transform blockUp;
	[SerializeField] private Transform blockDown;
	[SerializeField] private Transform successCollider;

	public void Setup(float positionVertical)
	{
		var positionUp = blockUp.localPosition;
		positionUp.y = positionVertical;
		blockUp.localPosition = positionUp;

		var positionDown = blockDown.localPosition;
		positionDown.y = -positionVertical;
		blockDown.localPosition = positionDown;

		var scale = successCollider.localScale;
		scale.y = positionVertical;
		successCollider.localScale = scale;
	}
}