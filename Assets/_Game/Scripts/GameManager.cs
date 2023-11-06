using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text _coin;
    int collectedCoin = 0;
    public Text _nyawa;
    int lives = 1;
    public Transform respawnPos;
    public GameObject player;
    public GameObject victoryUI;
    public GameObject gameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Time.timeScale = 1.0f;
    }

    public void TambahCoin(int i)
    {
        collectedCoin += i;
        _coin.text = $"{collectedCoin} / 5";
    }

    public void UpdateNyawa(int i)
    {
        lives += i;
        if (lives == 0)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
        }
        _nyawa.text = $"x{lives}";
    }

    public void Respawn()
    {
        player.transform.position = respawnPos.transform.position;
    }

    public void SetRespawn(Transform newRespawn)
    {
        respawnPos = newRespawn;
    }

    public void ShowVictory()
    {
        victoryUI.SetActive(true);
    }
}
