using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{

    public class HeathDrawer : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _health;
        private Damageable _target;
        private GameManager _gameManager;
        private float _startImageX;
        

        private void Start() {
            _gameManager = ServiceLocator.Current.Get<GameManager>();
            _startImageX = _image.rectTransform.sizeDelta.x;
        }
        public void SetTarget(Damageable damageable) {
            _target = damageable;
        }

        private void Update() {
            if (_target) {
                var camera = _gameManager.Camera;
                var pos = camera.WorldToScreenPoint(_target.transform.position);
                pos += _offset;
                transform.position = pos;
                var p = _target.Health.CurrentValue / _target.Health.MaxValue;
                var size = _image.rectTransform.sizeDelta;
                size.x = _startImageX * p;
                _health.text = ((int)_target.Health.CurrentValue).ToString();
                _image.rectTransform.sizeDelta = size;
            }
        }
    }
}