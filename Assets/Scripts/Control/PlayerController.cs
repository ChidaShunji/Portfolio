using System.Collections;
using System.Collections.Generic;
using PORTFOLIO.Movement;
using UnityEngine;


namespace PORTFOLIO.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }


        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hashit = Physics.Raycast(ray, out hit);
            if (hashit)
            {
                //methodの共有 Moverから持ってくる
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }

    }

}