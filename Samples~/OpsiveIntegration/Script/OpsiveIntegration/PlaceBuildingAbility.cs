using System.Collections;
using System.Collections.Generic;
using Opsive.UltimateCharacterController.Character.Abilities;
using Samples.UGM.Scripts.Examples;
using Samples.UGM.Scripts.Examples.Features.SkinSwap.Core;
using UGM.Core;
using UGM.Examples.Inventory.InventoryItems.Controls;
using UnityEngine;

namespace Script.OpsiveIntegration
{
    public class PlaceBuildingAbility : Ability
    {

        private bool isUsingThisAbility = false;
        public FilterItem buildingItem;
        private List<UGMDataTypes.TokenInfo> swappableItems = new List<UGMDataTypes.TokenInfo>();
        private int currentIndex = 0;
        private int numSwappableItem => swappableItems.Count;
        public override void Start()
        {
            base.Start();
            BindEvents();
        }

        public override void Update()
        {
            base.Update();
            Debug.Log("Update");
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            UnBindEvents();
        }

        private void BindEvents()
        {
            ExampleUIEvents.OnPlaceOrCancelBuilding.AddListener(OnPlaceOrCancelBuilding);
        }

        private void UnBindEvents()
        {
            ExampleUIEvents.OnPlaceOrCancelBuilding.RemoveListener(OnPlaceOrCancelBuilding);
        }

        private void OnPlaceOrCancelBuilding()
        {
            isUsingThisAbility = false;
        }

        public override bool CanStartAbility()
        {
            return true;
        }

        public override bool CanStopAbility(bool force)
        {
            return !isUsingThisAbility;
        }

        public override bool AbilityWillStart()
        {
            Debug.Log("Ability Will Start");

            return base.AbilityWillStart();
        }

        protected override void AbilityStarted()
        {
            
            Debug.Log("Ability Started");
            isUsingThisAbility = true;
            if(swappableItems.Count <= 0)
            {
                swappableItems = ItemFilteringHandler.Instance.GetTokenDataByFilter(buildingItem);
            }
            if(swappableItems.Count > 0)
                InstantiatableInventoryControl.Instance.SetTokenInfo(swappableItems[currentIndex]);

            UpdateIndex();
            base.AbilityStarted();
        }

        protected override void AbilityStopped(bool force)
        {
            Debug.Log("Ability Stopped");
            base.AbilityStopped(force);
        }
        
        private void UpdateIndex()
        {
            currentIndex = (currentIndex >= numSwappableItem - 1) ? 0 : currentIndex + 1;
        }
    }
}