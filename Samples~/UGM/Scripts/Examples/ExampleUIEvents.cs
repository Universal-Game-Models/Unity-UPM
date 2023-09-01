﻿using UGM.Core;
using UGM.Examples.WeaponController;
using UnityEngine.Events;

namespace Samples.UGM.Scripts.Examples
{
    public abstract class ExampleUIEvents
    {
        public static UnityEvent<bool> OnShowCursor = new UnityEvent<bool>();
        public static UnityEvent<WeaponType> OnWeaponDeterminedType = new UnityEvent<WeaponType>();
        public static UnityEvent<UGMDataTypes.TokenInfo> OnChangeEquipment = new UnityEvent<UGMDataTypes.TokenInfo>();
        public static UnityEvent SwapItem = new UnityEvent();
        public static UnityEvent<bool> IsFirstPersonView = new UnityEvent<bool>();
        public static UnityEvent OnPlaceOrCancelBuilding = new UnityEvent();
    }
}
