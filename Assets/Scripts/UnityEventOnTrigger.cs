using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class UnityEventOnTrigger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("If there are any specific tags that you want to interact with this")]
    private string[] tags;

    [SerializeField]
    [Tooltip("If there are any specific gameObjects that you want to interact with this")]
    private GameObject[] gameObjects;

    [Space]

    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    private HashSet<GameObject> gameObjectsInTrigger = new HashSet<GameObject>();

    #region Trigger calls
    private void OnTriggerEnter2D(Collider2D collision) {
        HandleEnter(collision.gameObject);
    }

    private void OnTriggerEnter(Collider collision) {
        HandleEnter(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        HandleExit(collision.gameObject);
    }

    private void OnTriggerExit(Collider collision) {
        HandleExit(collision.gameObject);
    }
    #endregion

    private void HandleEnter(GameObject collisionGameObject) {
        // Collider's gameObject is already inside the trigger
        // -> return
        if (gameObjectsInTrigger.Contains(collisionGameObject)) {
            return;
        }

        // If the collisions tags don't match any of the tags listed and there are tags to compare to
        // -> return
        if (tags.Length > 0 && !tags.Contains(collisionGameObject.tag)) {
            return;
        }

        // If the collisions gameObject don't match any of the tags listed and there are tags to compare to
        // -> return
        if (gameObjects.Length > 0 && !gameObjects.Contains(collisionGameObject)) {
            return;
        }

        // Triggers the onTriggerEnter event and adds the gameObject to the HashSet to keep track
        onTriggerEnter?.Invoke();
        gameObjectsInTrigger.Add(collisionGameObject);

        // Debug.log($"{collisionGameObject.name} entered {name}'s trigger);
    }

    private void HandleExit(GameObject collisionGameObject) {
        // Collider's gameObject isn't inside the trigger
        // -> return
        if (!gameObjectsInTrigger.Contains(collisionGameObject)) {
            return;
        }

        // Triggers the onTriggerExit event and removes the gameObject to the HashSet to keep track
        onTriggerExit?.Invoke();
        gameObjectsInTrigger.Remove(collisionGameObject);

        // Debug.log($"{collisionGameObject.name} exited {name}'s trigger);
    }
}