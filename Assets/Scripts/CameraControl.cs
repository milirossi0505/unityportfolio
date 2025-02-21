using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;  // El objeto alrededor del cual rotará la cámara
    public float distance = 10f;  // Distancia inicial de la cámara desde el objeto
    public float height = 5f;  // Altura inicial de la cámara
    public float rotationSpeed = 5f;  // Velocidad de rotación

    public bool onlyHorizontal;
    private float currentRotationX = 0f;
    private float currentRotationY = 0f;
    private Vector3 offset;

    void Start()
    {
        // Establecer la posición inicial de la cámara con base en la distancia y altura
        offset = new Vector3(0, height, -distance);
        transform.position = target.position + offset;
        transform.LookAt(target);  // La cámara siempre mira al objetivo
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

        // Limitar la rotación vertical (evitar que la cámara se voltee)
        currentRotationY = Mathf.Clamp(currentRotationY, -80f, 80f);

        // Calcular la nueva posición de la cámara
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        transform.position = target.position + rotation * offset;

        // Hacer que la cámara siempre mire al objeto
        transform.LookAt(target);
    }
}

