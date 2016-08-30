using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LookingAtMonocularState : IPlayerState
{
    private const float ZoomSpeed = 0.1f;
    private Camera _camera;
    private float _focalDistance;
    private GameObject _movableObject = null;

    private int _newLevel = 0;

    public IPlayerState HandleInput(Player p)
    {
        if (Input.GetMouseButtonDown(1))
        {
            var a = Mathf.Tan(0.5f*_camera.fieldOfView*Mathf.PI/180);
            var b = Mathf.Tan(0.5f*p.DefaultFov*Mathf.PI/180);
            var desl = _focalDistance*(1 - a/b);
            var _targetPos = p.transform.position + p.FirstPersonCamera.transform.forward*desl;
            if (_focalDistance <= 0)
            {
                p.SetScopeObject(false);
                _camera.fieldOfView = p.DefaultFov;
                return new MovingMonocularState(MovingMonocularType.FromEye);
            }
            if (_movableObject != null)
            {
                _movableObject.transform.position -= (_targetPos - p.transform.position);
                return new MovingToTargetState(p.transform.position, p.transform.position, _camera.fieldOfView);
            }
            if (_newLevel != 0)
            {
                p.GetComponent<SpawnAtPoint>().GoToSpawn(_newLevel);
                SpawnAtPoint.ResetAll = true;
                return new MovingToTargetState(p.transform.position, p.transform.position, _camera.fieldOfView);
            }
            return new MovingToTargetState(p.transform.position, _targetPos, _camera.fieldOfView);
        }
        return null;
    }

    public void Enter(Player p)
    {
        p.SetScopeObject(true);
        _camera = p.ZoomCamera;
        _focalDistance = 0.0f;
    }

    private float _deltaZoom = 0;
    public void Update(Player p)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hitInfo, Mathf.Infinity, ~(1<<8)))
        {
            _focalDistance = hitInfo.distance;
            _movableObject = hitInfo.collider.GetComponent<Movable>() ?
                hitInfo.collider.gameObject : null;
            var levelprev = hitInfo.collider.GetComponent<RenderLevelPreview>();
            _newLevel = levelprev ? levelprev.LevelNumber : 0;
        }
        else
        {
            _focalDistance = 0.0f;
            _movableObject = null;
        }


        // Change fov to zoom in;
        if((_camera.fieldOfView < p.MaxFov && Input.mouseScrollDelta.y < 0) ||
            (_camera.fieldOfView > p.MinFov && Input.mouseScrollDelta.y > 0))
            _deltaZoom += Input.mouseScrollDelta.y * p.ZoomSpeed;

        if (_deltaZoom < -p.MaxZoomDistance)
            _deltaZoom = -p.MaxZoomDistance;
        
        _camera.fieldOfView = Mathf.Clamp(2 * (180/Mathf.PI) * Mathf.Atan(
                        (p.MaxZoomDistance * Mathf.Tan(0.5f * p.DefaultFov * Mathf.PI / 180)) / 
                        (p.MaxZoomDistance + _deltaZoom)),p.MinFov , p.MaxFov);
    }
}
