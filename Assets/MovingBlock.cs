using System;
using Unity.VisualScripting;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
	private bool _isXaxis;
	private bool _isStop = false;
	private int _dir = 1;
	private float _moveDistance;
	private float _moveVelocity;
	private Vector3 _startPos;
	private Vector3 _farPos;
	private bool _isTargetStart;
	private Vector3 _target;
	private void Awake()
	{
		_startPos = transform.position;
	}
	public void Initialized(bool isXaxis,float moveVelocity, float moveDistance,int dir)
	{
		_moveDistance = moveDistance;
		_moveVelocity = moveVelocity ;
		_isXaxis = isXaxis;
		_dir = dir;
		_farPos = _startPos;
		if (_isXaxis)
		{
			_farPos.x += moveDistance;

		}
		else
		{
			_farPos.z += moveDistance;
		}

		_isTargetStart = false;
		_target = _farPos;
	}
	private void Update()
	{
		if (_isStop)
			return;
		if (ShouldTurn())
		{
			_dir = (_dir == 1) ? -1 : 1;
			if (_isTargetStart)
			{
				_target = _farPos;
			}
			else
			{
				_target = _startPos;
			}
			_isTargetStart = !_isTargetStart;
		}
		if (_isXaxis)
		{
			transform.position += Vector3.right * _moveVelocity *_dir* Time.deltaTime;
		}
		else
		{
			transform.position += Vector3.forward * _moveVelocity*_dir * Time.deltaTime;
		}
	}

	private bool ShouldTurn()
	{
		float curDistance = Vector3.Distance(transform.position, _target);
		Vector3 other = transform.position;
		if (_isXaxis)
		{
			other.x += _dir;
		}
		else
		{
			other.z += _dir;
		}
		return Vector3.Distance(other, _target) > curDistance;
	}

	public void Stop()
	{
		_isStop = true;
	}
	
}
