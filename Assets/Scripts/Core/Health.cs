using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PORTFOLIO.Core
{

    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 20f;

        bool isDead = false; //HPの確認　死んでいるのか

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamege(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);
            if(healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

    }
}
