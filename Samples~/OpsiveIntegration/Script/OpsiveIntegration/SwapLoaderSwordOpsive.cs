using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Script.OpsiveIntegration
{
    public class SwapLoaderSwordOpsive : SwapLoaderBaseWeaponOpsive
    {
        [Foldout("Original Item")]
        public MeshRenderer originalMesh;

        private bool test = false;
        [Button()]
        public void SetActive()
        {
            test = !test;
            originalMesh.enabled = test;
        }
        protected override void SetActiveOriginalGameObjectList(bool active, List<GameObject> gameObjectList)
        {
            originalMesh.enabled = active;
            base.SetActiveOriginalGameObjectList(active, gameObjectList);
        }
    }
}