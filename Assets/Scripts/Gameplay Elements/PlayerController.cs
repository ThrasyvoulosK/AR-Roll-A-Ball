using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    public FixedJoystick fixedJoystick;

    bool startMoving = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        rb = GetComponent<Rigidbody>();
        fixedJoystick = FindObjectOfType<FixedJoystick>();

        yield return new WaitForSeconds(1f);
        startMoving = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(startMoving)
        {
            float movementHorizontal = fixedJoystick.Direction.x;
            float movementVertical = fixedJoystick.Direction.y;

            Vector3 movement = new Vector3(movementHorizontal, 0, movementVertical);

            if (movement != Vector3.zero)
            {
                rb.isKinematic = false;
                rb.AddForce(speed * movement * Time.deltaTime);
            }

            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.02f, 6))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }
            }
            else
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }    
        
        
    }
}
