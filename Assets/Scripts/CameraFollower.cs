using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    //скрипт вешается на камеру и следует за игроком
    [SerializeField] Transform followTarget;
    private Vector3 offset;


    private void Start() {
        offset = transform.position - followTarget.position;
    }
    private void Update() {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, followTarget.position.z + offset.z);
        transform.position = newPosition;
    }
}
