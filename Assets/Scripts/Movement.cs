using System.Collections;
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

    private Rigidbody2D rb;

	private void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
    private void Update()
    {
        if (!dashLock)
        {
            if (Input.GetKey(leftButton))
            {
                Glide(Vector2.left);
                if (Input.GetKeyDown(leftButton))
                    StartCoroutine(MovementHandler(leftButton, Vector2.left));
            }
            if (Input.GetKey(rightButton))
            {
                Glide(Vector2.right);
                if (Input.GetKeyDown(rightButton))
                    StartCoroutine(MovementHandler(rightButton, Vector2.right));
            }
        }
        if (!diveLock && Input.GetKeyDown(downButton))
        {
            StartCoroutine(Dive());
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
