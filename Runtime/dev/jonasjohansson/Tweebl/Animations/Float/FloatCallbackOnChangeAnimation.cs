using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace dev.jonasjohansson.Tweebl
{
    [CreateAssetMenu(fileName = "FloatCallbackOnChangeAnimation", menuName = "JJ/Animations/FloatCallbackOnChangeAnimation", order = 0)]
    public class FloatCallbackOnChangeAnimation : TweenblFloatAnimation
    {
        protected override IEnumerator Animate(float p_value, float p_step, Guid p_guid, Action<float, float> p_onChange, Action p_callback = null)
        {          
            float modifiedChange = Change-Value;
            float time = p_step > 0.01f ? 1f-p_step : 0f;
            time = math.lerp(0, Duration, time);
            float step;
            while (time <= Duration)
            {
                step = Tweebl.InvLerp(0, Duration, time);
                p_onChange(step,Tweebl.Easeings[(int)Ease].Invoke(time, Value, modifiedChange, Duration));
                time += Time.deltaTime;
                yield return null;
            }
            p_onChange(0, Change);
            OnComplete(p_guid, p_callback);   
        }
    }
}