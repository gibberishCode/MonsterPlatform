using UnityEngine;



public abstract class State : ScriptableObject {
    protected FSMRunner _runner;
    
    // public void Construct(FSMRunner runner) {
    //     _runner = runner;
    // }

    public virtual void OnEnter() {}
    public virtual void OnExit() {}
    public virtual void Update() {}
    public virtual void FixedUpdate() {}
}