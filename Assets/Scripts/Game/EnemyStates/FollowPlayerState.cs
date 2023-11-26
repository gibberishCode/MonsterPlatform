using UnityEngine;


[CreateAssetMenu(fileName = "FollowPlayrState", menuName = "MonsterPlatform/FollowPlayerState", order = 0)]
public class FollowPlayerState : State {
    [SerializeField] LayerMask _attackMask;
    // private Player _player;
    private TargetMover _mover;
    private AttackState _attackState;

    public void Construct(FSMRunner runner, TargetMover mover, AttackState state) {
        _runner = runner;
        // _player = player;
        _mover = mover;
        _attackState = state;
    }
    public override void OnEnter() {
        // _mover.Target = _player;
        _mover.DesiredSpeed = _mover.MaxSpeed;
    }

    public override void OnExit()
    {
        _mover.DesiredSpeed = 0;
    }

    public override void Update()
    {
        var ray = new Ray(_runner.Transform.position, _runner.Transform.forward);
        Debug.DrawRay(_runner.Transform.position, _runner.Transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 3f, _attackMask, QueryTriggerInteraction.Ignore)) {
            Debug.Log("Found player");
            var damageable = hit.collider.GetComponent<Damageable>();
            Debug.Assert(damageable);
            _runner.SetState(_attackState);
            _attackState.SetTarget(damageable);
        }
    }
}