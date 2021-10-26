using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchScript : MonoBehaviour
{
    public string levelName = string.Empty;

    void Start()
    {}

    void Update()
    {}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Switch scene!");
            SceneManager.LoadScene(levelName);
        }
    }
}
