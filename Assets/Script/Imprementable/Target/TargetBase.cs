using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class TargetBase : MonoBehaviour
{
    [SerializeField] protected TargetData td;

    protected Rigidbody2D rb;

    protected bool isLanded = false;

    protected bool isOuted = false;

    protected bool isntTrack = false;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void OnEnable() => EventBus.OnReceiveSmash += OnSmash;
    public virtual void OnDisable() => EventBus.OnReceiveSmash -= OnSmash;

    protected virtual void OnSmash(float inputPower)
    {
        if (!isLanded) return;
        float evaluatedPower = td.powerCurve.Evaluate(inputPower);
        Debug.Log($"x:{inputPower} y:{evaluatedPower}");
        rb.AddForce(16000f * Vector2.right * evaluatedPower, ForceMode2D.Impulse);
    }

    protected virtual void CheckOutOfBounds()
    {
        var hitO = Physics2D.Raycast(transform.position, Vector2.down, 1.5f * td.rayLength, td.outLayer);
        Debug.DrawRay(transform.position, Vector2.down * 1.5f * td.rayLength, Color.blue);

        if (hitO.collider == null || isOuted) return;
        isOuted = true;
        DeactivateAsync().Forget();
    }

    private async UniTaskVoid DeactivateAsync()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(td.deactiveTime));
        EventBus.RequestNextTarget();
        gameObject.SetActive(false); // または Destroy(gameObject);
    }

    public virtual void TrackCenter()
    {
        TrackDisable();
        if (isLanded || isntTrack ) return;
        Vector2 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, 0f, 0.7f * Time.fixedDeltaTime);
        transform.position = pos;
    }

    public virtual void TrackDisable()
    {
        if (rb.linearVelocity.x > 3f)
        {
            isntTrack = true;
        }
    }
}