using UnityEngine;
using System.Collections;

public class NoClip : MonoBehaviour {

    public bool IsActive = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.N))
	    {
	        IsActive = !IsActive;
	    }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Speed += 10;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Speed -= 10;
        }
        GetComponent<Rigidbody>().useGravity = !IsActive;
	    GetComponent<Collider>().enabled = !IsActive;
	    if (Input.GetKey(KeyCode.W) && IsActive)
	    {
	        transform.position += Time.deltaTime*Speed*transform.forward;
	    }
        if (Input.GetKey(KeyCode.S) && IsActive)
        {
            transform.position -= Time.deltaTime * Speed * transform.forward;
        }

    }

    public float Speed = 1;
}
