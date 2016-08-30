using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class LevelView : MonoBehaviour
{
    public RenderTexture TheTexture { get; private set; }
    public bool TextureLoaded { get; set; }
    public Texture2D PreviewTexture;

    void Reset()
    {
        var viewCamera = GetComponent<Camera>();
        viewCamera.clearFlags = CameraClearFlags.SolidColor;
        viewCamera.backgroundColor = new Color(0.0f, 0.0f, 0.0f);
        viewCamera.cullingMask = (1 << 8);
        viewCamera.targetDisplay = -1;
        viewCamera.targetTexture = new RenderTexture(256, 256, 24);
        Render();

    }

    void Start()
    {
        //Hopefully finds the component
        StartCoroutine(Render());
    }

    public IEnumerator Render()
    {
        yield return new WaitForEndOfFrame();
        var viewCamera = GetComponent<Camera>();

        var CurrentRT = RenderTexture.active;
        RenderTexture.active = viewCamera.targetTexture;
        PreviewTexture = new Texture2D(viewCamera.targetTexture.width, viewCamera.targetTexture.height,
            TextureFormat.RGB24, false);
        PreviewTexture.ReadPixels(new Rect(0, 0, viewCamera.targetTexture.width, viewCamera.targetTexture.height), 0, 0);
        RenderTexture.active = CurrentRT;
        TextureLoaded = true;
    }

   
}
