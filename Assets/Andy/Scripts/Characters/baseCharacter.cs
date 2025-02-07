using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using UniRx;

namespace Andy.Scripts.Characters
{
    public class baseCharacter : MonoBehaviour
    {
        private float _atk;
        private float _hp;
        private float _speed;

        [Inject] public Transform _goal;

        NavMeshAgent _nav;

        private void Awake()
        {
            _nav = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _nav.SetDestination(_goal.position);
        }
    }
}