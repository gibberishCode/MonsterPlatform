using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Player _player;
    [SerializeField] private Joystick _joystick;


    private void Update() {
        _player.SetDirection(new Vector3(_joystick.Horizontal, 0, _joystick.Vertical));
    }
}
