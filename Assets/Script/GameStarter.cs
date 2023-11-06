using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerStats;

public class GameStarter : MonoBehaviour {
    public void StartGame() {
        UIStats.playerSeenInstruction = false;
        // 使用 SceneManager.LoadScene 來讀取 "Level1" 場景
        SceneManager.LoadScene("Level1");
    }
}
