using UnityEngine;
using TMPro;
public class Counter : MonoBehaviour
{
	private int _curCount;
	[SerializeField]
	private TextMeshProUGUI _text;
	public void Init()
	{
		_text.gameObject.SetActive(true);
		_curCount = 0;
		_text.text = "0";
	}

	public void Count()
	{
		++_curCount;
		_text.text =  _curCount+"";
	}
}
