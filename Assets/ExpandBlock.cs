using DG.Tweening;
using System;
using UnityEngine;

public class ExpandBlock : MonoBehaviour
{
	[SerializeField]
	private float _expandTime;
	[SerializeField]
	private AudioSource _audio;
	public event Action<GameObject> OnExpandEnd;
	
	public void Expand(bool isXAxis,float length)
	{
		Vector3 originPos = transform.position;
		Vector3 originSize = transform.localScale;
		Vector3 nextPos = originPos; 
		Vector3 nextSize = originSize;

		_audio.Play();



		if (isXAxis)
		{
			nextPos.x  = originPos.x - originSize.x / 2 + (length + originSize.x) / 2;
			nextSize.x += length;
		}
		else
		{
			nextPos.z = originPos.z - originSize.z / 2 + (length + originSize.z) / 2;
			nextSize.z += length;
		}

		Tween positionTween = DOTween.To(
				() => transform.position
				, (Vector3 pos) => transform.position = pos
				,nextPos
				,_expandTime
			);
		Tween sizeTween = DOTween.To(
				() => transform.localScale
				, (Vector3 scale) => transform.localScale = scale
				, nextSize
				, _expandTime
			);
		Debug.Log("Expand");
		Sequence sequence = DOTween.Sequence();
		sequence.Join( positionTween );
		sequence.Join(sizeTween);
		sequence.AppendCallback(()=>OnExpandEnd?.Invoke(gameObject));
	}
}
