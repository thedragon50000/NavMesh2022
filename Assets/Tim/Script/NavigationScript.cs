using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //記得!!

public class NavigationScript : MonoBehaviour
{
    public NavMeshAgent nma;
    public Transform target;

    public bool isChase;

    // Use this for initialization
    void Start()
    {
        nma.SetDestination(target.position); //新方法，意思一樣
    }

    // Update is called once per frame
    void Update()
    {
        nma.isStopped = !isChase;
        //nma.destination = target.position;//舊方法，還是能使用
    }
}