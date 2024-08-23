using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Scripts.Game
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadScreen;
        [SerializeField] private Transform _uiSceneContainer;

        private void Awake()
        {
            HideLoadingScene();
        }
        public void HideLoadingScene()
        {
            _loadScreen.SetActive(false);
        }
        public void ShowLoadingScene()
        {
            _loadScreen.SetActive(true);
        }
        public void AttachSceneUI(GameObject sceneUI)
        {
            ClearSceneUI();

            sceneUI.transform.SetParent(_uiSceneContainer, false);
        }

        private void ClearSceneUI()
        {
            var childCount = _uiSceneContainer.childCount;
            for(int i = 0; i < childCount; i++)
            {
                Destroy(_uiSceneContainer.GetChild(i).gameObject);
            }
        }
    }
}
