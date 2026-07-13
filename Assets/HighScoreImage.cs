using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
public class HighScoreImage : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI _text;
	[SerializeField]
	Image _image;
	[SerializeField]
	private float _waitTime;
	[SerializeField]
	private float _transitionTime;
	private void OnEnable()
	{
		_text.text = ScoreManager.HighScore.ToString();
		_image.color = new Color(1, 1, 1, 0);
		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(_waitTime);
		sequence.Append(_image.DOColor(Color.white,_transitionTime));
	}
	public void SetHightScore(int score)
	{
		_text.text = score.ToString();
	}
}
