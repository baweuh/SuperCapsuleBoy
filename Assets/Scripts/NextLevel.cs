using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    [SerializeField] int nextLevel = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // Utilisez la variable nextLevel dans le nom du niveau
            string nextLevelName = "Level" + nextLevel.ToString();
            SceneManager.LoadScene(nextLevelName);
        }
    }

}
