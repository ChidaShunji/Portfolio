using System;
using System.Collections;
using System.Collections.Generic;
using PORTFOLIO.Combat;
using PORTFOLIO.Core;
using PORTFOLIO.Movement;
using UnityEngine;


namespace PORTFOLIO.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        private void Start() {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead()) return;
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

                GameObject targetGameObject = target.gameObject;
                if(!GetComponent<Fighter>().CanAttack(targetGameObject))
                {
                    continue;
                }
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(targetGameObject);
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
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
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