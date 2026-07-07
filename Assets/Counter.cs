using UnityEngine;

public class Counter : MonoBehaviour
{
	private int _curCount;
	
	public void Init()
	{
		_curCount = 0;
	}

	public void Count()
	{
		++_curCount;
	}
}
