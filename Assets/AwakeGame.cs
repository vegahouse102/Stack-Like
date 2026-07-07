using DG.Tweening;

using UnityEngine;

public class AwakeGame : MonoBehaviour
{
	[SerializeField]
	private BaseBlock _baseBlock;
	private void Start()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Append(_baseBlock.Getup());
	}
}
