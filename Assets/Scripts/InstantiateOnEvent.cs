using UnityEngine;

public class InstantiateOnEvent : MonoBehaviour
{
    [SerializeField]
    [Tooltip("GameObject to instantiate")]
    private GameObject prefab;

    [Space]

    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    [Tooltip("If the instance should be a child of the target Transform")]
    private bool asChild;

    [SerializeField]
    [Tooltip("Will destroy the instance after the delay, if the delay is zero or less, it won't be destroyed")]
    private float destroyAfter = 0;

    private void Start() {
        if(targetTransform) return;
        targetTransform = transform;
    }
    public void CreateInstance() {
        GameObject instance;
        if (asChild) {
            instance = Instantiate(prefab, targetTransform);
        } else {
            instance = Instantiate(prefab, targetTransform.position, targetTransform.rotation);
        }

        if(destroyAfter <= 0) {
            return;
        }

        Destroy(instance, destroyAfter);
    }
}
