using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
{
    public void LoadLevel1()
    {
        // 使用 SceneManager.LoadScene 來讀取 "Level1" 場景
        SceneManager.LoadScene("Level1");
    }
}