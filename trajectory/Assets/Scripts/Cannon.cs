using UnityEngine;
using UnityEngine.UIElements;

public class Cannon : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] Rigidbody _ball;
    [SerializeField, Range(0.0f, 100.0f)] float _shootForce;
    [SerializeField] float _rotationSpeed;

    [SerializeField] Transform _shootPosition;
    [SerializeField] Transform _barrel;

    Vector3 _rotation;

    [Header("Simulation Settings")]
    [SerializeField] int _maxSteps = 50;
    [SerializeField] LineRenderer _lineRenderer;

    readonly float _increment = 0.025f; // physics step increment
    readonly float _overlap = 1.2f; // ray overlap to make sure we dont miss any surfaces :D
    [SerializeField] LayerMask _mask; // mask for raycast

    void Update()
    {
        Predict();
        HandleInput();
    }

    void Predict()
    {
        var vel = _shootForce / _ball.mass * _shootPosition.forward;
        var pos = _shootPosition.position;
        Vector3 next;
        float overlap;

        UpdateLineRenderer(_maxSteps, 0, pos);

        for (int i = 0; i < _maxSteps; i++)
        {
            vel = CalculateVelocity(vel, _ball.drag, _increment);
            next = pos + vel * _increment;

            overlap = Vector3.Distance(pos, next) * _overlap;

            if (Physics.Raycast(pos, vel.normalized, out RaycastHit hit, overlap, _mask))
            {
                UpdateLineRenderer(i, i - 1, hit.point);
                break;
            }

            pos = next;
            UpdateLineRenderer(_maxSteps, i, pos);
        }
    }

    void UpdateLineRenderer(int count, int point, Vector3 pos)
    {
        _lineRenderer.positionCount = count;
        _lineRenderer.SetPosition(point, pos);
    }

    Vector3 CalculateVelocity(Vector3 velocity, float drag, float increment)
    {
        velocity += Physics.gravity * increment;
        velocity *= Mathf.Clamp01(1f - drag * increment);
        return velocity;
    }

    void HandleInput()
    {
        _rotation += _rotationSpeed * Time.deltaTime * (transform.up * Input.GetAxisRaw("Horizontal") + transform.right * Input.GetAxisRaw("Vertical")).normalized;
        _barrel.rotation = Quaternion.Euler(_rotation);

        if (Input.GetKey(KeyCode.J))
            _shootForce += Time.deltaTime * 10;
        else if (Input.GetKey(KeyCode.K))
            _shootForce -= Time.deltaTime * 10;

        // ! SHOOTING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody ball = Instantiate(_ball, _shootPosition.position, Quaternion.identity);
            ball.AddForce(_shootPosition.forward * _shootForce, ForceMode.Impulse);
        }
    }
}
