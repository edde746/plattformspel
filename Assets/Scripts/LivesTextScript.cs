using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesTextScript : MonoBehaviour
{
    TextMeshProUGUI tmp;
    PlayerScript playerScript;

    // Start is called before the first frame update
    int cachedLives = 0;
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) playerScript.DoDie();
        tmp.text = $"Lives: {playerScript.GetLives()}";
    }
}
