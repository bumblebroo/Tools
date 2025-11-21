using UnityEngine;
using System.Collections;

public class ContinousMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 dir = Vector3.zero;

    [SerializeField]
    private float speed;

    private bool moving;

    public Vector3 Dir {
        get {
            return dir;
        }
        set {
            dir = value;
            if(dir.magnitude > 0 && !moving) {
                StartCoroutine(Move());
            }
        }
    }

    public Vector3 Velocity {
        get {
            return dir * speed;
        }
    }

    private void Start() {
        if(dir.magnitude > 0) {
            return;
        }

        dir = transform.right;
    }

    private IEnumerator Move() {
        moving = true;
        while (Dir.magnitude > 0) {
            transform.position += Dir * speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        moving = false;
    }
}
