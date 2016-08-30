using System;
using UnityEngine;
using System.Collections;

public class RenderLevelPreview : MonoBehaviour
{
    public LevelView LevelView;
    public MeshRenderer MeshToRender;

    private bool _textureAssigned = false;
    public int LevelNumber;

    // Use this for initialization
	void Start ()
    {
        StartCoroutine(Render());
    }



    public IEnumerator Render()
    {
            yield return new WaitUntil(() => LevelView.TextureLoaded);

        MeshToRender.material = new Material(Shader.Find(" Diffuse"));
        MeshToRender.material.mainTexture = Instantiate(LevelView.PreviewTexture);
    }
}
