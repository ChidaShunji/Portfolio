using System;
using System.Collections;
using System.Collections.Generic;
using PORTFOLIO.Combat;
using PORTFOLIO.Movement;
using UnityEngine;


namespace PORTFOLIO.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if (InteractWithCombat()) return; //移動と攻撃の優先順位
            if (InteractWithMovement()) return; //無効なエリアをクリックした時
        }

        private bool InteractWithCombat() //移動と攻撃の優先順位
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target =  hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            return false; //targerが見つからない場合
        }

        private bool InteractWithMovement()
        {
        //     if (Input.GetMouseButton(0))
        //     {
        //         MoveToCursor();
        //     }
        // }

        // private void MoveToCursor()
        // {
            RaycastHit hit;
            bool hashit = Physics.Raycast(GetMouseRay(), out hit);
            if (hashit)
            {
                if (Input.GetMouseButton(0))
                {
                    //GetComponent<Mover>().MoveTo(hit.point); //methodの共有 Moverから持ってくる
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}