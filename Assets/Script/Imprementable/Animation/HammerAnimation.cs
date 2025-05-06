using UnityEngine;
using ResultSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class HammerAnimation : MonoBehaviour
{
    [SerializeField] private LerpParams[] hammerParam;

    [SerializeField] private Transform hammer;

    private ImputResult cachedResult;

    private void OnEnable() => EventBus.OnBarStopped += OnSmashEvaluated;

    private void OnDisable() => EventBus.OnBarStopped -= OnSmashEvaluated;

    private void OnSmashEvaluated(ImputResult result)
    {
        cachedResult = result;
        AnimateHammer(result.hammerSpeed).Forget();
    }

    private async UniTask AnimateHammer(float hammerSpeed)
    {
        await UniTask.Yield();
        await UniTask.WhenAll(
            //DOTweenHelper.LerpAsync(hammerStartPos, hammerEndPos, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.position = value),
            //DOTweenHelper.LerpAsync(hammerStartRot, hammerEndRot, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.rotation = value)
        );
        await UniTask.WhenAll(
            //DOTweenHelper.LerpAsync(hammerStartPos, hammerEndPos, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.position = value),
            //DOTweenHelper.LerpAsync(hammerStartRot, hammerEndRot, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.rotation = value)
        );

        EventBus.ReceiveScore(cachedResult.score);
        EventBus.ReceiveSmash(cachedResult.power);

        await UniTask.WhenAll(
            //DOTweenHelper.LerpAsync(hammerStartPos, hammerEndPos, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.position = value),
            //DOTweenHelper.LerpAsync(hammerStartRot, hammerEndRot, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.rotation = value)
        );
        await UniTask.WhenAll(
            //DOTweenHelper.LerpAsync(hammerStartPos, hammerEndPos, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.position = value),
            //DOTweenHelper.LerpAsync(hammerStartRot, hammerEndRot, 1f/hammerSpeed, Ease.InOutQuad, (value) => this.transform.rotation = value)
        );
    }
}
