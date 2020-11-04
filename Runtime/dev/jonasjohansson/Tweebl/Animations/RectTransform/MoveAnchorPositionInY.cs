using System;
using System.Collections;
using UnityEngine;

namespace dev.jonasjohansson.Tweebl
{
    [CreateAssetMenu(fileName = "MoveAnchorPositionInY", menuName = "JJ/Animations/MoveAnchorPositionInY", order = 0)]
    public class MoveAnchorPositionInY : TweenblRectTransformAnimation
    {
        public float Duration = 1f;
        public float StartY = 0;
        public float TargetY = 100;
        public Tweebl.Easeing Easeing;

        protected override IEnumerator Animate(RectTransform p_target, Guid p_guid, Action p_callback = null) 
        {
            float time = 0f;
            var anch = p_target.anchoredPosition;
            while (time <= Duration)
            {
                anch.y = Tweebl.Easeings[(int)Easeing].Invoke(time, StartY, TargetY, Duration);
                p_target.anchoredPosition = anch;
                time += Time.deltaTime;
                yield return null;
            }
            anch.y = StartY + TargetY;
            p_target.anchoredPosition = anch;
            OnComplete(p_guid, p_callback);         
        }
    }
}