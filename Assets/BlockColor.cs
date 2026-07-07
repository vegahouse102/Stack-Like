
using UnityEngine;

public class BlockColor : MonoBehaviour
{
	[SerializeField]
	[Range(0,1f)]
	private float _changeColorSpeed = 0.03f;
	private float _hue;
	
	private void Awake()
	{
		_hue = Random.value;
	}

	public Color GetColor()
	{
		return Color.HSVToRGB(_hue,0.6f,0.95f);
	}
	public void NextColor()
	{
		_hue += _changeColorSpeed;
		if (_hue > 1)
			_hue -= 1;
	}
}
