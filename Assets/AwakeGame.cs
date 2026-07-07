using DG.Tweening;

using UnityEngine;

public class AwakeGame : MonoBehaviour
{
	[SerializeField]
	private BaseBlock _baseBlock;
	[SerializeField]
	private GameObject _GameStartInput;
	private void Start()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Append(_baseBlock.Getup())
			.AppendCallback(() =>
			{
				_GameStartInput.SetActive(true);
			});
	}
}
