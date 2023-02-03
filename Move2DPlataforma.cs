using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2DPlataforma : MonoBehaviour
{
    // Declarando la variable tipo Rigidbody, es como un objeto que se utiliza en unity.
    private Rigidbody2D rb2d;

    [Header("Movimiento")]
    //Declaramos la variable para el movimiento horizontal.
    private float Mov_Horz = 0f;
    //Declaramos la velocidad de movimiento. 
    [SerializeField] private float vel_mov;
    //Declaramos el suavizado del movimiento.
    [SerializeField] private float Sua_Mov;
    //Iniciamos la velocidad del eje z en 0
    private Vector3 vel = Vector3.zero;
    // Para identificar en que direccion estamos mirando.
    private bool mirando = true;

    [Header("Salto")]
    //Con la fuerza que salta el caballero.
    [SerializeField] private float Fuerza_Salto;
    //Para identificar en que lugares se puede salta    r.
    [SerializeField] private LayerMask QES;
    //Esta caja es la genera la informacion si estamos en el suelo o no.
    [SerializeField] private Transform CTRL_Suelo;
    //Genera un caja que toma la informacion que recolecto el controlSuelo.
    [SerializeField] private Vector3 DimensionCaja;
    //Determina si la informacion que esta en la caja es valida para saltar.
    [SerializeField] private bool Suelo;
    //Cuando el PJ esta en el aire y que este no pueda saltar mas de una vez.
    private bool salto = false;

    //[Header("Animacion")]
    //private Animator ani;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //ani = GetComponent<Animator>();
    }
    private void Update()
    {
        Mov_Horz = Input.GetAxisRaw("Horizontal") * vel_mov;
        //ani.SetFloat("Horizontal", Mathf.Abs(Mov_Horz));
        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }
    }
    private void FixedUpdate()
    {
        // Con esto mandamos la se�al para el salto.
        Suelo = Physics2D.OverlapBox(CTRL_Suelo.position, DimensionCaja, 0f, QES);
        //ani.SetBool("JumpSuelo", Suelo);
        //ani.SetFloat("JumpVelocity", rb2d.velocity.y);

        //Mover
        Mover(Mov_Horz * Time.fixedDeltaTime, salto);

        //Desactivamos el salto, para que no siempre se encuentre saltando, osea que no este en el aire dando saltos.
        salto = false;
    }

    private void Mover(float mover, bool salto)
    {
        Vector3 Vel_Obj = new Vector2(mover, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, Vel_Obj, ref vel, Sua_Mov);

        if (mover > 0 && !mirando)
        {
            //Girar
            Girar();
        }
        else if (mover < 0 && mirando)
        {
            //Girar
            Girar();
        }

        if (Suelo && salto)
        {
            // para eso le desactivamos la variable que indica que esta en el suelo.
            Suelo = false;
            // y le agregamos una fuerza en su eje Y.
            rb2d.AddForce(new Vector2(0f, Fuerza_Salto));
        }
    }

    private void Girar()
    {
        // se compara la variable. Si, esta sigue siendo True o si es False.
        mirando = !mirando;
        //Declaramos el vector3 del pj para transfomrar la escala de pj.
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        // le damos color a los gizmos
        Gizmos.color = Color.red;
        // dibujamos la caja.
        Gizmos.DrawWireCube(CTRL_Suelo.position, DimensionCaja);
    }
}
