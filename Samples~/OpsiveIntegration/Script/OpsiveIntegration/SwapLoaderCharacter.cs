using System.Collections.Generic;
using NaughtyAttributes;
using Opsive.UltimateCharacterController.Camera;
using Opsive.UltimateCharacterController.Character;
using UGM.Examples.Features.SkinSwap.Core;
using UGM.Examples.ThirdPersonController;
using UnityEngine;

namespace Script.OpsiveIntegration
{
    public class SwapLoaderCharacter : SkinSwapLoader
    {
        public GameObject Character;
        public CameraController cameraController;
        public List<GameObject> originalGameObjectList;
        [SerializeField]private RuntimeAnimatorController runtimeAnimator;
        public GameObject ItemRightHand;
        public GameObject ItemLeftHand;
        private Avatar avatar;
        public Animator animator;
        public AnimatorMonitorUGM animatorMonitorUgm;
        public CharacterIKUgm characterIKUgm;
        [Tooltip("Animator Controller to use on loaded character")]
        private RuntimeAnimatorController animatorController;
        [SerializeField]
        [Tooltip("Animator Avatar to use on loaded character")]
        private Avatar animatorAvatar;
        [SerializeField]
        [Tooltip("Animator use apply root motion")]
        private bool applyRootMotion;
        [SerializeField]
        [Tooltip("Animator update mode")]
        private AnimatorUpdateMode updateMode;
        [SerializeField]
        [Tooltip("Animator culling mode")]
        private AnimatorCullingMode cullingMode;
        [Button()]
        public void Init()
        {
            Load("1");
        }


        protected override void OnModelSuccess(GameObject loadedGO)
        {


            //Remove old animator as it doesn't point to the character
            if (animator != null)
            {
                //Get the existing settings
                animatorController = animator.runtimeAnimatorController;
                animatorAvatar = animator.avatar;
                applyRootMotion = animator.applyRootMotion;
                updateMode = animator.updateMode;
                cullingMode = animator.cullingMode;
                DestroyImmediate(animator);
                if(characterIKUgm != null)
                    DestroyImmediate(characterIKUgm);
            }
            //Add new animator that points to the new character
            animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            animator.avatar = AvatarCreator.CreateAvatar(loadedGO);
            animator.applyRootMotion = applyRootMotion;
            animator.updateMode = updateMode;
            animator.cullingMode = cullingMode;
            animator.enabled = true;
            // animator.
            
            animatorMonitorUgm.SetAnimator(animator);
            SetActiveOriginalGameObjectList(false, originalGameObjectList);
            characterIKUgm = gameObject.AddComponent<CharacterIKUgm>();
            
            characterIKUgm.OnAttachLookSource(cameraController);
            
            ChangeItemPosition(ItemRightHand, characterIKUgm.m_RightHand);
            ChangeItemPosition(ItemLeftHand, characterIKUgm.m_LeftHand);

            base.OnModelSuccess(loadedGO);
        }

        private void ChangeItemPosition(GameObject item, Transform positionTransform)
        {
            item.transform.SetParent(positionTransform);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        }

        protected virtual void SetActiveOriginalGameObjectList(bool active, List<GameObject> gameObjectList)
        {
            foreach (var originalItem in gameObjectList)
            {
                originalItem.SetActive(active);
            }
        }
    }
}