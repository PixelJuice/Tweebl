using System;
using System.Collections;
using UnityEngine;

namespace dev.jonasjohansson.Tweebl
{
    public abstract class TweenblCallbackAnimation<T> : ScriptableObject
    {
        public float Duration = 1f;
        public float Value = 0;
        public float Change = 100;
        public Tweebl.Easeing Ease;
        protected abstract IEnumerator Animate(float p_value, float p_time, Guid p_guid, Action<float,T> p_onChange, Action p_callback = null);

        public virtual Guid Play(Action<float,T> p_onChange, Action p_callback = null)
        {
            return Play(Value, 0, p_onChange, p_callback);
        }
        public virtual Guid Play(float p_value, Action<float,T> p_onChange, Action p_callback = null)
        {
            return Play(p_value, 0, p_onChange, p_callback);
        }
        public virtual Guid Play(float p_value, float p_time, Action<float,T> p_onChange, Action p_callback = null)
        {
            var guid = Guid.NewGuid();
            return Tweebl.Get.PlayAnimation(() => Animate(p_value, p_time, guid, p_onChange, p_callback), guid);
        }

        public virtual void Stop(Guid p_animationID)
        {
            Tweebl.Get.StopAnimation(p_animationID);
        }

        protected void OnComplete(Guid p_guid, Action p_callback = null)
        {
            Tweebl.Get.StopAnimation(p_guid);
            p_callback?.Invoke();
        }
    }
}

