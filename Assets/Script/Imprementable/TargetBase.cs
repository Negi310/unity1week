using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class TargetBase : MonoBehaviour
{
    [SerializeField] protected TargetData td;

    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SetupVisual();
    }

    protected virtual void OnEnable() => EventBus.I.OnReceiveSmash += OnSmash;
    protected virtual void OnDisable() => EventBus.I.OnReceiveSmash -= OnSmash;

    protected virtual void OnSmash(float inputPower, float score) => rb.AddForce(Vector2.right * td.powerRatio * inputPower, ForceMode2D.Impulse);

    protected virtual void CheckOutOfBounds()
    {
        var hitO = Physics2D.Raycast(transform.position, Vector2.down, td.rayLength, td.outLayer);
        Debug.DrawRay(transform.position, Vector2.down * td.rayLength, Color.blue);

        if (hitO.collider != null) return;
        DeactivateAsync().Forget();
    }

    protected virtual void SetupVisual()
    {
        //var im = GetComponent<Image>();
        //if (im != null) return;
        //im.color = td.color;
    }

    private async UniTaskVoid DeactivateAsync()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(td.deactiveTime));
        EventBus.I.RequestNextTarget();
        gameObject.SetActive(false); // または Destroy(gameObject);
    }
}