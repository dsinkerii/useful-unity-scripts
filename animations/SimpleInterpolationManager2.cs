using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;
public class SimpleInterpolationManager2 : MonoBehaviour
{
    // re-written by dsinkerii ❤️‍🔥
    // SerializedDictionary by ayellowpaper
    // everything easing related - https://easings.net/

    // logic is the same as the first one, however im making it easier by utilizing lambda functions
    public enum InterpolationType{
        Linear = 0,
        EaseInSine,
        EaseOutSine,
        EaseInOutSine,
        
        EaseInCubic,
        EaseOutCubic,
        EaseInOutCubic,
        
        EaseInQuint,
        EaseOutQuint,
        EaseInOutQuint,
        
        EaseInCirc,
        EaseOutCirc,
        EaseInOutCirc,
        
        EaseInQuad,
        EaseOutQuad,
        EaseInOutQuad,
        
        EaseInQuart,
        EaseOutQuart,
        EaseInOutQuart,
        
        EaseInExpo,
        EaseOutExpo,
        EaseInOutExpo,
        
        EaseInBack,
        EaseOutBack,
        EaseInOutBack,

        EaseInBounce,
        EaseOutBounce,
        EaseInOutBounce,

        EaseInElastic,
        EaseOutElastic,
        EaseInOutElastic
    }
    public static class EasingFunctions{ // https://easings.net/
        public static float Linear(float from, float to, float x) 
            {return from + (to - from) * x;}
        public static float EaseInSine(float from, float to, float x) 
            {return Linear(from, to, 1 - Mathf.Cos(x * Mathf.PI / 2));}
        public static float EaseOutSine(float from, float to, float x) 
            {return Linear(from, to, (float)Math.Sin(x * Math.PI / 2));}
        public static float EaseInOutSine(float from, float to, float x) 
            {return Linear(from, to, -(Mathf.Cos((float)Math.PI * x) - 1f) / 2f);}
        
        public static float EaseInCubic(float from, float to, float x) 
            {return Linear(from, to, x*x*x);}
        public static float EaseOutCubic(float from, float to, float x) 
            {return Linear(from, to, 1 - Mathf.Pow(1 - x, 3));}
        public static float EaseInOutCubic(float from, float to, float x) 
            {return Linear(from, to, x < 0.5f ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2);}

        public static float EaseInQuint(float from, float to, float x) 
            {return Linear(from, to, x * x * x * x * x);}
        public static float EaseOutQuint(float from, float to, float x) 
            {return Linear(from, to, 1 - Mathf.Pow(1 - x, 5));}
        public static float EaseInOutQuint(float from, float to, float x) 
            {return Linear(from, to, x < 0.5f ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2);}
        
        public static float EaseInCirc(float from, float to, float x) 
            {return Linear(from, to,  1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2)));}
        public static float EaseOutCirc(float from, float to, float x) 
            {return Linear(from, to, Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2)));}
        public static float EaseInOutCirc(float from, float to, float x) 
            {return Linear(from, to, x < 0.5f ? (1 - Mathf.Sqrt(1 - (float)Math.Pow(2 * x, 2))) / 2 : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2);}
        
        public static float EaseInQuad(float from, float to, float x) 
            {return Linear(from, to, x * x);}
        public static float EaseOutQuad(float from, float to, float x) 
            {return Linear(from, to, 1 - (1 - x) * (1 - x));}
        public static float EaseInOutQuad(float from, float to, float x) 
            {return Linear(from, to, x < 0.5f ? 2f * x * x : 1 - (float)Math.Pow(-2 * x + 2, 2) / 2);}
        
        public static float EaseInQuart(float from, float to, float x) 
            {return Linear(from, to, x * x * x * x);}
        public static float EaseOutQuart(float from, float to, float x) 
            {return Linear(from, to, 1 - Mathf.Pow(1 - x, 4));}
        public static float EaseInOutQuart(float from, float to, float x) 
            {return Linear(from, to, x < 0.5f ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2);}
        
        public static float EaseInExpo(float from, float to, float x) 
            {return Linear(from, to, x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10));}
        public static float EaseOutExpo(float from, float to, float x) 
            {return Linear(from, to, x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x));}
        public static float EaseInOutExpo(float from, float to, float x)
            {return Linear(from, to, x == 0 ? 0 : x == 1 ? 1 : x < 0.5f ? Mathf.Pow(2, 20 * x - 10) / 2 : (2 - Mathf.Pow(2, -20 * x + 10)) / 2);}

        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;
        const float c3 = c1 + 1;
        const float c4 = (2 * Mathf.PI) / 3;
        const float c5 = (2 * Mathf.PI) / 4.5f;

        public static float EaseInBack(float from, float to, float x) 
            {return Linear(from, to, c3 * x * x * x - c1 * x * x);}
        public static float EaseOutBack(float from, float to, float x) 
            {return Linear(from, to, 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2));}
        public static float EaseInOutBack(float from, float to, float x) // good LORD.....
            {return Linear(from, to, x < 0.5 ? (Mathf.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2 : (Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2);}

        public static float EaseInElastic(float from, float to, float x) 
            {return Linear(from, to, x == 0 ? 0 : x == 1 ? 1 : -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((float)(x * 10 - 10.75) * c4));}
        public static float EaseOutElastic(float from, float to, float x) 
            {return Linear(from, to, x == 0 ? 0 : x == 1 ? 1 : Mathf.Pow(2, -10 * x) * Mathf.Sin((float)(x * 10 - 0.75) * c4) + 1);}
        public static float EaseInOutElastic(float from, float to, float x)
            {return Linear(from, to, x == 0 ? 0 : x == 1 ? 1 : x < 0.5f ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((float)(20 * x - 11.125f) * c5)) / 2 : (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2 + 1);}
        
        const float n1 = 7.5625f;
        const float d1 = 2.75f;

        public static float EaseOutBounce(float from, float to, float x) {
            float val = 0;
            if (x < 1 / d1) {
                val = n1 * x * x;
            } else if (x < 2 / d1) {
                val = n1 * (x -= 1.5f / d1) * x + 0.75f;
            } else if (x < 2.5f / d1) {
                val = n1 * (x -= 2.25f / d1) * x + 0.9375f;
            } else {
                val = n1 * (x -= 2.625f / d1) * x + 0.984375f;
            }
            return Linear(from, to, val);
        }
        public static float EaseInBounce(float from, float to, float x) 
            {return Linear(from, to, 1 - EaseOutBounce(0, 1, 1 - x));}
        public static float EaseInOutBounce(float from, float to, float x)
            {return Linear(from, to, x < 0.5f ? (1 - EaseOutBounce(0, 1, 1 - 2 * x)) / 2 : (1 + EaseOutBounce(0, 1, 2 * x - 1)) / 2);}
        public static Func<float, float, float, float> GetAction(InterpolationType InterType){
            switch(InterType){
                case InterpolationType.EaseInSine:
                    return EasingFunctions.EaseInSine;
                case InterpolationType.EaseOutSine:
                    return EasingFunctions.EaseOutSine;
                case InterpolationType.EaseInOutSine:
                    return EasingFunctions.EaseInOutSine;
                case InterpolationType.EaseInCubic:
                    return EasingFunctions.EaseInCubic;
                case InterpolationType.EaseOutCubic:
                    return EasingFunctions.EaseOutCubic;
                case InterpolationType.EaseInOutCubic:
                    return EasingFunctions.EaseInOutCubic;
                case InterpolationType.EaseInQuint:
                    return EasingFunctions.EaseInQuint;
                case InterpolationType.EaseOutQuint:
                    return EasingFunctions.EaseOutQuint;
                case InterpolationType.EaseInOutQuint:
                    return EasingFunctions.EaseInOutQuint;
                case InterpolationType.EaseInCirc:
                    return EasingFunctions.EaseInCirc;
                case InterpolationType.EaseOutCirc:
                    return EasingFunctions.EaseOutCirc;
                case InterpolationType.EaseInOutCirc:
                    return EasingFunctions.EaseInOutCirc;
                case InterpolationType.EaseInQuad:
                    return EasingFunctions.EaseInQuad;
                case InterpolationType.EaseOutQuad:
                    return EasingFunctions.EaseOutQuad;
                case InterpolationType.EaseInOutQuad:
                    return EasingFunctions.EaseInOutQuad;
                case InterpolationType.EaseInQuart:
                    return EasingFunctions.EaseInQuart;
                case InterpolationType.EaseOutQuart:
                    return EasingFunctions.EaseOutQuart;
                case InterpolationType.EaseInOutQuart:
                    return EasingFunctions.EaseInOutQuart;
                case InterpolationType.EaseInExpo:
                    return EasingFunctions.EaseInExpo;
                case InterpolationType.EaseOutExpo:
                    return EasingFunctions.EaseOutExpo;
                case InterpolationType.EaseInOutExpo:
                    return EasingFunctions.EaseInOutExpo;
                case InterpolationType.EaseInBack:
                    return EasingFunctions.EaseInBack;
                case InterpolationType.EaseOutBack:
                    return EasingFunctions.EaseOutBack;
                case InterpolationType.EaseInOutBack:
                    return EasingFunctions.EaseInOutBack;
                case InterpolationType.EaseInElastic:
                    return EasingFunctions.EaseInElastic;
                case InterpolationType.EaseOutElastic:
                    return EasingFunctions.EaseOutElastic;
                case InterpolationType.EaseInOutElastic:
                    return EasingFunctions.EaseInOutElastic;
                case InterpolationType.EaseInBounce:
                    return EasingFunctions.EaseInBounce;
                case InterpolationType.EaseOutBounce:
                    return EasingFunctions.EaseOutBounce;
                case InterpolationType.EaseInOutBounce:
                    return EasingFunctions.EaseInOutBounce;
                default:
                    return EasingFunctions.Linear;
            }
        }
    }
    [Serializable]
    public class Event{
        public InterpolationType InterType;
        public float from;
        public float to; 
        public float value;
        public Coroutine cor;
        public bool IsFinished;
        public float Duration;
    }
    public enum RuntimeSpace{
        GameTime,
        RealTime,
        AudioTime,
    }
    public class BadAnimValue : Exception{
        public BadAnimValue (){}
        public BadAnimValue (string message) : base(message){}
        public BadAnimValue (string message, Exception innerException): base (message, innerException){}    
    }
    public static SimpleInterpolationManager2 Instance {
        get { return instance; }
        set { instance = value; }
    }
    public static SimpleInterpolationManager2 instance;
    void Start() => instance = this;
    private int LastID;
    private readonly object lockObject = new();
    private readonly SerializedDictionary<int,Event> AllEvents = new();

    // internal logic, available to public as well if needed for some reason.

    public bool DoesEventExist(int ID){
        return AllEvents.ContainsKey(ID);
    }
    public bool EndEvent(int ID){
        if (DoesEventExist(ID)){
            SetEventValue(ID,AllEvents[ID].to);
            AllEvents.Remove(ID);
            return true;
        }
        return false;
    }
    public void SetEventValue(int ID, float val){
        AllEvents[ID].value=val;
    }
    public float GetEventValue(int ID){
        return DoesEventExist(ID) ? AllEvents[ID].value : 0;
    }
    public bool IsEventFinished(int ID){
        return DoesEventExist(ID) && AllEvents[ID].IsFinished;
    }
    Action<float, int> CreateFuncIfSandbox(Action<float, int> action, bool isSandbox){ // even if used once, to not mess with logic much in launch event
        if(!isSandbox) return action;
        return new ((v, f) => {
            try{ action(v,f); }catch(Exception e){
                Debug.Log($"SIM2 caught an error while running a frame!! continuing anyway....\n{e}");
            }
        });
    }
    /// <summary>
    /// launches the event, duh!
    /// </summary>
    /// <param name="frame">runs every frame during the animation. Automatically sets the frame and such, so no getting the value, game freezing because of missing yield return 0, and no more leftovers.
    //  output arguments: 
    //      value (float) - given by SIM2, the very value we interpolate
    //      frame (int) - frame count</param>
    /// <param name="from">from value (starter value from which we interpolate from)</param>
    /// <param name="to">to value (end value)</param>
    /// <param name="duration">duration</param>
    /// <param name="InterType">interpolation type (check easings)</param>
    /// <param name="StartOffset">time we wait until we start interpolating. doesn't affect frame time</param>
    /// <param name="OnEnd">action to call on the end of the animation</param>
    /// <param name="TimeSpace">what time do we run in?</param>
    /// <returns></returns>
    public void LaunchEventFunc(
        Action<float, int> frame, 
        float from, float to, 
        float duration, 
        InterpolationType InterType, 
        float StartOffset = 0, 
        bool DontRunInSandbox = false, 
        Action OnEnd = null,
        RuntimeSpace TimeSpace = RuntimeSpace.GameTime
    ){    
        StartCoroutine(LaunchEvent(frame, from, to, duration, InterType, StartOffset, DontRunInSandbox, OnEnd, TimeSpace));
    }
    public IEnumerator LaunchEvent(
        Action<float, int> frame, 
        float from, 
        float to, 
        float duration, 
        InterpolationType InterType, 
        float StartOffset = 0, 
        bool DontRunInSandbox = false, 
        Action OnEnd = null,
        RuntimeSpace TimeSpace = RuntimeSpace.GameTime
    ){
        //shotgun bad scenarios before anything happens
        if(duration <= 0) throw new BadAnimValue("Duration shouldn't be less or equal to zero.");
        if(frame == null) throw new BadAnimValue("Animation frame is empty.");
        Func<float,float,float,float> easingPointer = EasingFunctions.GetAction(InterType);
        lock (lockObject){
            LastID++;
            Coroutine cor = StartCoroutine(LaunchEvent(CreateFuncIfSandbox(frame, DontRunInSandbox), LastID, from, to, duration,easingPointer, StartOffset, TimeSpace));
            AllEvents.Add(LastID, new Event(){InterType=InterType, from=from, to=to, value=from, cor=cor,Duration=duration});
        }
        yield return AllEvents[LastID].cor;
        OnEnd?.Invoke();
    }

    private Func<float> GetTimeFunc(RuntimeSpace space) => space switch {
        RuntimeSpace.GameTime => () => Time.time,
        RuntimeSpace.AudioTime => () => (float)AudioSettings.dspTime,
        RuntimeSpace.RealTime => () => Time.realtimeSinceStartup,
        _ => () => Time.time
    };

    IEnumerator LaunchEvent(Action<float, int> frame, int ID, float from, float to, float duration, Func<float,float,float,float> easingPointer, float StartOffset, RuntimeSpace space){
        yield return null;
        var timefunc = GetTimeFunc(space);
        float StartTime = timefunc() + StartOffset;
        int frameTick = 0;
        Event eventRef = AllEvents[ID];
        while((timefunc() - StartTime) < duration){
            if(StartTime > timefunc()){
                yield return null;
                continue;
            }
            float elapsedTime = timefunc() - StartTime;
            float value = easingPointer(from, to, elapsedTime / duration);
            eventRef.value = value;
            frameTick++;

            frame(value, frameTick); // run frame for user
            yield return null;
        }
        EndEvent(ID);
        frame(to, frameTick); // reassure we end up with the last value being correct :-)
        yield return null;
    }

    // quick helper
    public static float QuickLerp(float lerp, bool condition, float delta) { // lerps a value based on condition to 0 or 1, snaps to target when close
        return (lerp > 0.99f && condition) ? 1 :  // snap to 1, cause we're close
               (lerp < 0.03f && !condition) ? 0 : // snap to 0, cause we're close
               Mathf.Lerp(lerp, condition ? 1 : 0, delta); // default case
    }
}