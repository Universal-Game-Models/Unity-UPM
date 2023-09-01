using System;
using Samples.UGM.Scripts.Examples;
using UnityEngine;

namespace Script.OpsiveIntegration
{
    public class EventFirstPersonView : MonoBehaviour
    {
        private void OnEnable()
        {
            Debug.Log("FirstPerson On");
            ExampleUIEvents.IsFirstPersonView.Invoke(true);
        }

        private void OnDisable()
        {
            Debug.Log("Third Person On");
            ExampleUIEvents.IsFirstPersonView.Invoke(false);
        }
    }
}