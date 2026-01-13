using System;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float speed = 120f;
    public GameObject player;
    public float sensitivityX = 3f;
   
                // Camera offset from the player (y is height, z is distance behind)
                public Vector3 offset = new Vector3(0f, 2f, -5f);
                public float lookAtHeight = 2.25f;

                void Start()
                {
       

        if (player == null)
                    {
                        Debug.LogError("RotateCamera: player is not assigned.");
                        enabled = false;
                        return;
                    }

                    // If the offset hasn't been set in inspector, derive it from current positions
                    if (offset == Vector3.zero)
                        offset = transform.position - player.transform.position;
                }

                // Use LateUpdate so camera follows after player moved this frame
                void LateUpdate()
                {
                    float mouseX = Input.GetAxis("Mouse X") * sensitivityX;

                    // Rotate the offset around the player based on horizontal mouse movement
                    Quaternion camTurn = Quaternion.AngleAxis(mouseX * speed * Time.deltaTime, Vector3.up);
                    offset = camTurn * offset;

                    // Set camera position relative to player using the rotated offset
                    transform.position = player.transform.position + offset;

                    // Ensure camera stays above player by using offset.y (keeps vertical offset stable)
                    transform.position = new Vector3(transform.position.x, player.transform.position.y + offset.y, transform.position.z);

        // Look at the player (optionally adjust vertical aim with lookAtHeight)
        transform.LookAt(player.transform.position + Vector3.up * lookAtHeight);

       
    }

    private GameObject Instantiate(GameObject projectile, object value, Quaternion rotation)
    {
        throw new NotImplementedException();
    }
}
