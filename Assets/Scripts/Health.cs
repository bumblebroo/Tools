using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    private bool canBeInvulnerable;

    private bool invulnerable = false;
    private Timer invulnerabilityTimer;

    public UnityEvent onDamaged, onHealed, onExitInvulnerability, onDeath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canBeInvulnerable = TryGetComponent<Timer>(out invulnerabilityTimer);
        if (canBeInvulnerable) {
            invulnerabilityTimer.onTimerFinished.AddListener(ExitInvulnerability);
        }
    }

    private void ExitInvulnerability() {
        onExitInvulnerability?.Invoke();
        invulnerable = false;
    }

    private void OnDestroy() {
        if (canBeInvulnerable) {
            invulnerabilityTimer.onTimerFinished.RemoveListener(ExitInvulnerability);
        }
    }

    public void TakeDamage(float damage) {
        if (invulnerable) {
            return;
        }

        currentHealth -= damage;
        onDamaged?.Invoke();

        HandleDeath();
    }

    public void Heal(float amount) {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        onHealed?.Invoke();
    }

    public void Kill() {
        TakeDamage(maxHealth + 1);
    }

    private void HandleDeath() {
        if(currentHealth > 0) {
            return;
        }

        // Do death animation
        onDeath?.Invoke();
        Destroy(this.gameObject);
    }
}
