using UnityEngine;

public class DebugLogger : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Script DebugLogger iniciado.");
    }

    void Update()
    {
        Debug.Log("Atualiza��o do frame: " + Time.frameCount);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colis�o detectada com: " + other.gameObject.name);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Colis�o encerrada com: " + other.gameObject.name);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colis�o detectada com: " + collision.gameObject.name);
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Colis�o encerrada com: " + collision.gameObject.name);
    }

    void OnDestroy()
    {
        Debug.Log("Objeto destru�do: " + gameObject.name);
    }
}
