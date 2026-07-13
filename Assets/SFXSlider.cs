using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
	[SerializeField]
	AudioMixer _audioMixer;
	[SerializeField]
	Slider _slider;
	private void Awake()
	{
		if (_audioMixer.GetFloat("SFX", out float db))
		{
			_slider.value = Mathf.Pow(10f, db / 20f);
		}
	}

	public void SetSFXValue(float value01)
	{
		value01 = Mathf.Max(value01, 0.0001f);

		_audioMixer.SetFloat(
		    "SFX",
		    20f * Mathf.Log10(value01));
	}
}
