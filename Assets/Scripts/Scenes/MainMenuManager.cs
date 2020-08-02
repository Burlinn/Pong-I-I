using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {

        public Button _playButton;
        public Button _createPlaylistButton;

        // Use this for initialization
        public void Start()
        {
            _playButton.onClick.AddListener(delegate { PlayGame(); });
            _createPlaylistButton.onClick.AddListener(delegate { CreatePlayList(); });
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

        }

    }
}