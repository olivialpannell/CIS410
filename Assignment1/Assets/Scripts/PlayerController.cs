using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

	public float speed;
	public Text countText;
	public Text winText;
	private Rigidbody rb;
	public LayerMask groundLayers;
	public float jump;
	public SphereCollider sc;
	private int count;
	private bool doublejump;
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		sc = GetComponent<SphereCollider>();
		count = 0;
		SetCountText();
		winText.text = "";
	}
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.AddForce(movement * speed);
        if (IsGrounded())
        {
            doublejump = true;
        }
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (IsGrounded())
			{
				rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
			}
			else
			{
				if (doublejump)
				{
					rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
					doublejump = false;
				}
			}
		}

	}
	private bool IsGrounded()
	{
		Vector3 ground =new Vector3(sc.bounds.center.x, sc.bounds.min.y, sc.bounds.center.z) ;
		return Physics.CheckCapsule(sc.bounds.center, ground, sc.radius * .9f, groundLayers);
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Pick Up"))
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText();
		}
	}
	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();
		if (count >= 8)
		{
			winText.text = "You Win!";
		}
	}
}