using System;
using System.Net.WebSockets;
using MyUnityHelpers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIOption : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image _sprite;
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _text;
    public UnityEvent OnClicked;
    private UIOptionData _data;

    public void Init(UIOptionData data, Transform caller)
    {
        _sprite.sprite = data.Icon;
        _sprite.color = data.Color;
        _title.text = data.Title;
        _data = data;
        var resources = _data.GetResources();
        if (resources != null && resources.Count > 0)
        {
            _text.text = ConstructText();
        }
        else
        {
            _text.enabled = false;
        }
        OnClicked.AddListener(() => data.Select(caller));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var resourceManager = ServiceLocator.Current.Get<ResourceManager>();
        var resources = _data.GetResources();
        if (resourceManager.CanSpend(resources))
        {
            resourceManager.Spend(resources);
            OnClicked?.Invoke();
        }
    }

    private string ConstructText()
    {
        var str = "";
        foreach (var resource in _data.GetResources())
        {
            str += $"{resource.Type}: {Math.Ceiling(resource.Amount)} ";
        }
        return str;
    }
}