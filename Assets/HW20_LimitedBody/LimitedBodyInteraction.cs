using UnityEngine;

public class LimitedBodyInteraction : MonoBehaviour
{
    public Camera arCamera;
    public Transform movableObject;
    public Transform snapZone;

    public float moveSpeed = 5f;
    public float snapDistance = 0.3f;

    private bool selected = false;
    private Vector3 targetPosition;

    Renderer objectRenderer;

    void Start()
    {
        targetPosition = movableObject.position;
        objectRenderer = movableObject.GetComponent<Renderer>();
    }

    void Update()
    {
        Tap();
        Drag();
        Snap();
        LerpMove();

        if (Input.GetMouseButton(0))
        {
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);

            Plane plane = new Plane(Vector3.up, Vector3.zero);

            if (plane.Raycast(ray, out float distance))
            {
                targetPosition = ray.GetPoint(distance);
            }
        }
    }

    void Tap()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = arCamera.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == movableObject)
                {
                    selected = !selected;

                    objectRenderer.material.color =
                    selected ? Color.yellow : Color.white;
                }
            }
        }
    }

    void Drag()
    {
        if (!selected) return;

        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            Ray ray = arCamera.ScreenPointToRay(touch.position);

            Plane plane =
            new Plane(Vector3.up, Vector3.zero);

            if (plane.Raycast(ray, out float distance))
            {
                targetPosition =
                ray.GetPoint(distance);
            }
        }
    }

    void Snap()
    {
        float distance =
        Vector3.Distance(
        targetPosition,
        snapZone.position);

        if (distance < snapDistance)
        {
            targetPosition =
            snapZone.position;
        }
    }

    void LerpMove()
    {
        movableObject.position =
        Vector3.Lerp(
        movableObject.position,
        targetPosition,
        Time.deltaTime * moveSpeed
        );
    }
}