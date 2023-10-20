using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerNotifier : MonoBehaviour
{
    [SerializeField] LayerMask _layer;
    [SerializeField] bool _useTriggers;
    public UnityEvent<GameObject> OnTriggerEnterEvent;
    public UnityEvent<GameObject> OnTriggerExitEvent;

    private void OnTriggerEnter(Collider other) {
        if (!_useTriggers && other.isTrigger) {
            return;
        }
        if ((1 << other.gameObject.layer & _layer.value) != 0) {
            OnTriggerEnterEvent?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!_useTriggers && other.isTrigger) {
            return;
        }
        if ((1 << other.gameObject.layer & _layer.value) != 0) {
            OnTriggerExitEvent?.Invoke(other.gameObject);
        }        
    }

    
}
