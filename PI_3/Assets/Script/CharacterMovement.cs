using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento do personagem
    public float runSpeed = 10f; // Velocidade de corrida do personagem

    public SceneTransition transition;
    public CharacterMovement script;


    private Rigidbody rb;
    private Animator anim;
    private bool isWalking; // Vari�vel para verificar se o personagem est� andando

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obt�m o componente Rigidbody
        anim = GetComponent<Animator>(); // Obt�m o componente Animator
        isWalking = false; // Inicialmente, o personagem n�o est� andando
        transition = FindObjectOfType<SceneTransition>(GetComponent<SceneTransition>());
        script = GetComponent<CharacterMovement>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Obt�m a entrada horizontal (A e D ou Setas)
        float moveVertical = Input.GetAxis("Vertical"); // Obt�m a entrada vertical (W e S ou Setas)

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); // Cria um vetor de movimento com base nas entradas

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && (Mathf.Abs(moveHorizontal) > 0 || Mathf.Abs(moveVertical) > 0); // Verifica se o personagem est� correndo

        Vector3 movePosition = transform.position + movement * (isRunning ? runSpeed : moveSpeed) * Time.fixedDeltaTime; // Calcula a posi��o de movimento

        rb.MovePosition(movePosition); // Move o Rigidbody para a nova posi��o

        // Ativa a anima��o com base na entrada do jogador
        if (movement.magnitude > 0)
        {
            if (isRunning)
            {
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
                anim.SetBool("Idle", false); 
                anim.SetBool("Grab", false);
            }
            else
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);
                anim.SetBool("Grab", false);
            }
            anim.SetBool("Idle", false);

            // Calcula a rota��o para olhar na dire��o do movimento
            Quaternion newRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Euler(0f, newRotation.eulerAngles.y, 0f); // Mant�m apenas a rota��o no eixo Y
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            anim.SetBool("Idle", true);
            anim.SetBool("Grab", false);
        }

        // Define isWalking como verdadeiro se o jogador estiver pressionando uma tecla de movimento, exceto a tecla Shift
        isWalking = (Mathf.Abs(moveHorizontal) > 0 || Mathf.Abs(moveVertical) > 0) /*&& !Input.GetKey(KeyCode.LeftShift)*/;

        if (transition.fase1Ativa ^ transition.fase2Ativa && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("Grab", true);
            anim.SetBool("Run", false);
            anim.SetBool("Idle", false);

            if (script != null && Input.GetKeyDown(KeyCode.E))
            {
                script.enabled = false;
            }
        }
        else
        {
            anim.SetBool("Grab", false);
        }

    }
}
