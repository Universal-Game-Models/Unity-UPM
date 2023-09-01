using Opsive.UltimateCharacterController.Character.Abilities;

namespace Script.OpsiveIntegration
{
    public class CarSwapAbility : Ability
    {
        public SwapLoaderCar swapLoaderCar;
        protected override void AbilityStarted()
        {
            base.AbilityStarted();
            if(swapLoaderCar) swapLoaderCar.OnUse();
        }
    }
}