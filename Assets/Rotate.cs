using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Rotate : MonoBehaviour
{

    public float Speed;

    void Update () {
	    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0.0f, Speed*Time.deltaTime, 0.0f));
	}
}
