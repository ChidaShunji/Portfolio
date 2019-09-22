using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PORTFOLIO.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform target;



        void Update()
        {
            //animationを付随させる
            UpdateAnimator();
        }


        public void MoveTo(Vector3 destination)
        {
            GetComponent<NavMeshAgent>().destination = destination;
        }

        //animationを追加
        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localvelocity = transform.InverseTransformDirection(velocity);
            float speed = localvelocity.z;
            GetComponent<Animator>().SetFloat("forwardspeed", speed);
        }


    }


}
   
    
