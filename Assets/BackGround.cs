using DG.Tweening;
using UnityEngine;

public class BackGround : MonoBehaviour
{
	[SerializeField]
	BlockColor _BackgroundColor;
	[SerializeField]
	Renderer _render;
	[SerializeField]
	private float _time;

	private Tween _top,_bottom;
	
	void Start()
	{
		_render.material.SetColor("_BottomColor", Color.black);
		_render.material.SetColor("_TopColor", Color.black);
		float tmp = _time;
		_time = 0.3f;
		SetBackgroundColor();
		_time = tmp;
	}
	public void SetBackgroundColor()
	{
		Color a = _BackgroundColor.GetColor();
		_BackgroundColor.NextColor();
		Color b = _BackgroundColor.GetColor();

		if (_bottom != null)
			_bottom.Kill();
		if (_top != null)
			_top.Kill();

		_bottom =  DOTween.To(
			()=> _render.material.GetColor("_BottomColor"),
			(Color color) => _render.material.SetColor("_BottomColor", color),
			a,
			_time);
		_top = DOTween.To(
			() => _render.material.GetColor("_TopColor"),
			(Color color) => _render.material.SetColor("_TopColor", color),
			b,
			_time);
	}
	
}
