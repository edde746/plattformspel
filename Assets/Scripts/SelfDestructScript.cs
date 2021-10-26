using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructScript : MonoBehaviour
{
    public float time = 50;

    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0) Destroy(gameObject);
    }
}
