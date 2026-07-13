using TMPro;
using UnityEngine;

public class HighScoreText : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _text;
	void Start()
	{
		_text.text = ScoreManager.HighScore.ToString();
	}


}
