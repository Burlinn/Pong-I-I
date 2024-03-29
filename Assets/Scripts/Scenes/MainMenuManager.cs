﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {

        public Button _playButton;
        public Button _createPlaylistButton;
        public Button _optionsButton;

        // Use this for initialization
        public void Start()
        {
            _playButton.onClick.AddListener(delegate { PlayGame(); });
            _createPlaylistButton.onClick.AddListener(delegate { CreatePlayList(); });
            _optionsButton.onClick.AddListener(delegate { AccessOptions(); });
            GameManager.SetEnemyScore(0);
            GameManager.SetPlayerScore(0);
        }

        public void Update()
        {

        }

        private void PlayGame()
        {
            GameManager.CreateRandomPlayList();
            SceneManager.LoadScene(GameManager.GetSceneByIndex(0));
        }

        private void CreatePlayList()
        {
            SceneManager.LoadScene(Constants.CREATE_PLAYLIST);
        }

        private void AccessOptions()
        {
            SceneManager.LoadScene(Constants.OPTIONS_MENU);
        }

    }
}