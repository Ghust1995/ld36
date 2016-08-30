using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Player : MonoBehaviour {

    private IPlayerState _state;

    public GameObject ScopeObject;
    
    public float DefaultFov = 60.0f;
    public float MinFov = 15.0f;
    public float MaxFov = 60.0f;
    public float MaxZoomDistance = 10.0f;

    public float ZoomSpeed;
    public float BlinkAnimationTime;

    public Camera ZoomCamera;
    public Camera MovingCamera;
    public Camera FirstPersonCamera;

    public Transform ScopeToEyeStart;
    public Transform ScopeToEyeEnd;
    public float MovineScopeToEyeAnimationTime;
    

    public void SetScopeObject(bool value)
    {
        FirstPersonCamera.GetComponent<DepthOfField>().enabled = value;
    }

    // Use this for initialization
	void Start ()
	{
        SetScopeObject(false);
        _state = new IdleState();
        _state.Enter(this);
    }
	
	// Update is called once per frame
	void Update ()
	{
        // TODO: Change keycode state
        // TODO: Use cool state system
	    var newState = _state.HandleInput(this);
	    if (newState != null)
	    {
            _state = newState;
            _state.Enter(this);
	    }

        _state.Update(this);
	}
}
