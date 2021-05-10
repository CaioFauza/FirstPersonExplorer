using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    GameManager gm;
    float followingSpeed = 15.0f;
    bool foundPlayer = false;
    Animator animator;
    Rigidbody rb;
    Transform player;

    void Start()
    {
        gm = GameManager.GetInstance();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        animator.SetBool("FoundPlayer", foundPlayer ? true : false);
        FollowPlayer();
    }
    void FixedUpdate()
    {
        Search();
    }

    void Search()
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        origin.y += 1.0f;

        Debug.DrawRay(origin, transform.forward * 20.0f, Color.red);
        if (Physics.SphereCast(origin, 4.0f, transform.forward, out hit, 20.0f))
        {
            foundPlayer = hit.transform.CompareTag("Player") ? true : false;
        }
        else
        {
            foundPlayer = false;
        }
    }

    void FollowPlayer()

    {
        if (gm.gameState != GameManager.GameState.GAME) return;
        Vector3 direction = Vector3.MoveTowards(transform.position, player.position, followingSpeed * Time.fixedDeltaTime);
        rb.MovePosition(direction);
        transform.LookAt(player);
    }
}
