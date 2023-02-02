using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movTOPDOWN : MonoBehaviour
{
    private Rigidbody2D rbd2D;

    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Vector2 direccion;

    private void Start()
    {
        rbd2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        direccion = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        rbd2D.MovePosition(rbd2D.position + direccion * velocidadMovimiento * Time.fixedDeltaTime);
    }
}


// Para el repositorio.