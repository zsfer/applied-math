using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed = 5.0f;

    public Vector3 Target { get; set; }
    Vector3 _prev;

    void Update()
    {
        if (Target == Vector3.zero) return;

        Vector3 targPosition = new(
            Mathf.MoveTowards(transform.position.x, Target.x, _moveSpeed * Time.deltaTime),
            transform.position.y,
            Mathf.MoveTowards(transform.position.z, Target.z, _moveSpeed * Time.deltaTime)
        );
        _prev = transform.position;
        transform.position = targPosition;

        var disp = Target - targPosition;
        if (disp.magnitude > 0.5f)
        {
            var rot = transform.position - _prev;
            var targ = ((Mathf.Atan2(rot.x, rot.z) * Mathf.Rad2Deg) + 360) % 360;
            Debug.Log($"{transform.eulerAngles.y}, ${targ}");
            transform.rotation = Quaternion.Euler(Vector3.up * Mathf.Lerp(transform.eulerAngles.y, targ, _rotationSpeed * Time.deltaTime));
        }
        else
        {
            Target = Vector3.zero;
        }
    }
}
