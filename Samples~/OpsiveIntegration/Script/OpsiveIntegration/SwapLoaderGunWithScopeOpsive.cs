using NaughtyAttributes;
using UnityEngine;

namespace Script.OpsiveIntegration
{
    public class SwapLoaderGunWithScopeOpsive : SwapLoaderBaseWeaponOpsive
    {
        [Header("Gun Scope Configuration")]
        [Foldout("Gun Scope Positioning")]
        public GameObject scope;
        [Foldout("Gun Scope Positioning")]
        public Vector3 originalScopePosition;
        [Foldout("Gun Scope Positioning")]
        public Vector3 thisItemScopePosition;

        protected override void OnModelSuccess(GameObject loadedGO)
        {
            base.OnModelSuccess(loadedGO);
            if (scope != null)
                scope.transform.localPosition = thisItemScopePosition;
        }

        public override void SwapToOriginalSkin()
        {
            base.SwapToOriginalSkin();
            if (scope != null)
            {
                scope.transform.localPosition = originalScopePosition;
            }
        }
    }
}