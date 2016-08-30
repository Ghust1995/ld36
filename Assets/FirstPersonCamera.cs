using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirstPersonCamera : MonoBehaviour
{
    public float BaseSensitivity = 15F;

    private float sensitivity = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationX = 0F;
    float rotationY = 0F;

    private List<float> rotArrayX = new List<float>();
    float rotAverageX = 0F;

    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0F;

    public float frameCounter = 20;

    Quaternion originalRotation;
    private Quaternion startRotation;

    public Player player;
    private float _defaultFov;
    private Camera _camera;

    // Use this for initialization
    void Start () {
        originalRotation = transform.localRotation;
        startRotation = originalRotation;
        _defaultFov = player.DefaultFov;
        _camera = player.ZoomCamera;
        Cursor.lockState = CursorLockMode.Locked;
    }
    //http://wiki.unity3d.com/index.php/SmoothMouseLook
    // Update is called once per frame

    public void ResetRotation()
    {
        Input.ResetInputAxes();
        originalRotation = Quaternion.identity;
        rotArrayY = new List<float>();
        rotArrayX = new List<float>();
        rotationY = 0;
        rotationX = 0;
    }

    void Update ()
	{ 
        sensitivity = BaseSensitivity * _camera.fieldOfView/ _defaultFov;

        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.visible = !Cursor.visible;
        }        

        rotAverageY = 0f;
        rotAverageX = 0f;

        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        rotationX += Input.GetAxis("Mouse X") * sensitivity;

        rotArrayY.Add(rotationY);
        rotArrayX.Add(rotationX);

        if (rotArrayY.Count >= frameCounter)
        {
            rotArrayY.RemoveAt(0);
        }
        if (rotArrayX.Count >= frameCounter)
        {
            rotArrayX.RemoveAt(0);
        }

        foreach (var t in rotArrayY)
        {
            rotAverageY += t;
        }
	    foreach (var t in rotArrayX)
	    {
	        rotAverageX += t;
	    }

	    rotAverageY /= rotArrayY.Count;
        rotAverageX /= rotArrayX.Count;

        rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
        rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

        Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

        transform.localRotation = originalRotation * xQuaternion * yQuaternion;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}
