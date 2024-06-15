using Assets.mBuild.Core.Scripts.Game;
using Core.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scripts.Game
{
    public class GameEntryPoint
    {
        private static GameEntryPoint m_instance;
        private Coroutines m_coroutines;
        private UIRootView m_UIRootView;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            m_instance = new GameEntryPoint();
            m_instance.RunGame();
        }

        private GameEntryPoint()
        {
            m_coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(m_coroutines.gameObject);

            var prefabUIRootView = Resources.Load<UIRootView>("UIRoot");
            m_UIRootView = Object.Instantiate(prefabUIRootView);
            Object.DontDestroyOnLoad(m_UIRootView.gameObject);

        }

        private void RunGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.GAMEPLAY) 
            {
                m_coroutines.StartCoroutine(LoadAndStartGameplay());
                return; 
            }
            if (sceneName != Scenes.BOOT) return;
#endif
            m_coroutines.StartCoroutine(LoadAndStartGameplay());
        }
        private IEnumerator LoadAndStartGameplay()
        {
            m_UIRootView.ShowLoadingScene();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(2);
            //
            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            sceneEntryPoint.Run();

            m_UIRootView.HideLoadingScene();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}