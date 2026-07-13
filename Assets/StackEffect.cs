using DG.Tweening;
using System;
using System.Drawing;
using UnityEngine;

public class StackEffect : MonoBehaviour
{
	[SerializeField]
	private GameObject _boundObj;
	[SerializeField]
	private float _diff;
	[SerializeField]
	private int _maxStack;
	[SerializeField]
	private AudioSource _stackAudio;

	private int _curStack;

	private float[] pitch =
	{
	    0f,
	    1.000000f, // 도
	    1.122462f, // 레
	    1.259921f, // 미
	    1.334840f, // 파
	    1.498307f, // 솔
	    1.681793f, // 라
	    1.887749f, // 시
	    2.000000f  // 높은 도
	};
	public void SetBoundEffect(Vector3 boxPos,Vector3 boxScale)
	{
		_curStack++;
		StackSound(_curStack);
		
		GameObject boundObj = Instantiate(_boundObj.gameObject);
		Bound bound = boundObj.GetComponent<Bound>();
		Vector3 size = boxScale;
		Vector3 pos = boxPos;
		pos.y -= boxScale.y/2;

		size.x += _diff;
		size.z += _diff;
		bound.SetPos(pos);
		bound.SetSize(size);
		bound.BasicEffect();

		int stackidx = (_curStack - 1) % 8;
		if (stackidx > 4)
		{
			Sequence sequence = DOTween.Sequence();
			int effectCount = stackidx - 4;
			for(int i = 0; i <effectCount; i++)
			{
				Sequence expandEffect = DOTween.Sequence();
				expandEffect.AppendInterval(0.2f*i);
				expandEffect.Append(SetExpandEffect(boxPos,boxScale));
				sequence.Join(expandEffect);
			}
		}
		//bound.ExpandEffect();
	}
	private Sequence SetExpandEffect(Vector3 boxPos, Vector3 boxScale)
	{
		Sequence sequence = DOTween.Sequence();
		GameObject boundObj = Instantiate(_boundObj.gameObject);
		Bound bound = boundObj.GetComponent<Bound>();
		boundObj.SetActive(false);
		Vector3 size = boxScale;
		size.x += _diff;
		size.z += _diff;
		boxPos.y -= boxScale.y/2;
		bound.SetPos(boxPos);
		bound.SetSize(size);
		sequence.AppendCallback(() =>
		{

			boundObj.SetActive(true);

		});

		sequence.Append(bound.ExpandEffect());
		

		return sequence;
	}
	private void StackSound(int stackCount)
	{
		int count = stackCount-1;
		_stackAudio.pitch = pitch[ count % 8+1];
		_stackAudio.Play();
	}

	public void StackInit()
	{
		_curStack = 0;
	}
	public bool IsMaxStack()
	{
		return _curStack >= _maxStack;
	}
}
