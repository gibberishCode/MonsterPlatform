using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    public UnityEvent<Collider> OnTriggerEnterEvent;

    // private void OnTriggerEnter(Collider other) {
    //     OnTriggerEnterEvent<Collider>()
    // }

}
