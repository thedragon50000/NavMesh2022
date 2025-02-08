using System;
using System.Data;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using UniRx;
using UnityEngine.Serialization;

namespace Andy.Scripts.Characters
{
    public class baseCharacter : MonoBehaviour
    {
        [SerializeField] private float atk;
        [SerializeField] private float hp;
        [SerializeField] private float speed;
        [SerializeField] private float coolDown; // 攻速
        private float _canAttackTime; // 攻速冷卻時間

        public bool bAllied; // 是否為友軍
        private ReactiveProperty<EState> _state; // 反應式狀態接收器
        private EState _currentState; // 狀態機


        NavMeshAgent _nav;
        [Inject] public Transform castle;

        [Inject]
        void Init()
        {
        }

        private void Awake()
        {
            _nav = GetComponent<NavMeshAgent>();
            _state.Subscribe(state =>
            {
                _currentState = state;
                StateHandler(_currentState);
            });
            _state.Value = EState.Idle;
        }

        private void Start()


        {
            _nav.SetDestination(castle.position);
        }

        private void Update()
        {
            // 大於 
            _canAttackTime += Time.deltaTime * 1;
            // _coolDown 
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
                    // todo: 播放 Idle動畫
                    // todo: 敵人進入範圍要攻擊(進入Attack狀態)
                    break;
                case EState.Move:
                    // todo: 開啟NavMeshAgent並設置目標
                    // todo: 可考慮實作移動時遇敵
                    break;
                case EState.Chase:
                    // todo: 開啟NavMeshAgent並設置目標
                    // todo: triggerEnter時才會進行攻擊
                    // todo: triggerExit時開啟NavMeshAgent繼續追擊

                    break;
                case EState.Attack:
                    // todo: 播放攻擊動畫
                    // todo: 計算攻速
                    // todo: 呼叫攻擊方法
                    break;
                case EState.Defend:
                    // todo: 播放 Idle動畫
                    // todo: triggerEnter時才會進行攻擊
                    // todo: triggerExit時丟失目標
                    break;
                case EState.Destroyed:
                    // todo: 播放死亡動畫
                    break;
            }
        }
    }


    enum EState
    {
        Idle, // 閒置
        Move, // 移動時碰到可攻擊目標時不理會
        Chase, // 追擊或攻擊移動，碰到敵軍單位會停下來攻擊
        Attack, // 攻擊狀態
        Defend, // 不移動，攻擊範圍內敵人
        Destroyed, // 掛了
    }
}