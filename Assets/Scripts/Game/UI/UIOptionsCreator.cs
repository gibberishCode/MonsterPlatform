using System;
using System.Collections.Generic;
using MyUnityHelpers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class UIOptionsCreator : MonoBehaviour, IGameService {
    [SerializeField] Camera _camera;
    [SerializeField] UIOption _uiOptionPrefab;
    [SerializeField] public float Offset;
    private List<Transform> _spawned = new List<Transform>();
    
    public void Open(List<UIOptionData> options, Transform caller) {
        _spawned.DestroyAll();
        _spawned.Clear();
        var offset = _camera.WorldToScreenPoint(caller.transform.position);
        var angle = 360.0f / options.Count;
        for (int i = 0; i < options.Count; i++) {
            UIOptionData option = options[i];
            var uiOption = Instantiate(_uiOptionPrefab, transform);
            uiOption.transform.position =
            offset + Quaternion.Euler(0, 0, angle * i) * Vector3.right * Offset;
            _spawned.Add(uiOption.transform);
            uiOption.Init(option, caller);
        }
    }

    internal void Close()
    {
        _spawned.DestroyAll();
        _spawned.Clear();
    }
}