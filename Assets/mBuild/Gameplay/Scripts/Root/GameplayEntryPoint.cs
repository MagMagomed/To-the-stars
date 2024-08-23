using Core.Scripts.Game;
using Core.Scripts.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    public event Action GoToMainMenuSceneRequesteed;
    [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;

    public void Run(UIRootView uiRoot)
    {
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.GoToMainMenuButtonClicked += () =>
        {
            GoToMainMenuSceneRequesteed?.Invoke();
        };
    }
}
