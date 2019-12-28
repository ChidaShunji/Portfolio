using System.Collections;
using System.Collections.Generic;
using PORTFOLIO.Core;
using PORTFOLIO.Movement;
using UnityEngine;

namespace PORTFOLIO.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {

        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;


        Health target;
        //Transform target;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return; //ターゲットの確認
            if (target.IsDead()) return;
           
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
               // GetComponent<Mover>().MoveTo(target.position);

            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform); //まず敵を見る、回転する
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //　Hit()させるためのトリガー
                TriggerAttack();
                timeSinceLastAttack = 0;
                Hit();
                //Health healthComponet = target.GetComponent<Health>();
                //healthComponet.TakeDamege(weaponDamage);

            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation Event 
        public void Hit()
        {
            if(target == null) {return;}
            Health healthComponet = target.GetComponent<Health>();
            target.TakeDamege(weaponDamage);
            //healthComponet.TakeDamege(weaponDamage);
        }
        
        private bool GetIsInRange()
        {
           // return Vector3.Distance(transform.position, target.position) < weaponRange;
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        //Enemyも利用できるようにコンバットターゲットを指定しない
        //public bool CanAttack(CombatTarget combatTarget)
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) {return false;}
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        
        }

        public void Attack(GameObject combatTarget)
        {
           GetComponent<ActionScheduler>().StartAction(this);  //WHY?
            target = combatTarget.GetComponent<Health>();
            //target = combatTarget.transform;

        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}


