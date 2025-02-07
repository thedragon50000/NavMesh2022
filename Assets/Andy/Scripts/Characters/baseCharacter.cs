using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using UniRx;
using UnityEngine.Serialization;

namespace Andy.Scripts.Characters
{
    public class baseCharacter : MonoBehaviour
    {
        private float _atk;
        private float _hp;
        private float _speed;
        private float _coolDown; // 攻速
        public bool bAllied; // 是否為友軍
        private ReactiveProperty<EState> _state; // 反應式狀態機


        NavMeshAgent _nav;
        [Inject] public Transform castle;


        private void Awake()
        {
            _nav = GetComponent<NavMeshAgent>();
            _state.Value = EState.Idle;
        }

        private void Start()


        {
            _nav.SetDestination(castle.position);
        }

        // private void OnTriggerStay(Collider other)
        // {
        //     Debug.Log(@$"{other.gameObject.name}");
        // }

        private void OnTriggerEnter(Collider other)


        {
            Debug.Log(@$"{other.transform.gameObject.name}");
            // if (other.gameObject)
            // {
            // }
        }
    }


    enum EState
    {
        Idle,
    }
}