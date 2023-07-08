using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jungle.Exam
{
    public class GameOverUI : MonoBehaviour
    {
        public GameObject ClearUIGo;
        public GameObject FailUIGo;
        public GameObject GotoMenuButtonGo;
        
        public void GameClear()
        {
            ClearUIGo.SetActive(true);
            GotoMenuButtonGo.SetActive(true);
            GameObject.FindWithTag("GameController").GetComponent<InputManager>().InputAsset.Ingame.Disable();
        }
        
        public void GameFail()
        {
            FailUIGo.SetActive(true);
            GotoMenuButtonGo.SetActive(true);
            GameObject.FindWithTag("GameController").GetComponent<InputManager>().InputAsset.Ingame.Disable();
        }

        public void GameRestart()
        {
            SceneManager.LoadScene(0);
        }
    }
}