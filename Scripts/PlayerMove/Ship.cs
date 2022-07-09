using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

	public float moveSpeed = 8f;
	public float speedRotation = 2f;
	private Rigidbody rg;
	public GameObject JoySticks;

	private Vector3 moveVector;
	private MobileSystem mSystem;


	private CharacterController characterController;


	// Start is called before the first frame update
	void Start()
	{
		rg = gameObject.GetComponent<Rigidbody>();
		mSystem = JoySticks.GetComponent<MobileSystem>();
		characterController = GetComponent<CharacterController>();

	}

	// Update is called once per frame
	void Update()
	{
		CharacterMove();
	}

	public void CharacterMove()
    {
		moveVector = Vector3.zero;
		moveVector.x = mSystem.Horizontal() * moveSpeed;
		moveVector.z = mSystem.Vertical() * moveSpeed;

		if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
			Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedRotation, 0.0f);
			transform.rotation = Quaternion.LookRotation(direct);
        }

		characterController.Move(moveVector* Time.deltaTime);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Fuel" && ResourcesSystem.OgrFuel > ResourcesSystem.Fuel)
		{
			ResourcesSystem.Fuel += 1;
		}

		if (collision.gameObject.tag == "Metal" && ResourcesSystem.OgrMetal > ResourcesSystem.Metal)
		{
			ResourcesSystem.Metal += 1;
		}

		if (collision.gameObject.tag == "Glass" && ResourcesSystem.OgrGlass > ResourcesSystem.Glass)
		{
			ResourcesSystem.Glass += 1;
		}
	}
}
