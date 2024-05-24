using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator; 
    public int playerIndex; // Indica el índice del jugador
    public string playerTag; // Indica si es "Player1" o "Player2"

    private void Start()
    {
        animator = GetComponent<Animator>();
        // Puedes usar el playerIndex o playerTag para ajustar la animación, configuración, etc.
        Debug.Log("Player Index: " + playerIndex + ", Player Tag: " + playerTag);
    }

    // Método para configurar el índice del jugador y la etiqueta del jugador
    public void SetPlayerIndexAndTag(int index, string tag)
    {
        playerIndex = index;
        Debug.Log(index);
        playerTag = tag;
        Debug.Log(tag);
    }

    public void StartDamageAnimation()
    {
        StartCoroutine(Hurt());
    }

    public void StartAttackAnimation()
    {
        StartCoroutine(Attacking());
    }

    public void StartJumpingAnimation()
    {
        StartCoroutine(Jumping());
    }

    public void StartRunningAnimation()
    {
        StartCoroutine(Running());
    }

    public void StartRunningAnimation2()
    {
        StartCoroutine(Running2());
    }

    public void StopRunningAnimation()
    {
        StartCoroutine(StopRunning());
    }

    public void StartVictoryAnimation()
    {
        animator.SetBool("Ganador", true);
    }

    public void StartLoseAnimation()
    {
        animator.SetBool("Pierde", true);
    }

    IEnumerator Hurt()
    {
        yield return new WaitForSeconds(1.3f);
        animator.SetBool("Dañado", true); // Activa la animación de daño en el Animator
        yield return new WaitForSeconds(1f);
        animator.SetBool("Dañado", false);
    }

    IEnumerator Attacking()
    {
        animator.SetBool("Atacando", true);
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("Atacando", false);
    }

    IEnumerator Jumping()
    {
        //animator.SetBool("Atacando", true);
        yield return new WaitForSeconds(1.2f);
        //animator.SetBool("Atacando", false);
    }

    IEnumerator Running()
    {
        animator.SetBool("Corriendo", true);
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("Corriendo", false);
    }

    IEnumerator Running2()
    {
        animator.SetBool("Corriendo", true);
        yield return new WaitForSeconds(0);
        
    }

    IEnumerator StopRunning()
    {
        animator.SetBool("Corriendo", false);
        yield return new WaitForSeconds(0);
    }

}
