using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    private Vector3 dir;
    public float speed;
    public float maxSpeed;

    public float force;
    public float gravity;

    private int lineToMove = 1; // линия по которой бежим, 0 левая, 1 средняя, 2 правая
    public float lineDistance = 4;// Растояние между линиями

    Animator anim;
    public GameObject losePanel; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        StartCoroutine(SpeedInc());
    }

    void Update()
    {   
        //проверка случился ли свайп вправо
        if(SwipeController.swipeRight) 
        {
            if(lineToMove < 2)
                lineToMove++; 
        }

        if(SwipeController.swipeLeft)
        {
            if(lineToMove > 0)
                lineToMove--;
        }

        if(SwipeController.swipeUp)
        {
            if(controller.isGrounded)
            Jump();
        }

        //расчитываем позицию в которой мы окажемся после свайпа
        //targetPosition хранит в себе координаты z и y теже самые которые меняються сами по себе от других скриптов
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if(lineToMove == 0)//если стоим с лева, то двигаемся в право
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)//если стоим с право, то двигаемся в лево
            targetPosition += Vector3.right * lineDistance;

        if(transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if(moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);

    }

    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    void Jump()
    {
        dir.y = force;
        anim.SetTrigger("Jump_trig");
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "obstacle")
        {
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);
            Invoke("Die", 2);
        }
    }

    IEnumerator SpeedInc()
    {
        yield return new WaitForSeconds(0.5f);
        if(speed < maxSpeed)
        {
            speed += 3;
            StartCoroutine(SpeedInc());
        }
    }

    void Die()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }
}
