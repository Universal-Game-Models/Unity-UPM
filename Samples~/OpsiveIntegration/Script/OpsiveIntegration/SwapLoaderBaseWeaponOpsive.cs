using System;
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
    public class SwapLoaderBaseWeaponOpsive : SkinSwapLoader
    {
        [Header("Original Game Objects")]
        [Foldout("Original Item")]
        public List<GameObject> originalGameObjectList;
        
        [Header("Swappable Item rotation and position offset")]
        public Vector3 rotationOffset;
        public Vector3 positionOffset;

        [Header("Camera View Type: FPS or ThirdPS")]
        public ViewTypesSwapItem viewType;
        [ReadOnly][SerializeField]private ViewTypesSwapItem currentViewType;
        private bool isEquiped = false;
        [Required("Character Item can be found on the parent Component")]
        public CharacterItem item;
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

            if(item == null) Debug.LogError("Character Item is missing!");

            ExampleUIEvents.IsFirstPersonView.AddListener(OnChangePerspective);
            item?.EquipItemEvent.AddListener(OnEquipItem);
            item?.UnequipItemEvent.AddListener(OnUnEquipItem);
            
            if (ItemFilteringHandler.Instance.isFirstPersonOnly)
            {
                currentViewType = ViewTypesSwapItem.FIRST_PERSON;
            }
            else
            {
                currentViewType = ViewTypesSwapItem.THIRD_PERSON;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                SwapToOriginalSkin();
            }
        }

        [Button()]
        public void GetItemComponent()
        {
            item ??= transform.root.gameObject.GetComponent<CharacterItem>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ExampleUIEvents.IsFirstPersonView.RemoveListener(OnChangePerspective);
            item?.EquipItemEvent.RemoveListener(OnEquipItem);
            item?.UnequipItemEvent.RemoveListener(OnUnEquipItem);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            ExampleUIEvents.SwapItem.AddListener(OnSwapItem);
            // First person game object doens't listen to event for some reason on the first equip
            OnEquipItem();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ExampleUIEvents.SwapItem.RemoveListener(OnSwapItem);

            OnUnEquipItem();
            
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
        }

        protected override void OnModelSuccess(GameObject loadedGO)
        {
            onModelSuccess?.Invoke(loadedGO);
            UpdateTransform();
            
            if (viewType == ViewTypesSwapItem.FIRST_PERSON)
            {
                SetLayerRecursively(InstantiatedGO, LayerMask.NameToLayer("Overlay"));
            }
            SetActiveOriginalGameObjectList(false, originalGameObjectList);

            // Update weapon visibility
            OnChangePerspective(currentViewType == ViewTypesSwapItem.FIRST_PERSON);
        }

        private void UpdateIndex()
        {
            currentIndex = (currentIndex >= numSwappableItem - 1) ? 0 : currentIndex + 1;
        }
        
        protected void SetLayerRecursively(GameObject obj, int newLayer)
        {
            obj.layer = newLayer;
    
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, newLayer);
            }
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
            if (isEquiped == false) return;
            
            Load(swappableItems[currentIndex].token_id);
            UpdateIndex();
            
        }
        
        public virtual void OnChangePerspective(bool isFirstPersonView)
        {
            currentViewType = isFirstPersonView ? ViewTypesSwapItem.FIRST_PERSON : ViewTypesSwapItem.THIRD_PERSON;
            if (isFirstPersonView)
            {
                if (IsThirdPersonViewType())
                {
                    if (InstantiatedGO != null)
                    {
                        InstantiatedGO.SetActive(false);
                    }
                }
            }
            else
            {
                if (IsThirdPersonViewType())
                {
                    if (InstantiatedGO != null)
                    {
                        InstantiatedGO.SetActive(true);
                    }
                }
            }
        }

        protected bool IsThirdPersonViewType()
        {
            return viewType == ViewTypesSwapItem.THIRD_PERSON;
        }

        protected virtual void OnEquipItem()
        {
            isEquiped = true;
        }

        protected virtual void OnUnEquipItem()
        {
            isEquiped = false;
        }
        
    }
}