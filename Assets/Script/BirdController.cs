using UnityEngine;
using System.Collections;
using System;
using MoleMole;
public class BirdController : MonoBehaviour
{
    public float speed;

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("jump");
        }
    }
   
    void OnTriggerExit2D(Collider2D col)
    {
        NotificationCenter.GetInstance().PostNotification("ScoreAdd");
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (this.enabled)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            NotificationCenter.GetInstance().PostNotification("GameOver");
            Singleton<ContextManager>.Instance.Push(new RestartContext());
        }
        this.enabled = false;
        
    }

}




