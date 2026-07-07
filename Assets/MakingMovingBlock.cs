using UnityEngine;

public class MakingMovingBlock : MonoBehaviour
{
	[SerializeField]
	private GameObject _movingCube;
	[SerializeField]
	private float _moveVelocity;
	[SerializeField]
	private BlockColor _blockColor;

	public MovingBlock CreateMovingBlock(Vector3 _startBlockPos, Vector3 _StartBlockScale,Vector3 blockCenter,Vector3 scale,bool isXaxis)
	{
		float moveDistance;
		Vector3 startPos = blockCenter;
		if (isXaxis)
		{
			startPos.x = _startBlockPos.x - _StartBlockScale.x- _StartBlockScale.x/2;
			moveDistance = 3 * _StartBlockScale.x;
		}
		else
		{
			startPos.z = _startBlockPos.z  - _StartBlockScale.z - _StartBlockScale.z / 2;
			moveDistance = 3 * _StartBlockScale.z;
		}
		GameObject Cube = CreateCube(_movingCube,startPos, scale);
		MovingBlock block = Cube.GetComponent<MovingBlock>();
		block.Initialized(isXaxis,_moveVelocity,moveDistance,1);
		return block;
	}
	public GameObject CreateCube(GameObject cube,Vector3 blockCenter, Vector3 scale)
	{
		GameObject Cube = Instantiate(cube, blockCenter, Quaternion.identity);
		Renderer renderer = Cube.GetComponent<Renderer>();
		renderer.material.color = _blockColor.GetColor();
		Cube.transform.localScale = scale;
		return Cube;
	}
}
