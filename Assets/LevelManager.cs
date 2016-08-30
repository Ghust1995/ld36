using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class LevelManager : MonoBehaviour
{
    public Player Player;
    public GameObject LunetaRotating;

    public List<Vector2> PreviewGrid;
    public RenderLevelPreview BaseLevelPreviewPrefab;
    public Transform PreviewGridBaseTransform;
    private List<RenderLevelPreview> _baseLevelPreviewObjects = new List<RenderLevelPreview>();

    private CustomRigidbody _customRigidbody;
    private SpawnAtPoint _playerLevelInfo;

    public bool FirstTimeOnHub = true;

    void MoveToLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            MoveToLayer(child, layer);
    }

    void OnApplicationQuit()
    {
        foreach (var baseLevelPreviewobject in _baseLevelPreviewObjects)
        {
            Destroy(baseLevelPreviewobject.gameObject);
        }
    }

    void Start()
    {
        _playerLevelInfo = Player.GetComponent<SpawnAtPoint>();
        _customRigidbody = Player.GetComponent<CustomRigidbody>();
        for (int i = 0; i < PreviewGrid.Count; i++)
        {
            var preview = (RenderLevelPreview)Instantiate(BaseLevelPreviewPrefab, Vector3.zero, Quaternion.identity);
            preview.transform.parent = PreviewGridBaseTransform;
            preview.transform.localRotation = Quaternion.identity;
            preview.transform.localPosition = new Vector3(PreviewGrid[i].x, 0.0f, PreviewGrid[i].y);
            preview.LevelView = (_playerLevelInfo.SpawnPoints[i+1].gameObject.GetComponent<LevelView>());
            preview.LevelNumber = i + 1;
            _baseLevelPreviewObjects.Add(preview);
        }
    }

    void FixedUpdate ()
	{
       

        LunetaRotating.SetActive(FirstTimeOnHub);
        Player.ScopeObject.SetActive(!FirstTimeOnHub);
        Player.enabled = !FirstTimeOnHub;

        for (int i = 0; i < _playerLevelInfo.SpawnPoints.Count; i++)
        {
            MoveToLayer(_playerLevelInfo.SpawnPoints[i].gameObject.transform,
                (_playerLevelInfo.CurrentLevel == i ? 0 : 8));
        }
        for (int i = 0; i < _baseLevelPreviewObjects.Count; i++)
        {
            _baseLevelPreviewObjects[i].gameObject.SetActive(i < _playerLevelInfo.BestLevelYet && !FirstTimeOnHub);
        }

        if (_customRigidbody.IsOnLevelEnd && Input.GetMouseButtonDown(0))
        {
            if(_playerLevelInfo.CurrentLevel == 0)
                _playerLevelInfo.GoToSpawn(_playerLevelInfo.BestLevelYet);
            else
                _playerLevelInfo.GoToNext();
            FirstTimeOnHub = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_playerLevelInfo.CurrentLevel == 0)
            {
                Debug.Log("Exiting game");
                Application.Quit();
            }
            else
            {
                _playerLevelInfo.GoToSpawn(0);
            }
        }

    }
}
