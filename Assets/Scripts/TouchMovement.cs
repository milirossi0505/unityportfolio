using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    public float moveSpeed = 0.001f;   // Velocidad de movimiento
    public float rotationSpeed = 0.002f; // Velocidad de rotaci�n
    public float zoomSpeed = 0.001f;  // Velocidad de zoom
    public float minZoom = 5f;      // Zoom m�nimo
    public float maxZoom = 50f;     // Zoom m�ximo
    public Transform target;        // El objetivo alrededor del cual rotar� la c�mara (por ejemplo, el centro del escenario)

    private Vector2 lastTouchPosition;
    private Vector2 deltaTouchPosition;

    void Update()
    {
        // Movimiento con un dedo o clic del rat�n
        if (Input.touchCount == 1 || Input.GetMouseButton(0))
        {
            Vector2 currentTouchPosition = Input.touchCount == 1 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

            if (Input.touchCount == 1 || Input.GetMouseButtonDown(0))
            {
                lastTouchPosition = currentTouchPosition;
            }

            deltaTouchPosition = currentTouchPosition - lastTouchPosition;
            transform.Translate(-deltaTouchPosition.x * moveSpeed, 0, -deltaTouchPosition.y * moveSpeed);

            lastTouchPosition = currentTouchPosition;
        }

        // Rotaci�n alrededor de un punto (p. ej. el centro del escenario)
        if (Input.touchCount == 2 || (Input.GetMouseButton(0) && Input.GetMouseButton(1)))
        {
            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
                Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

                float prevTouchDeltaMag = (touch1PrevPos - touch2PrevPos).magnitude;
                float touchDeltaMag = (touch1.position - touch2.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // Rotar alrededor del objetivo (el centro del escenario)
                transform.RotateAround(target.position, Vector3.up, deltaMagnitudeDiff * rotationSpeed);
            }
            else if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            {
                float rotationAmount = Input.GetAxis("Mouse X") * rotationSpeed;

                // Rotar alrededor del objetivo (el centro del escenario)
                transform.RotateAround(target.position, Vector3.up, rotationAmount);
            }
        }

        // Zoom con gesto de pellizco (pantalla t�ctil) o rueda del rat�n (escritorio)
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Distancia entre los dos toques
            float previousTouchDelta = (touch1.position - touch2.position).magnitude;
            float currentTouchDelta = (touch1.position - touch2.position).magnitude;

            float delta = previousTouchDelta - currentTouchDelta;

            // Controlar el zoom
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView + delta * zoomSpeed, minZoom, maxZoom);
        }

        // Zoom con la rueda del rat�n (solo para escritorio)
        if (Input.mouseScrollDelta.y != 0)
        {
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - Input.mouseScrollDelta.y * zoomSpeed, minZoom, maxZoom);
        }
    }
}
public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 2f;   // Velocidad de zoom
    public float minDistance = 5f; // Distancia m�nima de la c�mara
    public float maxDistance = 20f; // Distancia m�xima de la c�mara

    private float currentDistance; // Distancia actual de la c�mara

    void Start()
    {
        // Inicializa la distancia de la c�mara
        currentDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    void Update()
    {
        // Detectar la entrada de la rueda del rat�n
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Cambiar la distancia de la c�mara seg�n la rueda del rat�n
        currentDistance -= scrollInput * zoomSpeed;

        // Limitar la distancia de la c�mara
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // Actualizar la posici�n de la c�mara
        Vector3 direction = transform.forward * currentDistance;
        Camera.main.transform.position = direction;
    }
}