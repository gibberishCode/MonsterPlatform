using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "MonsterPlatform/AttackState", order = 0)]
public class AttackState : State {
    [SerializeField] LayerMask _attackMask;
    private Damageable _damageable;
    private FollowPlayerState _followState;
    private Attacker _attacker;

    public void Construct(FSMRunner runner, FollowPlayerState followState, Attacker attacker) {
        _runner = runner;
        _followState = followState;
        _attacker = attacker;
        _attacker.TargetOutOfRangeEvent.AddListener(
            (target) => _runner.SetState(followState)
        );
    }
    public override void OnEnter() {
        _attacker.enabled = true;
    }

    public override void OnExit()
    {
        if (_attacker) {
            _attacker.enabled = false;
        }
    }

    public override void Update()
    {
        
    }

    public void SetTarget(Damageable damageable) {
        if (!damageable) {
            _runner.SetState(_followState);
            return;
        }
        _damageable = damageable;
        _damageable.DiedEvent.AddListener(
            () => _runner.SetState(_followState)
        );
    }
}