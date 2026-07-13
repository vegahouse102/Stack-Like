using UnityEngine;
using DG.Tweening;
public class Bound : MonoBehaviour
{
	[SerializeField]
	private float _removeTime;
	private void Awake()
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
	}
	public void SetPos(Vector3 pos)
	{
		transform.position = pos;
	}
	public void SetSize(Vector3 scale)
	{
		transform.localScale = new Vector3(scale.x,scale.z,scale.y);
	}
	public Sequence ExpandEffect()
	{
		Sequence sequence =  DOTween.Sequence();
		return sequence;
	}
}
