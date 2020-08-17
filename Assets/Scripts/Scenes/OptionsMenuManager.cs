using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace CreatePlaylist
{
    public class OptionsMenuManager : MonoBehaviour
    {

        public Button _backButton;

        public Toggle _pongToggle;
        public Toggle _breakoutToggle;
        public Toggle _invisiballToggle;
        public Toggle _multiballToggle;
        public Toggle _missileToggle;
        public Toggle _windmillToggle;
        public Toggle _portalToggle;

        // Use this for initialization
        public void Start()
        {
            _backButton.onClick.AddListener(delegate { Back(); });
            _pongToggle.onValueChanged.AddListener(delegate { ToggleScene(Constants.PONG, _pongToggle.isOn); });
            _breakoutToggle.onValueChanged.AddListener(delegate { ToggleScene(Constants.BREAKOUTBALL, _breakoutToggle.isOn); });
            _invisiballToggle.onValueChanged.AddListener(delegate { ToggleScene(Constants.INVISIBALL, _invisiballToggle.isOn); });
            _multiballToggle.onValueChanged.AddListener(delegate { ToggleScene(Constants.MULTIBALL, _multiballToggle.isOn); });
            _missileToggle.onValueChanged.AddListener(delegate { ToggleScene(Constants.MISSILE, _missileToggle.isOn); });
            _windmillToggle.onValueChanged.AddListener(delegate { ToggleScene(Constants.WINDMILL, _windmillToggle.isOn); });
            _portalToggle.onValueChanged.AddListener(delegate { ToggleScene(Constants.PORTAL, _portalToggle.isOn); });
        }

        public void Update()
        {

        }

        private void Back()
        {
            SceneManager.LoadScene(Constants.MAIN_MENU);
        }

        private void ToggleScene(string sceneName, bool isOn)
        {
            if (isOn)
            {
                GameManager.DisableScene(sceneName);
            }
            else
            {
                GameManager.EnableScene(sceneName);
            }
        }
        
    }
}