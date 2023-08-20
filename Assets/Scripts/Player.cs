using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    CharacterController controller;
    private Vector3 dir = new Vector3();
    public float speed;
    public float maxSpeed;
    
    public float force;
    public float gravity;

    public int coins;
    public int coinsAll;
    public Text coinsText;

    public Score scoreScript;

    private int lineToMove = 1;
    public float lineDistance = 4;
    Animator anim;

    bool  gameOver = false;
    public GameObject losePanel;    

    
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        coinsAll = PlayerPrefs.GetInt("coins");
        //PlayerPrefs.DeleteKey("coins");

        Time.timeScale = 1;
        StartCoroutine(SpeedInc());
    }

    void Update()
    {
        if(SwipeController.swipeRight && !gameOver)
        {
            if(lineToMove < 2)
                lineToMove++;
        }

        if(SwipeController.swipeLeft && !gameOver)
        {
            if(lineToMove > 0)
                lineToMove--;
        }

        if(SwipeController.swipeUp && !gameOver)
        {
            if(controller.isGrounded)
            Jump();
        }

        //if(controller.isGrounded == true)
           // dirtParticle.Play();

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if(lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

            //transform.position = targetPosition;
        if(transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if(moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    void Jump()
    {
        dir.y = force;
        anim.SetTrigger("Jump_trig");
        playerAudio.PlayOneShot(jumpSound, 1.0f);
    }

    void FixedUpdate()
    {
        if(!gameOver)
        {
            dir.z = speed;
            dir.y += gravity * Time.fixedDeltaTime;
            controller.Move(dir * Time.fixedDeltaTime);
        }
        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "obstacle")
        {
            int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
            PlayerPrefs.SetInt("lastRunScore", lastRunScore);//сохраняем в реестр

            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);
            gameOver = true;
            Invoke("Die", 2);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            coins++;
            coinsAll += coins;
            PlayerPrefs.SetInt("coins", coinsAll);//сохраняем в реестр
            coinsText.text = coins.ToString();
            Destroy(other.gameObject);
        }
    }

    void Die()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator SpeedInc()
    {
        yield return new WaitForSeconds(2);
        if(speed < maxSpeed)
        {
            speed += 1;
            StartCoroutine(SpeedInc());
        }
    }
}
