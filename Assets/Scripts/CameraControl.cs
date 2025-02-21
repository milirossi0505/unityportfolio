using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;  // El objeto alrededor del cual rotar� la c�mara
    public float distance = 10f;  // Distancia inicial de la c�mara desde el objeto
    public float height = 5f;  // Altura inicial de la c�mara
    public float rotationSpeed = 5f;  // Velocidad de rotaci�n

    public bool onlyHorizontal;
    private float currentRotationX = 0f;
    private float currentRotationY = 0f;
    private Vector3 offset;

    void Start()
    {
        // Establecer la posici�n inicial de la c�mara con base en la distancia y altura
        offset = new Vector3(0, height, -distance);
        transform.position = target.position + offset;
        transform.LookAt(target);  // La c�mara siempre mira al objetivo
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float horizontal = Input.GetAxis("Mouse X");
            float vertical = Input.GetAxis("Mouse Y");

            if (onlyHorizontal)
            {
                vertical = 0;
            }

            currentRotationX += horizontal * rotationSpeed;
            currentRotationY -= vertical * rotationSpeed;
        }

        // Limitar la rotaci�n vertical (evitar que la c�mara se voltee)
        currentRotationY = Mathf.Clamp(currentRotationY, -80f, 80f);

        // Calcular la nueva posici�n de la c�mara
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        transform.position = target.position + rotation * offset;

        // Hacer que la c�mara siempre mire al objeto
        transform.LookAt(target);
    }
}

