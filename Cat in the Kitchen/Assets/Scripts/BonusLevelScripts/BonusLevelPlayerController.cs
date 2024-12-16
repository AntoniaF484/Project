using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelPlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    private float horizontalInput;

    private float verticalInput;

    public float turnSpeed;

    public float catPower;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        verticalInput = Input.GetAxis("Vertical");
        playerRb.AddRelativeForce(Vector3.forward * verticalInput * catPower);
    }
}
