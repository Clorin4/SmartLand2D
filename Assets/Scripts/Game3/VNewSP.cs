using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VNewSP : MonoBehaviour
{
    //Zorrito activity
    private Animator ObjectAnim;

    // Start is called before the first frame update
    private void Start()
    {
        ObjectAnim = GetComponent<Animator>();
    }

    //Zorrito nada
    public void StartNothing()
    {
        StartCoroutine(Nothing());
    }
    //zorrito pierde
    public void StartLose()
    {
        StartCoroutine(Lose());
    }
    //zorrito habla
    public void StartTalk()
    {
        StartCoroutine(Talk());
    }
    //zorrito gana
    public void StartWin()
    {
        StartCoroutine(Win());
    }
    //-----Coroutines-----
    IEnumerator Nothing()
    {
        ObjectAnim.SetBool("Parpadea", true);
        yield return new WaitForSeconds(1.5f);
    }

    IEnumerator Lose()
    {
        ObjectAnim.SetBool("Pierde", true);
        yield return new WaitForSeconds(2.0f);
        ObjectAnim.SetBool("Pierde", false);
    }

    IEnumerator Talk()
    {
        ObjectAnim.SetBool("Hablando", true);
        yield return new WaitForSeconds(3.0f);
        ObjectAnim.SetBool("Hablando", false);
    }

    IEnumerator Win()
    {
        ObjectAnim.SetBool("Ganador", true);
        yield return new WaitForSeconds(2.0f);
        ObjectAnim.SetBool("Ganador", false);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
