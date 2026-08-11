using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoPrueba : MonoBehaviour
{
    public float velocidad = 4f;
    public float sensibilidadMouse = 0.15f;

    private CharacterController controlador;
    private Camera camara;

    private float rotacionX = 0f;

    void Start()
    {
        controlador = GetComponent<CharacterController>();
        camara = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Mover();
        Mirar();
    }

    void Mover()
    {
        Vector2 entrada = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed)
                entrada.y += 1;

            if (Keyboard.current.sKey.isPressed)
                entrada.y -= 1;

            if (Keyboard.current.dKey.isPressed)
                entrada.x += 1;

            if (Keyboard.current.aKey.isPressed)
                entrada.x -= 1;
        }

        Vector3 movimiento =
            transform.right * entrada.x +
            transform.forward * entrada.y;

        controlador.Move(
            movimiento.normalized *
            velocidad *
            Time.deltaTime
        );
    }

    void Mirar()
    {
        if (Mouse.current == null)
            return;

        Vector2 movimientoMouse =
            Mouse.current.delta.ReadValue();

        float mouseX =
            movimientoMouse.x * sensibilidadMouse;

        float mouseY =
            movimientoMouse.y * sensibilidadMouse;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -80f, 80f);

        camara.transform.localRotation =
            Quaternion.Euler(rotacionX, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}