﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossmovement : MonoBehaviour {

	[SerializeField]
	public float moveSpeed = 5f;

	[SerializeField]
	public float frequency = 20f;

	[SerializeField]
	public float magnitude = 0.5f;

    public float farmove = 8.5f;

	bool facingRight = true;

	Vector3 pos, localScale;

	// Use this for initialization
	void Start () {
		
		pos = transform.position;

		localScale = transform.localScale;

	}
	
	// Update is called once per frame
	void Update () {
		
		CheckWhereToFace ();

		if (facingRight)
			MoveRight ();
		else
			MoveLeft ();
	}

	void CheckWhereToFace()
	{
		if (pos.x < -farmove)
			facingRight = true;
		
		else if (pos.x > farmove)
			facingRight = false;

		if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
			localScale.x *= -1;

		transform.localScale = localScale;

	}

	void MoveRight()
	{
		pos += transform.right * Time.deltaTime * moveSpeed;
		transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
	}

	void MoveLeft()
	{
		pos -= transform.right * Time.deltaTime * moveSpeed;
		transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
	}

}