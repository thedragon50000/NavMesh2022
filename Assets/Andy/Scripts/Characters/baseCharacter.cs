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

        NavMeshAgent _nav;
        [Inject] public Transform castle;


        private void Awake()
        {
            _nav = GetComponent<NavMeshAgent>();
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
}