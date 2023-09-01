using Opsive.UltimateCharacterController.Character;
using UnityEngine;

namespace Script.OpsiveIntegration
{
    public class AnimatorMonitorUGM : AnimatorMonitor
    {
        public void SetAnimator(Animator animator)
        {
            m_Animator = animator;
        }
    }
}