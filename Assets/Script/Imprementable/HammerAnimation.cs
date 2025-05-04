using UnityEngine;
using ResultSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class HammerAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 hammerStartPos;
    [SerializeField] private Vector3 hammerEndPos;
    [SerializeField] private Quaternion hammerStartRot;
    [SerializeField] private Quaternion hammerEndRot;
    private ImputResult cachedResult;

    private void OnEnable() => EventBus.OnBarStopped += OnSmashEvaluated;

    private void OnDisable() => EventBus.OnBarStopped -= OnSmashEvaluated;

    private void OnSmashEvaluated(ImputResult result)
    {
        cachedResult = result;
        AnimateHammer(result.hammerSpeed);
    }

    private async void AnimateHammer(float hammerSpeed)
    {
        
        await UniTask.WhenAll(
            DOTweenHelper.LerpAsync(hammerStartPos, hammerEndPos, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.position = value),
            DOTweenHelper.LerpAsync(hammerStartRot, hammerEndRot, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.rotation = value)
        );
        await UniTask.WhenAll(
            DOTweenHelper.LerpAsync(hammerStartPos, hammerEndPos, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.position = value),
            DOTweenHelper.LerpAsync(hammerStartRot, hammerEndRot, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.rotation = value)
        );

        EventBus.ReceiveScore(cachedResult.score);
        EventBus.ReceiveSmash(cachedResult.power);
        Debug.Log("Receive");

        await UniTask.WhenAll(
            DOTweenHelper.LerpAsync(hammerStartPos, hammerEndPos, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.position = value),
            DOTweenHelper.LerpAsync(hammerStartRot, hammerEndRot, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.rotation = value)
        );
        await UniTask.WhenAll(
            DOTweenHelper.LerpAsync(hammerStartPos, hammerEndPos, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.position = value),
            DOTweenHelper.LerpAsync(hammerStartRot, hammerEndRot, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.rotation = value)
        );
    }
}
