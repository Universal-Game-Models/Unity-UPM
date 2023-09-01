using System;
using Opsive.Shared.StateSystem;
using Opsive.UltimateCharacterController.Camera.ViewTypes;
using Opsive.UltimateCharacterController.Items;
using UnityEngine;

namespace Script.OpsiveIntegration
{
    public class DummyTestOpsive : MonoBehaviour
    {
        public void OnEquiptItem(CharacterItem item, int i)
        {
            Debug.Log($"Character Item Name: {item.name } ItemType: {item.GetType()} Int: {i}" );
        }
    }
}