using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PORTFOLIO.Combat;

namespace PORTFOLIO.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform target;

        NavMeshAgent navMeshAgent;

        private void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();  
        }

        void Update()
        {
            //animationを付随させる
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination) 
        {
            GetComponent<Fighter>().Cancel();
            MoveTo(destination);   
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true; 
            //ある位置でストップさせる
        }

        //animationを追加
        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localvelocity = transform.InverseTransformDirection(velocity);
            float speed = localvelocity.z;
            GetComponent<Animator>().SetFloat("forwardspeed", speed);
        }

    }

}
   
    
