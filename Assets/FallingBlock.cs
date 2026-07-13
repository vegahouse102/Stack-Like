using UnityEngine;
using Enemy;
using Unity.VisualScripting;
public class FallingBlock : MonoBehaviour
{
	[SerializeField]
	private float _releaseY;
	[SerializeField]
	private Rigidbody _rigid;
	private void OnEnable()
	{
		transform.rotation = Quaternion.identity;
		_rigid.linearVelocity = Vector3.zero;

	}

	private void FixedUpdate()
	{
		if(_releaseY > transform.position.y)
		{
			PoolManager.Instance.ReleaseObject(gameObject);
		}
	}
}
