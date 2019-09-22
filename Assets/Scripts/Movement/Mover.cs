using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;



    void Update()
    {
        if(Input.GetMouseButton(0))
        {
           MoveToCursor();
        }
        //animationを付随させる
        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hashit = Physics.Raycast(ray, out hit);
        if (hashit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
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

   
    
