//Jaynie Leavins

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private RectTransform camBound;

    [SerializeField]
    private float moveSpeed;

    private float minX, maxX, minY, maxY;

    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;
    private void Awake()
    {
        UpdateBounds();
    }
    void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    private void MoveCamera()
    {
        if (Input.GetKey(KeyCode.W) && cam.transform.position.y < maxY)
        {
            cam.transform.position += Vector3.up * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.A) && cam.transform.position.x > minX)
        {
            cam.transform.position += Vector3.left * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.S) && cam.transform.position.y > minY)
        {
            cam.transform.position += Vector3.down * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.D) && cam.transform.position.x < maxX)
        {
            cam.transform.position += Vector3.right * Time.deltaTime * moveSpeed;
        }
    }

    private void ZoomCamera() {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && cam.orthographicSize < maxCamSize && WithinBounds())
        {
            cam.orthographicSize += zoomStep;
            UpdateBounds();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && cam.orthographicSize > minCamSize)
        {
            cam.orthographicSize -= zoomStep;
            UpdateBounds();
        }
    }

    private bool WithinBounds() {
        if (cam.transform.position.y < maxY && cam.transform.position.x > minX && cam.transform.position.y > minY
            && cam.transform.position.x < maxX) {
            return true;
        }
        return false;
    }
    private void UpdateBounds() {
        //minY = tileMap.cellBounds.position.y + cam.orthographicSize;
        //maxY = tileMap.cellBounds.size.y + tileMap.cellBounds.position.y - cam.orthographicSize;
        //minX = tileMap.cellBounds.position.x + cam.orthographicSize*2.5f;
        //maxX = tileMap.cellBounds.size.x + tileMap.cellBounds.position.x - cam.orthographicSize*2.5f;
    }
}
