using UnityEngine;
using DG.Tweening;
public class CameraMove : MonoBehaviour
{
	[SerializeField]
	private float _moveUpTime;
	public void MoveUp(float dy)
	{
		transform.DOKill();
		transform.DOMoveY(transform.position.y + dy, _moveUpTime).SetEase(Ease.OutCubic);
	}
}
