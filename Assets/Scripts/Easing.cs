using UnityEngine;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class Easing {
    public enum eases {
        easeLinear,
        easeInQuad, easeOutQuad, easeInOutQuad,
        easeInSine, easeOutSine, easeInOutSine,
        easeInCubic, easeOutCubic, easeInOutCubic,
        easeInExpo, easeOutExpo, easeInOutExpo,
        easeInBounce, easeOutBounce, easeInOutBounce
    }

    public delegate float EasingDelegate(float t);

    public static EasingDelegate GetEase(eases newEase) {
        switch (newEase) {
            case 0:
                return Linear;
            case (eases)1:
                return Quad.In;
            case (eases)2:
                return Quad.Out;
            case (eases)3:
                return Quad.InOut;
            case (eases)4:
                return Sine.In;
            case (eases)5:
                return Sine.Out;
            case (eases)6:
                return Sine.InOut;
            case (eases)7:
                return Cubic.In;
            case (eases)8:
                return Cubic.Out;
            case (eases)9:
                return Cubic.InOut;
            case (eases)10:
                return Expo.In;
            case (eases)11: 
                return Expo.Out;
            case (eases)12:
                return Expo.InOut;
            case (eases)13:
                return Bounce.In;
            case (eases)14:
                return Bounce.Out;
            case (eases)15:
                return Bounce.InOut;
            default:
                return Linear;
        }
    }

    //Class of 16 easings
    public static float Linear(float t) {
        return t;
    }
    public class Quad {
        public static float In(float t) {
            return t * t;
        }
        public static float Out(float t) {
            return t * (2f - t);
        }
        public static float InOut(float t) {
            if ((t *= 2f) < 1f) return 0.5f * t * t;
            return -0.5f * ((t -= 1f) * (t - 2f) - 1f);
        }
    };
    public class Sine {
        public static float In(float t) {
            return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
        }
        public static float Out(float t) {
            return Mathf.Sin(t * Mathf.PI * 0.5f);
        }
        public static float InOut(float t) {
            return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
        }
    };
    public class Cubic {
        public static float In(float t) {
            return t * t * t;
        }
        public static float Out(float t) {
            return 1 - Mathf.Pow(1 - t, 3);
        }
        public static float InOut(float t) {
            if (t < 0.5) {
                return 4 * t * t * t;
            } else {
                return 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
            }
        }
    };
    public class Expo {
        public static float In(float t) {
            if (t == 0) {
                return 0;
            } else {
                return Mathf.Pow(2, 10 * t - 10);
            }
        }
        public static float Out(float t) {
            if (t == 1) {
                return 1;
            } else {
                return 1 - Mathf.Pow(2, -10 * t);
            }
        }
        public static float InOut(float t) {
            if (t == 0) {
                return 0;
            } else {
                if (t == 1) {
                    return 1;
                } else {
                    if (t < 0.5) {
                        return Mathf.Pow(2, 20 * t - 10) / 2;
                    } else {
                        return (2 - Mathf.Pow(2, -20 * t + 10)) / 2;
                    }
                }
            }
        }
    };
    public class Bounce {
        public static float In(float time) {
            return 1 - Out(1 - time);
        }
        public static float Out(float time) {
            float n1 = 7.5625f;
            float d1 = 2.75f;
            if (time < 1 / d1) {
                return n1 * time * time;
            } else if (time < 2 / d1) {
                return n1 * (time -= 1.5f / d1) * time + 0.75f;
            } else if (time < 2.5 / d1) {
                return n1 * (time -= 2.25f / d1) * time + 0.9375f;
            } else {
                return n1 * (time -= 2.625f / d1) * time + 0.984375f;
            }
        }
        public static float InOut(float time) {
            return time < 0.5
                ? (1 - Out(1 - 2 * time)) / 2
                : (1 + Out(2 * time - 1)) / 2;
        }
    };
}