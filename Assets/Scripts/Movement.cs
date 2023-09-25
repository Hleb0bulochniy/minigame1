using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Movement : MonoBehaviour
{
    void Start()
    {
        
    }
    public float speed = 5f;
    public Vector2 move;
    public Transform teleportDestination;
    public Transform teleportFrom;
    public Rigidbody2D rb;
    private int active = 0;  //0 - игра, 1 - проигрыш, 2 - выигрыш
    public int lines = 2;
    public string on = "line";
    private int maxscore = 9;
    public Transform win, lose;

    void Update()
    {
        if (active == 0) { rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime); }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (on == "line") { maxscore++; }
            else if (on == "down") 
            {
                maxscore--;
                on = "line";
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (on == "line") { maxscore++; }
            else if (on == "up") { maxscore--; on = "line"; }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (on == "line") { maxscore++; }
            else if (on == "right") { maxscore--; on = "line"; }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (on == "line") { maxscore++; }
            else if (on == "left") { maxscore--; on = "line"; }
        }
        print(maxscore);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("end of the line") & lines>=1)
        {
            transform.position = teleportDestination.position;
            lines--;
        }
        if (collision.gameObject.CompareTag("end of the line") & lines <= 0)
        {
            if (maxscore == 0) { win.transform.position = new Vector3(0, 0, 0); active = 2; }
            else { lose.transform.position = new Vector3(0, 0, 0); active = 1; }
        }
    }
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("down"))
        {
            on = "down";
        }
        else if (trigger.gameObject.CompareTag("up"))
        {
            on = "up";
        }
        else if (trigger.gameObject.CompareTag("left"))
        {
            on = "left";
        }
        else if (trigger.gameObject.CompareTag("right"))
        {
            on = "right";
        }
    }
    private void OnTriggerExit2D(Collider2D trigger)
    {
        on = "line";
    }
}
