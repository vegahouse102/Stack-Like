using UnityEngine;
using DG.Tweening;
using TMPro;
public class TextMove : MonoBehaviour
{
	[SerializeField]
	private Vector2 d;
	[SerializeField]
	private float moveTime;
	[SerializeField]
	private TextMeshProUGUI _textMeshPro;
	private Vector2 original;
	
	void Awake()
	{
		original = transform.position;
		transform.position += (Vector3)d;
	}
	public Sequence StartMove()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Append(transform.DOMove(original, moveTime));
		_textMeshPro.color = new Color(0,0,0, 0.3882353f);
		Color resultColor = new Color(1,1,1, 0.3882353f);
		sequence.Join(DOTween.To(
			() => _textMeshPro.color,
			(Color color) => _textMeshPro.color = color,
			resultColor,
			moveTime
		));
		return sequence;
	}
	
}
