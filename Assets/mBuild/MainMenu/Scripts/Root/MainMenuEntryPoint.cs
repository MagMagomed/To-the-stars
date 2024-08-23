using Core.Scripts.Game;
using Core.Scripts.View;
using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    public event Action GoToGameplaySceneRequesteed;
    [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

    public void Run(UIRootView uiRoot)
    {
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.GoToGameplayButtonClicked += () =>
        {
            GoToGameplaySceneRequesteed?.Invoke();
        };
    }
}
