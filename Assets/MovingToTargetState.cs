using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class MovingToTargetState : IPlayerState
{
    private readonly Vector3 _targetPosition;
    private readonly Vector3 _initialPosition;
    private readonly float _initialFov;
    private float _time = 0;
    private Camera _camera;

    private float _percentageComplete;

    public MovingToTargetState(Vector3 initialPosition, Vector3 targetPosition, float initialFov)
    {
        _initialPosition = initialPosition;
        _targetPosition = targetPosition;
        _initialFov = initialFov;
    }

    public IPlayerState HandleInput(Player p)
    {
        if (_percentageComplete >= 1)
        {
            p.transform.position = p.MovingCamera.transform.position;
            p.MovingCamera.transform.localPosition = Vector3.zero;
            p.SetScopeObject(false);
            p.GetComponent<CustomRigidbody>().Stop();
            return new MovingMonocularState(MovingMonocularType.FromEye);
        }
        return null;
    }

    public void Enter(Player p)
    {
        _camera = p.ZoomCamera;
    }

    public void Update(Player p)
    {
        _time += Time.deltaTime;
        _percentageComplete = _time/p.BlinkAnimationTime;
        p.MovingCamera.transform.position = Vector3.Lerp(_initialPosition, _targetPosition, _percentageComplete);
        _camera.fieldOfView = Mathf.Lerp(_initialFov, p.DefaultFov, _percentageComplete);
    }
}
