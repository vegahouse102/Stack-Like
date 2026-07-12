using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	[Header("Block Dimensions")]
	[SerializeField] private Vector3 _blockCenter;
	[SerializeField] private Vector3 _blockSize;
	[SerializeField] private bool _isXaxis;
	[SerializeField] private float _placementThreshold;
	[SerializeField] private float _expandBlockLength;

	[Header("References")]
	[SerializeField] private MakingMovingBlock _makingMovingBlock;
	[SerializeField] private CameraMove _cameraMove;
	[SerializeField] private StackEffect _stackEffect;

	[Header("Prefabs")]
	[SerializeField] private GameObject _fallingBlock;
	[SerializeField] private GameObject _notFallingBlock;

	private MovingBlock _curBlock;
	private Vector3 _startSize;
	private Vector3 _startPos;

	private bool _isGameStart;
	private bool _isFinish;
	private bool _isBlockExpand;

	[Header("Events")]
	public UnityEvent OnGameOver;
	public UnityEvent OnGameStart;
	public UnityEvent<PlaceBlockType> OnBlockPlace;

	private void Awake()
	{
		_startSize = _blockSize;
		_startPos = _blockCenter;
	}

	public void GameStart()
	{
		_isGameStart = true;
		OnGameStart?.Invoke();
		MakingMovingBlock();
	}

	public void ClickHandler()
	{
		// 1. 가드 클로즈: 현재 입력을 받으면 안 되는 예외 상황 필터링
		if (_isFinish || !_isGameStart || _isBlockExpand)
			return;

		_curBlock.Stop();

		// 2. 패배 조건 검사 (AABB 충돌 실패)
		if (!IsAABB())
		{
			GameOver();
			return;
		}

		// 3. 배치 성공 분기 (퍼펙트 vs 슬라이스)
		if (IsPerfectPlace())
		{
			HandlePerfectPlace();
		}
		else
		{
			HandleSlicedPlace();
		}
	}

	private bool IsPerfectPlace()
	{
		if (_curBlock == null) return false;
		return Vector3.Distance(_curBlock.transform.position, _blockCenter) < _placementThreshold;
	}

	private void HandlePerfectPlace()
	{
		GameObject notFallingBlock = _makingMovingBlock.CreateCube(_notFallingBlock, _blockCenter, _blockSize);
		_stackEffect.SetBoundEffect(_blockCenter, _blockSize);

		// 맥스 콤보(스택) 달성 시 블록 확장 연출 페이즈 진입
		if (_stackEffect.IsMaxStack())
		{
			StartBlockExpansion(notFallingBlock);
		}
		else
		{
			// 일반 퍼펙트인 경우 즉시 블록 배치 프로세스 종료
			OnBlockPlace?.Invoke(PlaceBlockType.Perfect);
			EndPlaceBlock();
		}
	}

	private void HandleSlicedPlace()
	{
		SliceCube();
		_stackEffect.StackInit();

		OnBlockPlace?.Invoke(PlaceBlockType.Sliced);
		EndPlaceBlock();
	}

	private void StartBlockExpansion(GameObject targetBlock)
	{
		ExpandBlock expand = targetBlock.GetComponent<ExpandBlock>();
		if (expand == null) return;

		// 연출 상태 돌입 및 기존 무빙 블록 파괴
		_isBlockExpand = true;
		Destroy(_curBlock.gameObject);

		// 현재 확장 축의 크기 및 최대 스케일 한도 계산
		float curAxisLength = _isXaxis ? targetBlock.transform.localScale.x : targetBlock.transform.localScale.z;
		float maxAxisLength = _isXaxis ? _startSize.x : _startSize.z;
		float expandLength = Mathf.Min(curAxisLength + _expandBlockLength, maxAxisLength) - curAxisLength;

		// 더 늘어날 공간이 있다면 확장 연출 비동기 콜백 실행
		if (!Mathf.Approximately(expandLength, 0))
		{
			expand.OnExpandEnd += HandleExpandBlockEnd; // 콜백 등록
			expand.Expand(_isXaxis, expandLength);
		}
		else
		{
			// 이미 최대 크기인 경우 연출 스킵하고 즉시 턴 종료
			_isBlockExpand = false;
			OnBlockPlace?.Invoke(PlaceBlockType.Perfect);
			EndPlaceBlock();
		}
	}

	private void HandleExpandBlockEnd(GameObject expandObject)
	{
		// 안전장치: 연출이 완전히 끝났으므로 중복 실행 방지를 위해 이벤트 구독 해제(-=)
		ExpandBlock expand = expandObject.GetComponent<ExpandBlock>();
		if (expand != null) expand.OnExpandEnd -= HandleExpandBlockEnd;

		// 확장이 완료된 최종 트랜스폼 데이터로 기준점 갱신
		_blockCenter = expandObject.transform.position;
		_blockSize = expandObject.transform.localScale;

		// 연출이 시각적으로 완벽히 끝난 시점에 이벤트를 터트리고 다음 턴 전환
		OnBlockPlace?.Invoke(PlaceBlockType.Perfect);
		EndPlaceBlock();
		_isBlockExpand = false;
	}

	private void EndPlaceBlock()
	{
		_isXaxis = !_isXaxis;
		Upper();
		MakingMovingBlock();
	}

	private void Upper()
	{
		_blockCenter.y += _blockSize.y;
		_cameraMove.MoveUp(_blockSize.y);
	}

	private void GameOver()
	{
		_isFinish = true;
		_makingMovingBlock.CreateCube(_fallingBlock, _curBlock.transform.position, _blockSize);
		Destroy(_curBlock.gameObject);
		OnGameOver?.Invoke();
	}

	public void MakingMovingBlock()
	{
		if (_curBlock != null) Destroy(_curBlock.gameObject);
		_curBlock = _makingMovingBlock.CreateMovingBlock(_startPos, _startSize, _blockCenter, _blockSize, _isXaxis);
	}

	#region [수학 연산 및 충돌 물리 연산 구역]
	private void SliceCube()
	{
		Vector3 blockPos = _curBlock.gameObject.transform.position;
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

		_makingMovingBlock.CreateCube(_fallingBlock, vremovePos, vremoveSize);
		_makingMovingBlock.CreateCube(_notFallingBlock, vremainPos, vremainSize);
		_blockCenter = vremainPos;
		_blockSize = vremainSize;
	}

	private (float deletedpos, float deletesize, float remainpos, float remainsize) GetSlicedCube(float movingpos, float basepos, float size)
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
			apos = (basepos + halfsize);
			bpos = (movingpos + halfsize);
		}

		deletedPos = (apos + bpos) / 2;
		deleteSize = bpos - apos;

		float remainPos = (movingpos + basepos) / 2;
		float remainSize;

		if (movingpos > basepos)
		{
			remainSize = (basepos + halfsize) - (movingpos - halfsize);
		}
		else
		{
			remainSize = (movingpos + halfsize) - (basepos - halfsize);
		}

		return (deletedPos, deleteSize, remainPos, remainSize);
	}

	private bool IsAABB()
	{
		Vector3 movingBlockPos = _curBlock.gameObject.transform.position;
		Vector3 baseBlockPos = _blockCenter;

		float movingBlockxMax = movingBlockPos.x + _blockSize.x / 2;
		float baseBlockxMax = baseBlockPos.x + _blockSize.x / 2;
		float movingBlockxMin = movingBlockPos.x - _blockSize.x / 2;
		float baseBlockxMin = baseBlockPos.x - _blockSize.x / 2;

		float movingBlockzMax = movingBlockPos.z + _blockSize.z / 2;
		float baseBlockzMax = baseBlockPos.z + _blockSize.z / 2;
		float movingBlockzMin = movingBlockPos.z - _blockSize.z / 2;
		float baseBlockzMin = baseBlockPos.z - _blockSize.z / 2;

		if (movingBlockxMax < baseBlockxMin) return false;
		if (movingBlockxMin > baseBlockxMax) return false;
		if (movingBlockzMax < baseBlockzMin) return false;
		if (movingBlockzMin > baseBlockzMax) return false;

		return true;
	}
	#endregion
}
public enum PlaceBlockType
{
	None,
	Perfect,
	Sliced
}