using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 4f;
    public float minZoom = 5;
    public float maxZoom = 100;
    private float currentZoom;
    private float zoomVelocityRef = 7.0f;

    public Transform cameraLockTransform;
    public MeshFilter boundsMesh;
    private Vector3[] mapBounds = new Vector3[2];

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineFramingTransposer framingTransposer;

    public float dragSpeed = 0.5f;
    private bool isDragging = false;

    // Start is called before the first frame update
    void Start()
    {
        var boundsTransform = boundsMesh.GetComponent<Transform>();
        var bounds = boundsMesh.mesh.bounds;
        mapBounds[0] = Vector3.Scale(bounds.max, boundsTransform.localScale);
        mapBounds[1] = Vector3.Scale(bounds.min, boundsTransform.localScale);

        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        currentZoom = framingTransposer.m_CameraDistance;
    }

    // Update is called once per frame
    void Update()
    {
        HandleZoom();
        HandleMouseDrag(Input.mousePosition);
    }

    private void HandleZoom()
    {
        currentZoom -= Input.mouseScrollDelta.y * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        framingTransposer.m_CameraDistance = Mathf.SmoothDamp(framingTransposer.m_CameraDistance, currentZoom, ref zoomVelocityRef, 0.07f);
    }

    private void HandleMouseDrag(Vector3 mousePos)
    {
        if (Input.GetMouseButtonDown(0)) //on frame that mouse is pressed
        {
            Cursor.visible = false;
            isDragging = true;
        }

        if (isDragging && Input.GetMouseButtonUp(0)) //stopped holding down
        {
            Cursor.visible = true;
            isDragging = false;
        }

        if (Input.GetMouseButton(0) && isDragging) //if still held down and dragging
        {
            var movementX = Input.GetAxis("Mouse X");
            var movementY = Input.GetAxis("Mouse Y");

            if (movementX != 0 || movementY != 0)
            {
                Vector3 delta = new Vector3(movementX, 0, movementY);

                MoveMapRelativeToCamera(delta, dragSpeed);
            }
        }
    }

    private void MoveMapRelativeToCamera(Vector3 dir, float speed)
    {
        var newPos = cameraLockTransform.position + (dir * speed * (currentZoom / 10));
        if (WithinBounds(newPos))
            cameraLockTransform.position += dir * speed * (currentZoom / 10);
    }

    private bool WithinBounds(Vector3 newPos)
    {
        return (newPos.x < mapBounds[0].x)
            && (newPos.x > mapBounds[1].x)
            && (newPos.z < mapBounds[0].z)
            && (newPos.z > mapBounds[1].z);
    }
}
