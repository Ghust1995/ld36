using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnAtPoint : MonoBehaviour
{

    public List<Transform> SpawnPoints;

    public KeyCode NextLevel = KeyCode.E;
    public KeyCode PreviousLevel = KeyCode.Q;
    public KeyCode RestartLevel = KeyCode.R;

    public int CurrentLevel = 0;

    public int BestLevelYet = 0;

    public static bool ResetAll = false;

    // Use this for initialization
    void Start ()
	{
	    GoToSpawn();
	}


    void GoToSpawn()
    {
        this.transform.position = SpawnPoints[CurrentLevel].position;
        this.transform.rotation = SpawnPoints[CurrentLevel].rotation;
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }
        var fpsc = GetComponentInChildren<FirstPersonCamera>();
        if (fpsc != null)
        {
            fpsc.ResetRotation();
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(RestartLevel))
        {
            ResetAll = true;
        }
        if (ResetAll)
        {
            GoToSpawn();
            Invoke("StopReset", 0.1f);
        }


    }

    void StopReset()
    {
        ResetAll = false;
    }

    public void GoToNext()
    {
        GoToSpawn((CurrentLevel + 1 + SpawnPoints.Count)%SpawnPoints.Count);
    }

    public void GoToSpawn(int i)
    {
        if (i < SpawnPoints.Count)
        {
            CurrentLevel = i;
            if (CurrentLevel > BestLevelYet)
            {
                BestLevelYet = CurrentLevel;
            }
            GoToSpawn();
        }
    }
}
