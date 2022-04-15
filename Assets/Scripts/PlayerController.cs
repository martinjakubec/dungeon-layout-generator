using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;

    Rigidbody2D playerRigidbody;

    public float runSpeed = 20.0f;

    private Vector3 playerMovementDirectionVector;
    private Vector2 movementVector;
    private bool shouldPlayerBeMoving = false;
    public float moveLollipop;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            playerMovementDirectionVector = mainCamera.ScreenToWorldPoint(mousePos);
            movementVector = playerMovementDirectionVector - gameObject.transform.position;
            if (Vector3.Distance(playerMovementDirectionVector, gameObject.transform.position) > moveLollipop)
            {
                shouldPlayerBeMoving = true;
            }
        }

    }
    private void StopMoving()
    {
        playerRigidbody.velocity = new Vector2(0, 0);
        shouldPlayerBeMoving = false;
    }

    private void FixedUpdate()
    {
        float distanceBetweenPoints = Vector3.Distance(playerMovementDirectionVector, gameObject.transform.position);

        if (shouldPlayerBeMoving)
        {
            playerRigidbody.velocity = movementVector.normalized * runSpeed;
        }

        if (distanceBetweenPoints < moveLollipop)
        {
            StopMoving();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CustomTag.Obstacle))
        {
            StopMoving();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case CustomTag.Door:
                SceneManager.LoadScene(0);
                break;
            case CustomTag.Obstacle:
                StopMoving();
                break;
            default:
                Debug.Log("Hit something different");
                break;
        }
    }
}
