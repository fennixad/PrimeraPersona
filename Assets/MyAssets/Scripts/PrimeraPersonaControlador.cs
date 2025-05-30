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

    public float rotH;
    public float rotV;

    [Range(1f, 5f)] public float velMov;
    [Range(30f, 90f)] public float velRot;

    CharacterController controller;
    Transform camFalsa;
    #endregion
    // ***********************************************
    #region 2) Funciones de Unity
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        camFalsa = transform.GetChild(0);
    }

    void Start()
    {
        
    }

    void Update()
    {
        inputMov.x = Input.GetAxisRaw("Horizontal");
        inputMov.y = Input.GetAxisRaw("Vertical");

        inputRot.x = Input.GetAxisRaw("Mouse X");
        inputRot.y = Input.GetAxisRaw("Mouse Y");

        rotH += inputRot.x * velRot * Time.deltaTime;
        rotV += inputRot.y * velRot * Time.deltaTime;
        rotV = Mathf.Clamp(rotV, -90f, 90f);

        transform.rotation = Quaternion.Euler(Vector3.up * rotH);
        
        camFalsa.localRotation = Quaternion.Euler(Vector3.right * -rotV);

        #region DESPLAZAMIENTO
        Vector3 dirMovimiento = inputMov.x * transform.right + inputMov.y * transform.forward;
        dirMovimiento.Normalize();

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

