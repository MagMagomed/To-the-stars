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
    }
}
