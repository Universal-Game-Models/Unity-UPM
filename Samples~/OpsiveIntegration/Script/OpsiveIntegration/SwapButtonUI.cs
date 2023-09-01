using System;
using Samples.UGM.Scripts.Examples;
using UnityEngine;
using UnityEngine.UI;

namespace Script.OpsiveIntegration
{
    [RequireComponent(typeof(Button))]
    public class SwapButtonUI : MonoBehaviour
    {
        private Button swapButton;

        private void OnEnable()
        {
            if (swapButton == null)
                swapButton = GetComponent<Button>();
            if (swapButton is not null)
            {
                swapButton.onClick.AddListener(OnClickedSwapButton);
            }
        }

        private void OnDisable()
        {
            if (swapButton is not null)
            {
                swapButton.onClick.RemoveListener(OnClickedSwapButton);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                OnClickedSwapButton();
            }
        }

        private void OnClickedSwapButton()
        {
            ExampleUIEvents.SwapItem.Invoke();
        }
    }
}