using UnityEngine;
using System.Collections;


[RequireComponent(typeof(MeshRenderer))]
public class Movable : MonoBehaviour {

    void Reset()
    {
        GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Movable");
    }
}
