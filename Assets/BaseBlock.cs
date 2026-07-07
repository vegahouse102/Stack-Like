using UnityEngine;
using DG.Tweening;
public class BaseBlock : MonoBehaviour
{
	private Vector3 _originalPos;

	void Awake()
	{
		_originalPos = transform.position;
		transform.position -= Vector3.up * 50;
		
	}

	public Tween Getup()
	{
		return transform.DOMoveY(_originalPos.y, 0.5f).SetEase(Ease.OutCubic);
	}
}
