using UnityEngine;

public class StackEffect : MonoBehaviour
{
	[SerializeField]
	private GameObject _boundObj;
	[SerializeField]
	private float d;
	[SerializeField]
	private int _maxStack;


	private int _curStack;
	public void SetBoundEffect(Vector3 boxPos,Vector3 boxScale)
	{
		_curStack++;
		Vector3 size = boxScale;
		size.x += d;
		size.z += d;
		GameObject boundObj = Instantiate(_boundObj.gameObject);
		Bound bound = boundObj.GetComponent<Bound>();
	
		bound.SetPos(boxPos);
		bound.SetSize(size);
	}
	public void StackInit()
	{
		_curStack = 0;
	}
	public bool IsMaxStack()
	{
		return _curStack >= _maxStack;
	}
}
