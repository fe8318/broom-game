using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerStats;

public class startGame : MonoBehaviour
{
    public void StartGame()
    {
        UIStats.playerSeenInstruction = false;
        // 使用 SceneManager.LoadScene 來讀取 "LevelTest" 場景
        SceneManager.LoadScene("LevelTest");
    }
}
