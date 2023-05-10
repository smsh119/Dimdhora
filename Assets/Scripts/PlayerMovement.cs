using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float yPos = 1f;
    [SerializeField] float leftClampValue = -2.3f;
    [SerializeField] float rightClampValue = 2.3f;

    [SerializeField] ParticleSystem eggBreakParticle;

    [SerializeField] bool pcControl = true;
    
    void FixedUpdate()
    {
        if(pcControl)Movement();
        MovementMobile();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessEggCatch(collision);
    }

    private void Movement()
    {
        var mousePos = Input.mousePosition;
        var mousePosInWorldPoint = Camera.main.ScreenToWorldPoint(mousePos);
        var playerPos = new Vector2(Mathf.Clamp(mousePosInWorldPoint.x, leftClampValue, rightClampValue), yPos);
        transform.position = playerPos;
    }

    private void MovementMobile()
    {
        if (Input.touchCount > 0)
        {
            var touchPos = Input.GetTouch(0).position;
            var touchPosInWorldPoint = Camera.main.ScreenToWorldPoint(touchPos);
            var playerPos = new Vector2(Mathf.Clamp(touchPosInWorldPoint.x, leftClampValue, rightClampValue), yPos);
            transform.position = playerPos;
        }
    }


    private void ProcessEggCatch(Collision2D collision)
    {
        collision.gameObject.GetComponent<EggMovement>().DestroyOwn();
        if(gameObject.GetComponent<AudioSource>()!=null)gameObject.GetComponent<AudioSource>().Play(); //check for audio source

        if (collision.gameObject.tag=="Egg")  // just to check which egg is caught
        {
            FindObjectOfType<ScoreManager>().EggsCaught();
            eggBreakParticle.Play();
        }
        else
        {
            FindObjectOfType<Destroyer>().PlayParenaAudio(); // case of catching rotten egg
            FindObjectOfType<ScoreManager>().EggsDropped();  // case of catching rotten egg
        }
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}