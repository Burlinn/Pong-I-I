using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace CreatePlaylist
{
    public class CreatePlaylistManager : MonoBehaviour
    {

        public Button _playButton;
        public Button _backButton;

        public Button _pongButton;
        public Button _breakoutButton;
        public Button _invisiballButton;
        public Button _multiballButton;
        public Button _missileButton;
        public Button _windmillButton;
        public Button _portalButton;

        public List<Button> _sceneButtons;

        public List<Sprite> _sceneButtonSprites;

        public Button _undoButton;

        public Text _validationText;

        private int _scenesSelected = 0;

        private List<string> _sceneList;
        
        // Use this for initialization
        public void Start()
        {
            _playButton.onClick.AddListener(delegate { PlayGame(); });
            _backButton.onClick.AddListener(delegate { Back(); });
            _pongButton.onClick.AddListener(delegate { AddScene(Constants.PONG, _pongButton.GetComponent<Image>()); });
            _breakoutButton.onClick.AddListener(delegate { AddScene(Constants.BREAKOUTBALL, _breakoutButton.GetComponent<Image>()); });
            _invisiballButton.onClick.AddListener(delegate { AddScene(Constants.INVISIBALL, _invisiballButton.GetComponent<Image>()); });
            _multiballButton.onClick.AddListener(delegate { AddScene(Constants.MULTIBALL, _multiballButton.GetComponent<Image>()); });
            _missileButton.onClick.AddListener(delegate { AddScene(Constants.MISSILE, _missileButton.GetComponent<Image>()); });
            _windmillButton.onClick.AddListener(delegate { AddScene(Constants.WINDMILL, _windmillButton.GetComponent<Image>()); });
            _portalButton.onClick.AddListener(delegate { AddScene(Constants.PORTAL, _portalButton.GetComponent<Image>()); });

            _undoButton.onClick.AddListener(delegate { Undo(); });

            _sceneList = new List<string>();
            _validationText.gameObject.SetActive(false);
        }

        public void Update()
        {

        }

        private void PlayGame()
        {
            if(_scenesSelected == 20) { 
                GameManager.CreatePlayListFromList(_sceneList);
                SceneManager.LoadScene(GameManager.GetSceneByIndex(0));
            }
            else
            {
                _validationText.gameObject.SetActive(true);
            }
        }

        private void Back()
        {
            SceneManager.LoadScene(Constants.MAIN_MENU);
        }

        private void AddScene(string sceneName, Image image)
        {
            if(_scenesSelected < 20) { 
                _sceneButtons[_scenesSelected].image.sprite = image.sprite;
                _scenesSelected++;
                _sceneList.Add(sceneName);
            }
        }

        private void Undo()
        {
            if(_scenesSelected > 0) { 
                _scenesSelected--;
                _sceneButtons[_scenesSelected].image.sprite = _sceneButtonSprites[_scenesSelected];
                _sceneList.RemoveAt(_scenesSelected);
            }
        }

    }
}