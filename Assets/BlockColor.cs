
using UnityEngine;

public class BlockColor : MonoBehaviour
{
	[SerializeField]
	[Range(0,1f)]
	private float _changeColorSpeed = 0.03f;
	[SerializeField]
	[Range(0, 1f)]
	private float _s = 0.6f;
	[SerializeField]
	[Range(0, 1f)]
	private float _v = 0.95f;
	private float _hue;
	
	private void Awake()
	{
		_hue = Random.value;
	}

	public Color GetColor()
	{
		return Color.HSVToRGB(_hue,_s,_v);
	}
	public void NextColor()
	{
		_hue += _changeColorSpeed;
		if (_hue > 1)
			_hue -= 1;
	}
}
