using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody rb;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Move();
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    private void Move()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, rayLength, floorLayer))
        {
            rb.linearVelocity = (hit.point - player.position) * 1000 * Time.deltaTime;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}
