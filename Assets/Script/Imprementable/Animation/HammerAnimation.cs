using UnityEngine;
using ResultSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class HammerAnimation : MonoBehaviour
{
    [SerializeField] private LerpParams[] hammerParam;

    [SerializeField] private Transform hammer;

    [SerializeField] private Vector3[] pos;

    [SerializeField] private Quaternion[] rot;

    [SerializeField] private AnimationCurve[] curves;

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
        if (GameManager.I.CurrentState != GameState.Playing) return;
        await UniTask.WhenAll(
            hammerParam[0].RunLerp(value => hammer.position = (Vector3)value),
            hammerParam[1].RunLerp(value => hammer.rotation = (Quaternion)value)
        );
        
        EventBus.ReceiveScore(cachedResult.score);
        EventBus.ReceiveSmash(cachedResult.power);
        AudioManager.I.PlaySE(SE.Name.Hammer);

        await UniTask.WhenAll(
            hammerParam[2].RunLerp(value => hammer.position = (Vector3)value),
            hammerParam[3].RunLerp(value => hammer.rotation = (Quaternion)value)
        );
    }
}
