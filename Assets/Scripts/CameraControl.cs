using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public CameraControl instance;
    public StageManager stageManager;

    public Camera cam;
    public Transform cameraTransform;

    public Transform followTransform;

    public bool AllowRotation = false;
    public bool AllowZoom = true;
    public bool AllowDragMove = true;
    public bool AllowEdgePan = true;

    public float fastSpeed;
    public float normalSpeed;
    public float movementSpeed;
    public float movementTime;

    public float rotationAmount;

    public float zoomAmountMouseMultiplier;
    public float rotateMouseSensitivity;
    public float rotateKeyboardSensitivity;
    public Vector3 zoomAmount;
    public bool invertDrag;

    private Vector3 newPosition;
    private Quaternion newRotation;
    private Vector3 rotateStartPosition;
    private Vector3 rotateCurrentPosition;
    private Vector3 newZoom;

    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;
    public float DragThresholdSensitivity = 1f;

    private void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }


    void Update()
    {
        if(followTransform == null)
        {
            HandleMovementInput();
            HandleMouseInput();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            followTransform = null;
        }
    }

    void HandleMouseInput()
    {
        if (AllowZoom) HandleZoom();
        if (AllowRotation) HandleRotation();
        if (AllowDragMove) HandleMove();        
    }

    void HandleMove()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
                dragCurrentPosition = dragStartPosition;
            }
        }
        if (Input.GetMouseButton(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                if (invertDrag)
                {
                    newPosition = transform.position - dragStartPosition + dragCurrentPosition;
                }
                else
                {
                    newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                }

            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            float dragDist = Vector3.SqrMagnitude(dragStartPosition - dragCurrentPosition);
            if(dragDist < DragThresholdSensitivity)
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    Vector3 moveLocation = ray.GetPoint(entry);
                    stageManager.Move(moveLocation);
                }
            }
            
        }
    }

    void HandleZoom()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount * zoomAmountMouseMultiplier;
        }
    }

    void HandleRotation()
    {
        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;
            Vector3 diff = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.forward * (-diff.x / rotateMouseSensitivity));
        }
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += transform.forward * movementSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += transform.forward * -movementSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += transform.right * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += transform.right * -movementSpeed;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount * rotateKeyboardSensitivity);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount * rotateKeyboardSensitivity);
        }

        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
