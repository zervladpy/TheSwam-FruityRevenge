using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BackGroundController : MonoBehaviour
{

    // Velocidad a la que se desplazar�n las im�genes
    [SerializeField] float speed;

    // Referencia al renderizador del objeto
    Renderer render;

    // Start se llama antes del primer frame update
    void Start()
    {
        // Inicializamos el componente
        render = GetComponent<Renderer>();
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Vector que representa la cantidad de desplazamiento de la textura
        Vector2 offset = Vector2.up * speed * Time.time;

        // Accedemos al material del objeto y aplicamos el desplazamiento
        render.material.mainTextureOffset = offset;
    }

}
