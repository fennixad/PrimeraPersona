using UnityEngine;

/// <summary>
/// DESCRIPCION:
/// 
/// </summary>

[RequireComponent (typeof(CharacterController))]
public class PrimeraPersonaControlador : MonoBehaviour
{
    // ***********************************************
    #region 1) Definicion de variables
    public Vector2 inputMov, inputRot;

    public float rotH; // rotacion horizontal, lado der/izq
    public float rotV; // rotacion vertical, arriba/abajo

    [Range (1f, 3f)]
    public float velMov;

    [Range (30f, 60f)]
    public float velRot;

    public bool enSuelo;

    CharacterController controller;
    Transform camFalsa;

    float gravedad;

    Vector3 dirMovimiento;

    #endregion
    // ***********************************************
    #region 2) Funciones de Unity
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        camFalsa = transform.GetChild(0);

        gravedad = -9.81f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Almacenamos los valores de los ejes virtuales
        // "Horizontal" y "Vertical"
        inputMov.x = Input.GetAxisRaw("Horizontal"); // Teclas D ó A <-> 1 ó -1
        inputMov.y = Input.GetAxisRaw("Vertical");   // Teclas W ó S <-> 1 ó -1

        // Almacenamos los valores de los ejes virtuales
        // "Mouse X" y "Mouse Y"
        inputRot.x = Input.GetAxisRaw("Mouse X");
        inputRot.y = Input.GetAxisRaw("Mouse Y");

        rotH += inputRot.x * velRot * Time.deltaTime;
        rotV += inputRot.y * velRot * Time.deltaTime;

        // Se marca unos limites minimos y maximos a la variable rotV
        // a traves del resultado de Mathf.Clamp
        rotV = Mathf.Clamp(rotV, -90f, 90f);

        #region ROTACION DE PERSONAJE Y CAMARA FALSA

        // rotacion del personaje de lado a lado segun el uso "lateral"
        // del raton
        transform.rotation = Quaternion.Euler(Vector3.up * rotH);
        camFalsa.localRotation = Quaternion.Euler(Vector3.right * -rotV);
        #endregion

        #region DESPLAZAMIENTO DEL PERSONAJE
        dirMovimiento = inputMov.x * transform.right + inputMov.y * transform.forward;
        //dirMovimiento.Normalize();

        enSuelo = controller.isGrounded;

        if (enSuelo && dirMovimiento.y < 0f) dirMovimiento.y = 0f;

        // Jump
        //if (Input.GetButtonDown("Jump") && enSuelo) dirMovimiento.y = Mathf.Sqrt(2f * -2.0f * gravedad);
        

        // Aplicacion de la gravedad
        dirMovimiento.y += gravedad * Time.deltaTime;

        Debug.DrawRay(transform.position, dirMovimiento, Color.red);
        controller.Move(dirMovimiento * velMov * Time.deltaTime);
        #endregion

    }
    #endregion
    // ***********************************************
    #region 3) Funciones originales

    #endregion
    // ***********************************************
}
