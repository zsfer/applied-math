using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{
    [SerializeField] Transform _targetObj;
    [SerializeField] TargetFollower _follower;
    [SerializeField] LayerMask _layerMask;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000, _layerMask))
        {
            _targetObj.position = hit.point;

            if (Input.GetMouseButtonDown(1))
                _follower.Target = hit.point;
        }
    }
}
