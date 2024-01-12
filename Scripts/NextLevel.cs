using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                SceneManager.LoadScene("Level2");
            } 
            else if (SceneManager.GetActiveScene().name == "Level2")
            {
                SceneManager.LoadScene("YouWin");
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}