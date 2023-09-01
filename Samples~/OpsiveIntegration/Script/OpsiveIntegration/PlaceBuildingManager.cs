using Samples.UGM.Scripts.Examples;
using UGM.Examples.Inventory.InventoryItems.Controls;
using UnityEngine;

namespace Script.OpsiveIntegration
{
    public class PlaceBuildingManager : InstantiatableInventoryControl
    {
        protected override void PlaceOrCancel(RaycastHit hit)
        {
            base.PlaceOrCancel(hit);
            ExampleUIEvents.OnPlaceOrCancelBuilding.Invoke();
        }
    }
}