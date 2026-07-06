using UnityEngine;

public class MakingMovingBlock : MonoBehaviour
{
	[SerializeField]
	private GameObject _movingCube;
	[SerializeField]
	private float _moveVelocity;
	public MovingBlock CreateMovingBlock(Vector3 blockCenter,Vector3 scale,bool isXaxis)
	{
		Vector3 blockPos = blockCenter;
		float moveDistance;
		int dir = 1;
		if (isXaxis)
		{
			blockPos.x -= scale.x+ scale.x/2;
			moveDistance = 3*scale.x;

		}
		else
		{
			blockPos.z -= scale.z+ scale.z/2;
			moveDistance = 3 * scale.z;

		}
		GameObject Cube = CreateCube(_movingCube,blockPos, scale);
		MovingBlock block = Cube.GetComponent<MovingBlock>();
		block.Initialized(isXaxis,_moveVelocity,moveDistance,dir);
		return block;
	}
	public GameObject CreateCube(GameObject cube,Vector3 blockCenter, Vector3 scale)
	{
		GameObject Cube = Instantiate(cube, blockCenter, Quaternion.identity);
		Cube.transform.localScale = scale;
		return Cube;
	}
}
