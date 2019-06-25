using UnityEngine;
using UnityEngine.SceneManagement;
public class Switch_scenes : MonoBehaviour {
    public void GotoMainScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void GotoTest1()
    {
        SceneManager.LoadScene("Test Scenario 1");
    }
    public void GotoTest2()
    {
        SceneManager.LoadScene("Test Scenario 2");
    }
    public void GotoTest3()
    {
        SceneManager.LoadScene("Test Scenario 3");
    }

}
