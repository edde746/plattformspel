using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    GameObject target;
    PlayerScript playerScript;
    bool initialTransition = true;
    public float horizontalPadding = 4, verticalPadding = 2;
    float maxHorizontalDistance, maxVerticalDistance;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        playerScript = target.GetComponent<PlayerScript>();

        var camera = GetComponent<Camera>();
        var height = camera.orthographicSize;
        var width = height * camera.aspect;

        maxHorizontalDistance = width - horizontalPadding;
        maxVerticalDistance = height - verticalPadding;
    }

    void SetX(float newX)
    {
        if (Mathf.Abs(transform.position.x - newX) < 1) initialTransition = false;
        transform.position = Vector3.Lerp(transform.position, new Vector3(newX, transform.position.y, transform.position.z), initialTransition ? 0.005f : 0.02f);
    }
    void SetY(float newY)
    {
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void Update()
    {
        if (playerScript.walkingRight) SetX(target.transform.position.x + maxHorizontalDistance / 4);
        else SetX(target.transform.position.x - maxHorizontalDistance / 4);

        if (target.transform.position.y > transform.position.y + maxVerticalDistance)
            SetY(target.transform.position.y - maxVerticalDistance);
        if (target.transform.position.y < transform.position.y - maxVerticalDistance)
            SetY(target.transform.position.y + maxVerticalDistance);

        //transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
