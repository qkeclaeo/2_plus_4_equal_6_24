using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Transform _target;
    Vector3 originPos;
    float _offsetX;

    private void Start()
    {
        originPos = transform.position;
    }

    public void Init(Transform target)
    {
        //target = FindAnyObjectByType<Player>().transform;
        if (target == null)
            return;

        _target = target;
        transform.position = originPos;
        _offsetX = transform.position.x - _target.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
            return;

        Vector3 pos = transform.position;
        pos.x = _target.position.x + _offsetX;
        transform.position = pos;
    }
}
