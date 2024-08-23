using System;
using UnityEngine;

namespace Core.Scripts.View
{
    public class UIGameplayRootBinder : MonoBehaviour
    {
        public event Action GoToMainMenuButtonClicked;
        public void HandleGoToMainMenuButtonClick()
        {
            GoToMainMenuButtonClicked?.Invoke();
        }
    }
}
