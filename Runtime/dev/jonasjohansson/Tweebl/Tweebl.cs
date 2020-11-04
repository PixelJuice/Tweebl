using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace dev.jonasjohansson.Tweebl
{
    public class Tweebl : MonoBehaviour
    {
        public enum Easeing
        {
          	Linear,
			InSine,
			OutSine,
			InOutSine,
			InQuint,
			OutQuint,
			InOutQuint,
			InQuart,
			OutQuart,
			InOutQuart,
			InQuad,
			OutQuad,
			InOutQuad,
			InExpo,
			OutExpo,
			InOutExpo,
			InCircular,
			OutCircular,
			InOutCircular,
			InElastic,
			OutElastic,
			InOutElastic,
			InBack,
			OutBack,
			InOutBack,
			InBounce,
			OutBounce,
			InOutBounce,
			InCubic,
			OutCubic,
			InOutCubic
        }
        public static Func<float, float, float, float, float>[] Easeings = {
            Linear,
			InSine,
			OutSine,
			InOutSine,
			InQuint,
			OutQuint,
			InOutQuint,
			InQuart,
			OutQuart,
			InOutQuart,
			InQuad,
			OutQuad,
			InOutQuad,
			InExpo,
			OutExpo,
			InOutExpo,
			InCircular,
			OutCircular,
			InOutCircular,
			InElastic,
			OutElastic,
			InOutElastic,
			InBack,
			OutBack,
			InOutBack,
			InBounce,
			OutBounce,
			InOutBounce,
			InCubic,
			OutCubic,
			InOutCubic

        };
        private static readonly float PI_M2 = math.PI * 2;
        private static readonly float PI_D2 = math.PI / 2;
        private static readonly Dictionary<Guid, Coroutine> m_animations = new Dictionary<Guid, Coroutine>();
        private static Tweebl s_instance;
        public static Tweebl Get
        {
            get
            {
                if (s_instance == null)
                    s_instance = new GameObject("~Tweebl").AddComponent<Tweebl>();
                return s_instance;
            }
        }
        void Awake()
        {
            this.gameObject.isStatic = true;
#if !UNITY_EDITOR
                this.gameObject.hideFlags = HideFlags.HideAndDontSave;
#endif
#if UNITY_EDITOR
            if (Application.isPlaying)
                DontDestroyOnLoad(this.gameObject);
#else
                DontDestroyOnLoad( this.gameObject );
#endif
        }

        public Guid PlayAnimation(Func<IEnumerator> p_coroutine)
        {
            var guid = Guid.NewGuid();
            return PlayAnimation(p_coroutine, guid);
        }

        public Guid PlayAnimation(Func<IEnumerator> p_coroutine, Guid p_guid)
        {
            StopAnimation(p_guid);
            m_animations.Add(p_guid, StartCoroutine(p_coroutine()));
            return p_guid;
        }

        public void StopAllAnimations()
        {
            StopAllCoroutines();
            m_animations.Clear();
        }

        public void StopAnimation(Guid p_guid)
        {
            if (m_animations.ContainsKey(p_guid))
            {
                StopCoroutine(m_animations[p_guid]);
                m_animations.Remove(p_guid);
            }
        }

		public static float InvLerp(float a, float b, float v) {
            return (v-a) / (b-a);
        }

        #region Easeing
        /*
		Linear
		---------------------------------------------------------------------------------
		*/
        public static float Linear(float t, float b, float c, float d)
        {
            return c * t / d + b;
        }

        /*
		Sine
		---------------------------------------------------------------------------------
		*/
        public static float InSine(float t, float b, float c, float d)
        {
            return -c * math.cos(t / d * PI_D2) + c + b;
        }
        public static float OutSine(float t, float b, float c, float d)
        {
            return c * math.sin(t / d * PI_D2) + b;
        }
        public static float InOutSine(float t, float b, float c, float d)
        {
            return -c / 2 * (math.cos(math.PI * t / d) - 1) + b;
        }

        /*
		Quintic
		---------------------------------------------------------------------------------
		*/
        public static float InQuint(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t * t * t + b;
        }
        public static float OutQuint(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
        }
        public static float InOutQuint(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
        }

        /*
		Quartic
		---------------------------------------------------------------------------------
		*/
        public static float InQuart(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t * t + b;
        }
        public static float OutQuart(float t, float b, float c, float d)
        {
            return -c * ((t = t / d - 1) * t * t * t - 1) + b;
        }
        public static float InOutQuart(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t * t + b;
            return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
        }

        /*
		Quadratic
		---------------------------------------------------------------------------------
		*/
        public static float InQuad(float t, float b, float c, float d)
        {
            return c * (t /= d) * t + b;
        }
        public static float OutQuad(float t, float b, float c, float d)
        {
            return -c * (t /= d) * (t - 2) + b;
        }
        public static float InOutQuad(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t + b;
            return -c / 2 * ((--t) * (t - 2) - 1) + b;
        }

        /*
		Exponential
		---------------------------------------------------------------------------------
		*/
        public static float InExpo(float t, float b, float c, float d)
        {
            return (t == 0) ? b : c * math.pow(2, 10 * (t / d - 1)) + b;
        }
        public static float OutExpo(float t, float b, float c, float d)
        {
            return (t == d) ? b + c : c * (-math.pow(2, -10 * t / d) + 1) + b;
        }
        public static float InOutExpo(float t, float b, float c, float d)
        {
            if (t == 0) return b;
            if (t == d) return b + c;
            if ((t /= d / 2) < 1) return c / 2 * math.pow(2, 10 * (t - 1)) + b;
            return c / 2 * (-math.pow(2, -10 * --t) + 2) + b;
        }

        /*
		Elastic
		---------------------------------------------------------------------------------
		*/
		public static float InElastic(float t, float b, float c, float d)
        {
            return InElastic(t,b,c,d,-1f,-1f);
        }
        public static float InElastic(float t, float b, float c, float d, float a = -1, float p = -1)
        {
            float s;
            if (t == 0)
            {
                return b;
            }
            if ((t /= d) == 1)
            {
                return b + c;
            }
            if (p == -1)
            {
                p = d * .3f;
            }
            if (a == -1 || a < math.abs(c))
            {
                a = c;
                s = p / 4;
            }
            else
            {
                s = p / PI_M2 * math.asin(c / a);
            }
            return -(a * math.pow(2, 10 * (t -= 1)) * math.sin((t * d - s) * PI_M2 / p)) + b;
        }
        public static float OutElastic(float t, float b, float c, float d)
        {
			return OutElastic(t,b,c,d,-1f,-1f);
		}
		public static float OutElastic(float t, float b, float c, float d, float a = -1, float p = -1)
        {
            float s;
            if (t == 0)
            {
                return b;
            }
            if ((t /= d) == 1)
            {
                return b + c;
            }
            if (p == -1)
            {
                p = d * .3f;
            }
            if (a == -1 || a < math.abs(c))
            {
                a = c; s = p / 4;
            }
            else
            {
                s = p / PI_M2 * math.asin(c / a);
            }
            return (a * math.pow(2, -10 * t) * math.sin((t * d - s) * PI_M2 / p) + c + b);
        }
		public static float InOutElastic(float t, float b, float c, float d)
        {
			return InOutElastic(t,b,c,d,-1f,-1f);
		}
        public static float InOutElastic(float t, float b, float c, float d, float a = -1, float p = -1)
        {
            float s;
            if (t == 0) 
			{
				return b;
			}
            if ((t /= d / 2) == 2)
			{
				return b + c;
			}
            if (p == -1)
			{ 
				p = d * (.3f * 1.5f);
			}
            if (a == -1 || a < math.abs(c))
			{ 
				a = c; s = p / 4;
			}
            else 
			{ 
				s = p / PI_M2 * math.asin(c / a);
			}
            if (t < 1) 
			{ 
				return -.5f * (a * math.pow(2, 10 * (t -= 1)) * math.sin((t * d - s) * PI_M2 / p)) + b;
			}
            return a * math.pow(2, -10 * (t -= 1)) * math.sin((t * d - s) * PI_M2 / p) * .5f + c + b;
        }

        /*
		Circular
		---------------------------------------------------------------------------------
		*/
        public static float InCircular(float t, float b, float c, float d)
        {
            return -c * (math.sqrt(1 - (t /= d) * t) - 1) + b;
        }
        public static float OutCircular(float t, float b, float c, float d)
        {
            return c * math.sqrt(1 - (t = t / d - 1) * t) + b;
        }
        public static float InOutCircular(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1) return -c / 2 * (math.sqrt(1 - t * t) - 1) + b;
            return c / 2 * (math.sqrt(1 - (t -= 2) * t) + 1) + b;
        }

        /*
		Back
		---------------------------------------------------------------------------------
		*/
		public static float InBack(float t, float b, float c, float d)
        {
			return InBack(t,b,c,d,1.70158f);
		}
        public static float InBack(float t, float b, float c, float d, float s = 1.70158f)
        {
            return c * (t /= d) * t * ((s + 1) * t - s) + b;
        }
		public static float OutBack(float t, float b, float c, float d)
        {
			return OutBack(t,b,c,d,1.70158f);
		}
        public static float OutBack(float t, float b, float c, float d, float s = 1.70158f)
        {
            return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
        }
		public static float InOutBack(float t, float b, float c, float d) {
			return InOutBack(t,b,c,d,1.70158f);
		}
        public static float InOutBack(float t, float b, float c, float d, float s = 1.70158f)
        {
            if ((t /= d / 2) < 1) 
			{
				return c / 2 * (t * t * (((s *= (1.525f)) + 1) * t - s)) + b;
			}
            return c / 2 * ((t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2) + b;
        }

        /*
		Bounce
		---------------------------------------------------------------------------------
		*/
        public static float InBounce(float t, float b, float c, float d)
        {
            return c - OutBounce(d - t, 0, c, d) + b;
        }
        public static float OutBounce(float t, float b, float c, float d)
        {
            if ((t /= d) < (1 / 2.75f))
            {
                return c * (7.5625f * t * t) + b;
            }
            else if (t < (2 / 2.75f))
            {
                return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
            }
            else if (t < (2.5f / 2.75f))
            {
                return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
            }
            else
            {
                return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
            }
        }
        public static float InOutBounce(float t, float b, float c, float d)
        {
            if (t < d / 2) return InBounce(t * 2, 0, c, d) * .5f + b;
            else return OutBounce(t * 2 - d, 0, c, d) * .5f + c * .5f + b;
        }

        /*
		Cubic
		---------------------------------------------------------------------------------
		*/
        public static float InCubic(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t + b;
        }
        public static float OutCubic(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1) * t * t + 1) + b;
        }
        public static float InOutCubic(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t + 2) + b;
        }
        #endregion
    }
}