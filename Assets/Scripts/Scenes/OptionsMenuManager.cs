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

        public Toggle _largerPaddleToggle;
        public Toggle _fasterPaddleToggle;

        // Use this for initialization
        public void Start()
        {

            List<string> disabledScenes = GameManager.GetDisabledScenes();
            if (disabledScenes.Contains(Constants.PONG))
            {
                _pongToggle.isOn = true;
            }
            if (disabledScenes.Contains(Constants.BREAKOUTBALL))
            {
                _breakoutToggle.isOn = true;
            }
            if (disabledScenes.Contains(Constants.INVISIBALL))
            {
                _invisiballToggle.isOn = true;
            }
            if (disabledScenes.Contains(Constants.MULTIBALL))
            {
                _multiballToggle.isOn = true;
            }
            if (disabledScenes.Contains(Constants.MISSILE))
            {
                _missileToggle.isOn = true;
            }
            if (disabledScenes.Contains(Constants.WINDMILL))
            {
                _windmillToggle.isOn = true;
            }
            if (disabledScenes.Contains(Constants.PORTAL))
            {
                _portalToggle.isOn = true;
            }
            if (GameManager.GetLargerPaddle())
            {
                _largerPaddleToggle.isOn = true;
            }
            if (GameManager.GetFasterPaddle())
            {
                _fasterPaddleToggle.isOn = true;
            }

            _backButton.onClick.AddListener(delegate { Back(); });
            _pongToggle.onValueChanged.AddListener(delegate { ToggleScene(Constants.PONG, _pongToggle); });
            _breakoutToggle.onValueChanged.AddListener(delegate { ToggleScene(Constants.BREAKOUTBALL, _breakoutToggle); });
            _invisiballToggle.onValueChanged.AddListener(delegate { ToggleScene(Constants.INVISIBALL, _invisiballToggle); });
            _multiballToggle.onValueChanged.AddListener(delegate { ToggleScene(Constants.MULTIBALL, _multiballToggle); });
            _missileToggle.onValueChanged.AddListener(delegate { ToggleScene(Constants.MISSILE, _missileToggle); });
            _windmillToggle.onValueChanged.AddListener(delegate { ToggleScene(Constants.WINDMILL, _windmillToggle); });
            _portalToggle.onValueChanged.AddListener(delegate { ToggleScene(Constants.PORTAL, _portalToggle); });

            _largerPaddleToggle.onValueChanged.AddListener(delegate { ToggleLargerPaddle(_largerPaddleToggle.isOn); });
            _fasterPaddleToggle.onValueChanged.AddListener(delegate { ToggleFasterPaddle(_fasterPaddleToggle.isOn); });

        }

        public void Update()
        {

        }

        private void Back()
        {
            SceneManager.LoadScene(Constants.MAIN_MENU);
        }

        private void ToggleScene(string sceneName, Toggle sceneToggle)
        {
            if (sceneToggle.isOn)
            {
                if (GameManager.GetDisabledScenes().Count < 6)
                {
                    GameManager.DisableScene(sceneName);
                }
                else
                {
                    sceneToggle.isOn = false;
                }
            }
            else
            {
                GameManager.EnableScene(sceneName);
            }
        }

        private void ToggleLargerPaddle(bool isOn)
        {
            GameManager.SetLargerPaddle(isOn);
        }

        private void ToggleFasterPaddle(bool isOn)
        {
            GameManager.SetFasterPaddle(isOn);
        }

    }
}