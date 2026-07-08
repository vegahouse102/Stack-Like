using UnityEngine;
using DG.Tweening;
public class TextMove : MonoBehaviour
{
	[SerializeField]
	private Vector2 d;
	[SerializeField]
	private float moveTime;
	private Vector2 original;
	void Awake()
	{
		original = transform.position;
		transform.position += (Vector3)d;
	}
	public Tween StartMove()
	{
		return transform.DOMove(original,moveTime);
	}
	
}
