using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    public Vector3 targetDelta = new Vector3(-5, 0, 0);
    public Sprite pressedSprite;
    Sprite ogSprite;
    SpriteRenderer spriteRenderer;
    public GameObject elevator;
    Vector3 startPosition;
    bool buttonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = elevator.transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        ogSprite = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed)
            elevator.transform.position = Vector3.Lerp(elevator.transform.position, startPosition + targetDelta, Time.deltaTime);
        else
            elevator.transform.position = Vector3.Lerp(elevator.transform.position, startPosition, Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = pressedSprite;
            buttonPressed = true;
        }
    }



    IEnumerator UnpressWaiter()
    {
        yield return new WaitForSeconds(2);
        buttonPressed = false;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = ogSprite;
            StartCoroutine(UnpressWaiter());
        }
    }
}
