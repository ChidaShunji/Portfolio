using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PORTFOLIO.Core
{

    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction; //実行中の最後のアクションへの参照を保持

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
             currentAction = action; //現在のアクションを開始アクションにする
        }

        public void CancelCurrentAction()
        {
            StartAction(null);   
        }

    }
}
