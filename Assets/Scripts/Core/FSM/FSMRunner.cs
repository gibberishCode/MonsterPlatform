using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public class FSMDependencieManager {
    private Dictionary<Type, object> _typeToObj = new Dictionary<Type, object>();

    public void Register<T>(T obj) {
        _typeToObj.Add(typeof(T), obj);
    }

    public void ConstructState(State state) {
        MethodInfo method = state.GetType().GetMethod("Construct");
        var parameters = method.GetParameters();
        object[] toPass = new object[parameters.Length];
        for (int i = 0; i < parameters.Length; i++) {
            ParameterInfo parameter = parameters[i];
            if (!_typeToObj.TryGetValue(parameter.ParameterType, out object obj)) {
                Debug.LogError($"{parameter.ParameterType} not registered in fsm dependnecy manager");
                return;
            }
            toPass[i] =  obj;
        }
        method.Invoke(state, toPass);
    }
}

public class FSMRunner : MonoBehaviour {
    protected State _currentState;
    public virtual Transform Transform => transform;


    public void SetState(State state) {
        if (_currentState != null) {
            _currentState.OnExit();
        }
        _currentState = state;
        _currentState.OnEnter();
    }

    private void Update() {
        if (_currentState == null) {
            return;
        }
        _currentState.Update();
    }
}