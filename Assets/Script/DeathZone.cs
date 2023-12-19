using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour {
    // 在碰到"Death Zone"時觸發的碰撞事件
    private void OnTriggerEnter2D(Collider2D other) {
        // 檢查碰到的物體是否是玩家角色（根據需要調整碰撞條件）
        if (other.CompareTag("Player")) {
            // Notify the player object that it has failed
            other.GetComponent<PlayerManager>().failAndStop();
            Debug.Log("fail");
        }
    }
}