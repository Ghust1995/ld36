using UnityEngine;
using System.Collections;

public class CustomRigidbody : MonoBehaviour {

    // y speed
    private float _speed;

    public float Height;
    public float Gravity;
    public bool IsOnLevelEnd { get; private set; }

    public void Stop()
    {
        _speed = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    RaycastHit hitInfo;
	    if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, Height, ~(1<<8)))
        { 
	    
            IsOnLevelEnd = hitInfo.collider.gameObject.name == "LevelEnd";
            this.transform.position += (Height - hitInfo.distance)*Vector3.up;
            _speed = 0;
        }
	    else
	    {
            _speed -= Gravity * Time.deltaTime;
        }
	        
        transform.position += new Vector3(0.0f, _speed*Time.deltaTime, 0.0f);
	}
}
