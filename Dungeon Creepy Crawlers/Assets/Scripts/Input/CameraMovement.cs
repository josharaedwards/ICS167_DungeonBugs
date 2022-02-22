using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    //[SerializeField]
    //private SpriteRenderer mapRenderer;

    [SerializeField]
    private float moveSpeed;

    private float minX, maxX, minY, maxY;

    //[SerializeField]
    //private float zoomStep, minCamSize, maxCamSize;
    private void Awake()
    {
        minX = -3f;
        maxX = 3f;
        minY = -2f;
        maxY = 3f;
    }
    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (Input.GetKey(KeyCode.W) && cam.transform.position.y < maxY)
        {
            transform.position += Vector3.up * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.A) && cam.transform.position.x > minX)
        {
            transform.position += Vector3.left * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.S) && cam.transform.position.y > minY)
        {
            transform.position += Vector3.down * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.D) && cam.transform.position.x < maxX)
        {
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;
        }
    }
}
