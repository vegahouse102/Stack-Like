using System;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private Vector3 _blockCenter;
	[SerializeField]
	private  Vector3 _blockSize;
	[SerializeField]
	private  bool _isXaxis;
	[SerializeField]
	private MakingMovingBlock _makingMovingBlock;
	[SerializeField]
	private GameObject _fallingBlock;
	[SerializeField]
	private GameObject _notFallingBlock;
	private MovingBlock _curBlock;


	private bool isGameStart;
	private bool isFinish = false;
	public void ClickHandler()
	{
		if (isFinish)
			return;
		if (!isGameStart)
		{
			isGameStart = true;
			MakingMovingBlock();
			return;
		}
		_curBlock.Stop();
		
		if (!IsAABB())
		{
			GameOver();
			return;
		}
		SliceCube();
		Upper();
		MakingMovingBlock();
	}

	private void Upper()
	{
		_blockCenter.y += _blockSize.y;
	}

	private void SliceCube()
	{
		Vector3 blockPos = _curBlock.gameObject.transform.position;
		GameObject.Destroy(_curBlock.gameObject);
		Vector3 vremovePos;
		Vector3 vremoveSize;
		Vector3 vremainPos;
		Vector3 vremainSize;
		if (_isXaxis)
		{
			(float removePos, float removeSize, float remainPos, float remainSize) = GetSlicedCube(blockPos.x, _blockCenter.x, _blockSize.x);
			vremovePos = new Vector3(removePos, _blockCenter.y, _blockCenter.z);
			vremoveSize = new Vector3(removeSize, _blockSize.y, _blockSize.z);
			vremainPos = new Vector3(remainPos, _blockCenter.y, _blockCenter.z);
			vremainSize = new Vector3(remainSize, _blockSize.y, _blockSize.z);
		}
		else
		{
			(float removePos, float removeSize, float remainPos, float remainSize) = GetSlicedCube(blockPos.z, _blockCenter.z, _blockSize.z);
			vremovePos = new Vector3(_blockCenter.x, _blockCenter.y, removePos);
			vremoveSize = new Vector3(_blockSize.x, _blockSize.y, removeSize);
			vremainPos = new Vector3(_blockCenter.x, _blockCenter.y, remainPos);
			vremainSize = new Vector3(_blockSize.x, _blockSize.y, remainSize);
		}
		GameObject fallingBlock = _makingMovingBlock.CreateCube(_fallingBlock,vremovePos,vremoveSize);
		GameObject notFallingBlock = _makingMovingBlock.CreateCube(_notFallingBlock, vremainPos, vremainSize);
		//_blockCenter = vremainPos;
		//_blockSize = vremainPos;
	}
	private (float deletedpos,float deletesize,float remainpos,float remainsize) GetSlicedCube(float movingpos,float basepos,float size)
	{
		float halfsize = size / 2;
		float deletedPos;
		float deleteSize;
		float apos;
		float bpos;
		if (movingpos < basepos)
		{
			apos = movingpos - halfsize;
			bpos = (basepos - halfsize);
			
		}
		else
		{
			 apos = (basepos +halfsize);
			bpos = (movingpos + halfsize);

		}
		deletedPos = (apos + bpos) / 2;
		deleteSize = bpos-apos;


		float remainPos = (movingpos + basepos) / 2 ;
		float remainSize ;
		if (movingpos > basepos)
		{
			remainSize = (basepos + halfsize) - (movingpos-halfsize);
		}
		else
		{
			remainSize = (movingpos + halfsize) - (basepos - halfsize);
		}
		return (deletedPos,deleteSize,remainPos,remainSize);
	}
	private void GameOver()
	{
		isFinish = true;
		Debug.Log("GameOver");
	}

	private bool IsAABB()
	{
		Vector3 movingBlockPos = _curBlock.gameObject.transform.position;
		Vector3 baseBlockPos = _blockCenter;
		float movingBlockxMax = movingBlockPos.x+_blockSize.x/2;
		float baseBlockxMax = baseBlockPos.x + _blockSize.x / 2;
		float movingBlockxMin = movingBlockPos.x - _blockSize.x / 2; ;
		float baseBlockxMin = baseBlockPos.x - _blockSize.x / 2; ;

		float movingBlockzMax = movingBlockPos.z + _blockSize.z / 2;
		float baseBlockzMax = baseBlockPos.z + _blockSize.z / 2;
		float movingBlockzMin = movingBlockPos.z - _blockSize.z / 2; 
		float baseBlockzMin = baseBlockPos.z - _blockSize.z / 2; 

		if (movingBlockxMax < baseBlockxMin)
			return false;
		if (movingBlockxMin > baseBlockxMax)
			return false;
		if (movingBlockzMax < baseBlockzMin)
			return false;
		if (movingBlockzMin > baseBlockzMax)
			return false;
		return true;
	}

	public void MakingMovingBlock()
	{
		_curBlock = _makingMovingBlock.CreateMovingBlock(_blockCenter,_blockSize,_isXaxis) ;
		_isXaxis = !_isXaxis;
	}
	
}
