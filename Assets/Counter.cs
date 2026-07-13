using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class Counter : MonoBehaviour
{
	private int _curCount;
	[SerializeField]
	private TextMeshProUGUI _text;

	public UnityEvent<int> OnChangeHighScore;
	public void Init()
	{
		//_text.gameObject.SetActive(true);
		_curCount = 0;
		_text.text = "0";
	}

	public void Count()
	{
		++_curCount;
		if(_curCount > 0)
			_text.gameObject.SetActive(true);
		_text.text =  _curCount+"";
	}
	public int GetCount()
	{
		return _curCount;
	}

	public void GameOver()
	{
		int highScore = ScoreManager.HighScore;
		if(highScore < _curCount)
		{
			ScoreManager.HighScore = _curCount;
			OnChangeHighScore?.Invoke(_curCount);
		}
	}
}
