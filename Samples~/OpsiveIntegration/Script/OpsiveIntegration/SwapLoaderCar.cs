using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Opsive.UltimateCharacterController.Items;
using Samples.UGM.Scripts.Examples;
using Samples.UGM.Scripts.Examples.Features.SkinSwap.Core;
using UGM.Core;
using UGM.Examples.Features.SkinSwap.Core;
using UnityEngine;

namespace Script.OpsiveIntegration
{
    public class SwapLoaderCar : SkinSwapLoader
    {
        [Header("Original Game Objects")]
        [Foldout("Original Item")]
        public List<GameObject> originalGameObjectList;
        
        [Header("Swappable Item rotation and position offset")]
        public Vector3 rotationOffset;
        public Vector3 positionOffset;
        public Vector3 scaleOffset;
        

        private List<UGMDataTypes.TokenInfo> swappableItems = new List<UGMDataTypes.TokenInfo>();
        private int currentIndex = 0;
        private int numSwappableItem => swappableItems.Count;
        [Header("Filter this item by the weapon type")]
        public FilterItem filterItemSwap;

        protected IEnumerator Start()
        {
            loadOnStart = false;
            addBoxColliders = false;
            base.Start();
            
            yield return new WaitUntil(() => ItemFilteringHandler.Instance.IsTokenDataLoaded == true);
            swappableItems = ItemFilteringHandler.Instance.GetTokenDataByFilter(filterItemSwap);
            
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                SwapToOriginalSkin();
            }
        }

        protected override void OnEnable()
        {
        }

        protected override void OnDisable()
        {
        }
        
        [Button()]
        public void LoadItem()
        {
            LoadItem(nftId);
        }

        protected virtual void UpdateTransform()
        {
            InstantiatedGO.transform.Rotate(rotationOffset);
            InstantiatedGO.transform.localPosition = positionOffset;
            InstantiatedGO.transform.localScale = scaleOffset;
        }

        protected override void OnModelSuccess(GameObject loadedGO)
        {
            onModelSuccess?.Invoke(loadedGO);
            UpdateTransform();
            
            SetActiveOriginalGameObjectList(false, originalGameObjectList);

        }

        private void UpdateIndex()
        {
            currentIndex = (currentIndex >= numSwappableItem - 1) ? 0 : currentIndex + 1;
        }

        public override void SwapToOriginalSkin()
        {
            base.SwapToOriginalSkin();
            SetActiveOriginalGameObjectList(true, originalGameObjectList);
            
        }

        protected virtual void SetActiveOriginalGameObjectList(bool active, List<GameObject> gameObjectList)
        {
            foreach (var originalItem in gameObjectList)
            {
                originalItem.SetActive(active);
            }
        }

        protected virtual void OnSwapItem()
        {
            Load(swappableItems[currentIndex].token_id);
            UpdateIndex();
        }

        public void OnUse()
        {
            Load(swappableItems[currentIndex].token_id);
            UpdateIndex();
        }
    }
}