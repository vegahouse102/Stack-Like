using UnityEngine;
using DG.Tweening;
public class Bound : MonoBehaviour
{
	[SerializeField]
	private float _removeTime;
	[SerializeField]
	private float _expandSize;
	private void Awake()
	{
		
	}
	public void SetPos(Vector3 pos)
	{
		transform.position = pos;
	}
	public void SetSize(Vector3 scale)
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.size.Set(scale.x, scale.y);
		transform.localScale = new Vector3(scale.x,scale.z,scale.y);
	}
	public Sequence BasicEffect()
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(
			DOTween.To(
				() => spriteRenderer.color
			       , (Color color) => spriteRenderer.color = color
			       , new Color(1, 1, 1, 0)
			       , _removeTime
			).SetEase(Ease.OutSine)
		);
		sequence.AppendCallback(() => { GameObject.Destroy(gameObject); });
		return sequence;
	}
	public Sequence ExpandEffect()
	{
		Vector3 result = transform.localScale + Vector3.right * _expandSize+Vector3.up*_expandSize;
		Sequence sequence = DOTween.Sequence();
		sequence.Append(DOTween.To(
			() => transform.localScale,
			(Vector3 vec) => transform.localScale = vec,
			result,
			_removeTime
		));

		sequence.Join(BasicEffect());
		return sequence;
	}
}
