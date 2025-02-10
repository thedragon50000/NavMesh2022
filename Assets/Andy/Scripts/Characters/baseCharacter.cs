using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using UniRx;

namespace Andy.Scripts.Characters
{
    public class baseCharacter : MonoBehaviour
    {
        [SerializeField] private float atk;
        [SerializeField] private float hp;
        [SerializeField] private float speed;
        [SerializeField] private float coolDown; // 攻速
        private float _canAttackTime; // 攻速冷卻時間
        private bool _bCanAttack; // 可攻擊

        public bool bAllied; // 是否為友軍

        protected ReactiveProperty<EState> State; // 反應式狀態接收器
        private EState _currentState; // 狀態機

        protected Animator Animator;


        // 用字典儲存動畫名
        protected readonly Dictionary<int, string> AniNameDictionary = new();

        // 將動畫名轉成哈希
        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _moveStateHash = Animator.StringToHash("Move");
        private readonly int _attackStateHash = Animator.StringToHash("Attack");
        private readonly int _dieStateHash = Animator.StringToHash("Die");

        NavMeshAgent _nav;
        [Inject] public Transform castle;

        public baseCharacter(ReactiveProperty<EState> state)
        {
            State = state;
        }

        [Inject]
        void Init()
        {
        }

        private void Awake()
        {
            _nav = GetComponent<NavMeshAgent>();
            _nav.speed = speed;
            _nav.isStopped = true;
            Animator = GetComponent<Animator>();
            InitAnim();

            State.Subscribe(state =>
            {
                _nav.isStopped = true;
                _currentState = state;
                StateHandler(_currentState);
            });
            State.Value = EState.Idle;
        }

        private void InitAnim()
        {
            AniNameDictionary.Add(_idleStateHash, "Idle");
            AniNameDictionary.Add(_moveStateHash, "Move");
            AniNameDictionary.Add(_attackStateHash, "Attack");
            AniNameDictionary.Add(_dieStateHash, "Die");
        }

        private void Start()
        {
            _nav.SetDestination(castle.position);
        }

        private void Update()
        {
            _canAttackTime += Time.deltaTime * 1;
            _bCanAttack = _canAttackTime >= coolDown;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(@$"{other.transform.gameObject.name}");
            // if (other.gameObject)
            // {
            // }
        }

        void StateHandler(EState currentState)
        {
            switch (currentState)
            {
                case EState.Idle:
                    // 播放 Idle動畫
                    Animator.Play(_idleStateHash);

                    // todo: 敵人進入範圍要攻擊(進入Defend)
                    // use Trigger
                    break;
                case EState.Move:
                    // todo: 開啟NavMeshAgent並設置目標
                    _nav.isStopped = false;
                    // _nav.SetDestination();
                    // todo: 可考慮實作移動時遇敵
                    break;
                case EState.Chase:
                    // todo: 開啟NavMeshAgent並設置目標
                    _nav.isStopped = false;
                    // _nav.SetDestination();

                    // todo: triggerEnter時才會進行攻擊
                    // todo: triggerExit時開啟NavMeshAgent繼續追擊
                    break;
                case EState.Attack:
                    // 播放攻擊動畫，受公速(coolDown)影響
                    if (_bCanAttack)
                    {
                        Animator.Play(_attackStateHash, 0, 0);
                    }
                    // todo: 呼叫攻擊方法

                    break;
                case EState.Defend:
                    // todo: 播放 攻擊動畫
                    // todo: triggerExit時丟失目標
                    break;
                case EState.Destroyed:
                    // todo: 播放死亡動畫
                    break;
            }
        }
    }


    public enum EState
    {
        Idle, // 閒置
        Move, // 移動時碰到可攻擊目標時不理會
        Chase, // 追擊或攻擊移動，碰到敵軍單位會停下來攻擊
        Attack, // 攻擊狀態
        Defend, // 不移動，攻擊範圍內敵人
        Destroyed, // 掛了
        Max // 最大數，可以當成Enum的長度使用
    }
}