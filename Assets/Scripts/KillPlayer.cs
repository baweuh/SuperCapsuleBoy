using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public static int life = 4;

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
        }
        else
        {
            Invoke(nameof(ReloadLevel), 0f);
        }
    }
    
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
