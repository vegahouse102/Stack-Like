using UnityEngine;
using TMPro;
using DG.Tweening;
public class RetryText : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _textMeshPro;
	[SerializeField]
	private float _waitTime;
	[SerializeField]
	private float _transitionTime;
	private void OnEnable()
	{
		_textMeshPro.color = new Color(1, 1, 1, 0);
		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(_waitTime);
		sequence.Append( _textMeshPro.DOColor(Color.white,_transitionTime));
		
	}
}
