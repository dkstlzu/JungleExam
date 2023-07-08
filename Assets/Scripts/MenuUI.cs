using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jungle.Exam
{
    public class MenuUI : MonoBehaviour
    {
        public void OnStartButtonClicked()
        {
            SceneManager.LoadScene("InGame");
        }

        public void OnExitButtonClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}