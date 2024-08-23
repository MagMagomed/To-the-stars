using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuRootBinder : MonoBehaviour
{
    public event Action GoToGameplayButtonClicked;
    public void HandleGoToGameplayButtonClick()
    {
        GoToGameplayButtonClicked?.Invoke();
    }
}
