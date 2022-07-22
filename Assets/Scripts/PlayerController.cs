using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TMP_Text nameDisplay;
    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float minX, maxX, minY, maxY;

    private PhotonView view;
    private Animator anim;
    private Health health;
    private LineRenderer lineRend;
    private float resetSpeed;


    private void Start()
    {
        resetSpeed = speed;
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        health = FindObjectOfType<Health>();
        lineRend = FindObjectOfType<LineRenderer>();

        if (view.IsMine)
            nameDisplay.text = PhotonNetwork.NickName;
        else
            nameDisplay.text = view.Owner.NickName;
    }

    private void Update()
    {
        if (view.IsMine)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;

            Wrap();

            if(Input.GetKeyDown(KeyCode.Space) && moveInput != Vector2.zero)
            {
                StartCoroutine(Dash());
            }

            if (moveInput == Vector2.zero)
                anim.SetBool("isRunning", false);
            else
                anim.SetBool("isRunning", true);

            lineRend.SetPosition(0, transform.position);
        }
        else
        {
            lineRend.SetPosition(1, transform.position);
        }
    }

    IEnumerator Dash()
    {
        speed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        speed = resetSpeed;
    }

    private void Wrap()
    {
        if(transform.position.x < minX)
        {
            transform.position = new Vector2(maxX, transform.position.y);
        }

        if (transform.position.x > maxX)
        {
            transform.position = new Vector2(minX, transform.position.y);
        }

        if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, maxY);
        }

        if (transform.position.y > maxY)
        {
            transform.position = new Vector2(transform.position.x, minY);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!view)
            return;

        if(view.IsMine)
        {
            if (collision.tag == "Enemy")
            {
                health.TakeDamage();
            }
        }     
    }
}
