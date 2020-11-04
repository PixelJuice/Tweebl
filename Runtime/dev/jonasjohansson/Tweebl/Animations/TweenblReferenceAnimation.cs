using System;
using System.Collections;
using UnityEngine;

namespace dev.jonasjohansson.Tweebl
{
    public abstract class TweenblReferenceAnimation<T> : ScriptableObject
    {
        protected abstract IEnumerator Animate(T p_target, Guid p_guid, Action p_callback = null);

        public virtual Guid Play(T p_target, Action p_callback = null)
        {
            var guid = Guid.NewGuid();
            return Tweebl.Get.PlayAnimation(()=>Animate(p_target, guid, p_callback), guid);     
        }

        public virtual void Stop(Guid p_animationID) {
            Tweebl.Get.StopAnimation(p_animationID);
        }

        protected void OnComplete(Guid p_guid, Action p_callback = null)
        {
            Tweebl.Get.StopAnimation(p_guid);
            p_callback?.Invoke();
        }
    }
}

