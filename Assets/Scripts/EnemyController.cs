using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 centralPoint;
    [SerializeField] private Transform ball;
    [SerializeField] private bool menu = false;
    private float dodgeDistance = 0.5f;
    private float dodgeSpeed = 100f;
    private Vector3 targetPosition;
    private Vector3 randomPoint;
    private float distance = 1.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(RandomPoint());

        if (!menu)
        {
            dodgeSpeed = 100 + 75f * (SceneManager.GetActiveScene().buildIndex - 1);
        }
        else
        {
            dodgeSpeed = 350;
        }
    }

    private IEnumerator RandomPoint()
    {
        randomPoint = new Vector3(centralPoint.x + Random.Range(-0.35f, 0.35f), 0, centralPoint.z + Random.Range(-0.35f, 0.35f));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(RandomPoint());
    }

    void Update()
    {
        if (ball != null && Vector3.Distance(ball.position, transform.position) < distance)
        {
            Vector3 directionAwayFromBall = (transform.position - ball.position).normalized;
            //if (directionAwayFromBall.x <= 0)
            //{
            //    directionAwayFromBall.x = -1;
            //}
            //else
            //{
            //    directionAwayFromBall.x = 1;
            //}
            //if (directionAwayFromBall.z <= 0)
            //{
            //    directionAwayFromBall.z = -1;
            //}
            //else
            //{
            //    directionAwayFromBall.z = 1;
            //}
            targetPosition = new Vector3((transform.position.x + directionAwayFromBall.x) * dodgeDistance, 0, (transform.position.z + directionAwayFromBall.z) * dodgeDistance);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (Vector3.Distance(ball.position, transform.position) < distance)
        {
            rb.linearVelocity = (targetPosition - transform.position) * dodgeSpeed * Time.deltaTime;
        }
        else
        {
            //rb.velocity = (randomPoint - transform.position) * 50 * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, randomPoint, 0.75f * Time.deltaTime);
        }
    }
}
