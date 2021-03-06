﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {

    [Header("Control Keys")]
    public KeyCode rightButton;
    public KeyCode leftButton;
    public KeyCode downButton;

    [Header("Dash Properties")]
    public bool dashLock;
    public float dashTime;
    public float dashOffsetTime;
    public float dashForce;

    [Header("Glide Properties")]
    public float glideForce;

    [Header("Dive Properties")]
    public float diveForce;
    public float diveTime;
    public bool diveLock;

    [Header("Sound")]
    public AudioClip dashSound;
    public float soundVolume = 1.0f;

    private Rigidbody2D rb;
    public PlayerBehavior behavior;
    private Animator anim;

    public float lastLeftPress, lastRightPress;
    public float lastLeftUp, lastRightUp;

	private void Start () {
        rb = GetComponent<Rigidbody2D>();
        behavior = this.GetComponentInChildren<PlayerBehavior>();
        anim = GetComponent<Animator>();
	}

    private void Update()
    {
        bool dashLeft = false, dashRight = false;
        if (behavior.hasWon) {
            return;
        }
        if (Input.GetKeyUp(leftButton)) {
            lastLeftUp = Time.time;
        }

        if (Input.GetKeyUp(rightButton)) {
            lastRightUp = Time.time;
        }

        if (behavior.dead) {
            rb.velocity = Vector2.zero;
        }
        else {
            if (!dashLock)
            {
                if (Input.GetKey(leftButton))
                {
                    Glide(Vector2.left);

                    if (Input.GetKeyDown(leftButton))
                    {
                        if (Time.time - lastLeftUp < 0.15f) {
                            StartCoroutine(Dash(Vector2.left));
                        }
                    }
                }
                if (Input.GetKey(rightButton))
                {
                    Glide(Vector2.right);

                    if (Input.GetKeyDown(rightButton))
                    {
                        if (Time.time - lastRightUp < 0.15f) {
                            StartCoroutine(Dash(Vector2.right));
                        }
                    }
                }
            }
            if (!diveLock && Input.GetKeyDown(downButton))
            {
                StartCoroutine(Dive());
            }
        }
    }

    void FixedUpdate() {
        if (behavior.hasWon) {
            return;
        }
        if (rb.velocity.y < -3f) {
            print(rb.velocity);
            anim.SetBool("dash", true);
            anim.SetBool("glide", false);
        } else {
            anim.SetBool("dash", false);
            if (Mathf.Abs(rb.velocity.x) > 2f) {
                anim.SetBool("glide", true);
                if (rb.velocity.x > 0) {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                } 
                if (rb.velocity.x < 0) {
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);         
                }
            } else {
                anim.SetBool("glide", false);
            }
        }
    }

    private IEnumerator MovementHandler(KeyCode key, Vector2 dir)
    {
        float timer = dashOffsetTime; 
        yield return new WaitForEndOfFrame();
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (Input.GetKeyDown(key))
            {
                yield return Dash(dir);
                yield break;
            }
            yield return null;
        }
    }

	private IEnumerator Dash(Vector2 dir)
    {
        if (!dashLock)
        {
            dashLock = true;
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.AddForce(dir * dashForce);
            // AudioSource audio = AudioSourceController.GetAudioSourceController().GetComponent<AudioSource>();
		    // audio.PlayOneShot(dashSound, soundVolume);
            yield return new WaitForSeconds(dashTime);
            dashLock = false;
        }
    }

    private IEnumerator Dive()
    {
        if(!diveLock)
        {
            print("DIVE");
            diveLock = true;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.down * diveForce);
            yield return new WaitForSeconds(diveTime);
            diveLock = false;
        }
    }
    private void Glide(Vector2 dir)
    {
        if (!dashLock)
        {
            rb.AddForce(glideForce * dir);
        }
    }
}
