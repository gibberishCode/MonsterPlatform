using System;
using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using UnityEngine;






public class Enemy : MonoBehaviour {
    public static event Action DiedEvent;
    [SerializeField] LayerMask _castLayers;
    [SerializeField] FollowPlayerState _followPlayerState;
    [SerializeField] AttackState _attackState;
    private TargetMover _mover;
    private Attacker _attacker;
    public float Speed => _mover.Speed;
    public float IsAttcking => _attacker.IsAttacking ? 1 : 0;
    private bool _isAvoiding;
    private FSMRunner _runner;
    private FSMDependencieManager _dependencyManager;


    private void Awake() {
        _mover = GetComponent<TargetMover>();
        _attacker = GetComponent<Attacker>();
        _dependencyManager = new FSMDependencieManager();
        _runner = gameObject.AddComponent<FSMRunner>();
        _dependencyManager.Register(this);
        _dependencyManager.Register(_mover);
        _attackState = Instantiate(_attackState);
        var damageable = GetComponent<Damageable>();
        damageable.DiedEvent.AddListener(() => DiedEvent?.Invoke());
        _followPlayerState = Instantiate(_followPlayerState);
        _dependencyManager.Register(_runner);
        _dependencyManager.Register(_attackState);
        _dependencyManager.Register(_followPlayerState);
        _dependencyManager.Register(_attacker);
        _dependencyManager.ConstructState(_attackState);
        _dependencyManager.ConstructState(_followPlayerState);
        _runner.SetState(_followPlayerState);
        // TargetPlayer();
    }

    internal void Init(int difficulty) {
        var settings = ServiceLocator.Current.Get<GameManager>().GameSettings;
        var health = GetComponent<Damageable>();
        health.Health.CurrentValue *= settings.EnemyHealthIncreaseRate * difficulty;
        health.Health.MaxValue = health.Health.CurrentValue;
        var attack = GetComponent<DamageDealer>();
        attack.Damage_ = attack.Damage_ * settings.IncreaseEnemyAttackRate * difficulty;
    }

    // private void Update() {

    //     if (!_isAvoiding) {
    //         Avoid();
    //     }
    // }

    // private void SetAttackTaget() {
    //     if (Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hit, 10, _castLayers)) {

    //     }

    // }

    // private void Avoid() {
    //     var hits = Physics.RaycastAll(new Ray(transform.position, transform.forward), 10, _castLayers);
    //     if (hits.Length == 0) {
    //         return;
    //     }
    //     var hit = hits[0];
    //     var platform = hit.collider.GetComponentInParent<Platform>();
    //     _mover.Target = new WaypointTarget() {Position = platform.transform.position + transform.right * 24};
    //     _mover.ReachedTarget.AddListener(TargetPlayer);
    //     _isAvoiding = true;
    // }

    // private void TargetPlayer() {
    //     //TODO Fix it 
    //     _mover.Target = FindAnyObjectByType<Player>();
    //     _mover.ReachedTarget.RemoveListener(TargetPlayer);
    //     _isAvoiding = false;

    // }

}
