using DG.Tweening;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BackGround : MonoBehaviour
{
	[SerializeField]
	BlockColor _BackgroundColor;
	[SerializeField]
	Renderer _render;
	[SerializeField]
	private float _time;
	[SerializeField]
	Camera _camera;
	[SerializeField]
	private ParticleSystem _particleSystem;
	[SerializeField,Min(0)]
	int _changeCount;

	private Tween _top,_bottom;
	private int _curCount;
	private void Awake()
	{
		transform.rotation = _camera.transform.rotation;
		transform.position = _camera.transform.position+_camera.transform.forward*200;
		float height = 2*_camera.orthographicSize;
		float width = height * _camera.aspect;
		transform.localScale = new Vector3(width, height, 1);
		var shape = _particleSystem.shape;
		shape.scale= new Vector3(width, height, 1);
	}
	void Start()
	{
		_render.material.SetColor("_BottomColor", Color.black);
		_render.material.SetColor("_TopColor", Color.black);
		float tmp = _time;
		_time = 0.3f;
		ChangeColor();
		_time = tmp;
	}
	public void SetBackgroundColor()
	{
		_curCount++;
		if (_curCount != _changeCount)
		{
			return;
		}
		_curCount = 0;

		ChangeColor();
	}
	
	private void ChangeColor()
	{
		Color a = _BackgroundColor.GetColor();
		_BackgroundColor.NextColor();
		Color b = _BackgroundColor.GetColor();

		if (_bottom != null)
			_bottom.Kill();
		if (_top != null)
			_top.Kill();

		_bottom = DOTween.To(
			() => _render.material.GetColor("_BottomColor"),
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
