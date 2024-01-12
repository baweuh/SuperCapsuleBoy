using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public static int life = 4;
    public TextMeshPro livesText;

    private void Start()
    {
        UpdateLivesText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Body"))
        {
            CheckLife();
            Debug.Log(life);
        }
    }

    void CheckLife()
    { 
        life--;
        
        if (life < 0)
        {
            SceneManager.LoadScene("YouLose");
            Cursor.lockState = CursorLockMode.None;
            life = 4;
        }
        else
        {
            Invoke(nameof(ReloadLevel), 0f);
        }
        
        UpdateLivesText();
    }

    void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Vies : " + life.ToString();
        }
    }
    
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
