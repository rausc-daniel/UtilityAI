using UnityEngine;

namespace EventSystem
{
    public abstract class Events
    {
        public abstract class UtilityAi
        {
            public class OnValueChanged : GameEvent { }
            
            public class OnActionChanged : GameEvent
            {
                public static Action Action;
                public static AiClient Origin;

                public OnActionChanged(AiClient origin, Action action)
                {
                    Origin = origin;
                    Action = action;
                }
            }
        }

        public abstract class Senses
        {
            public class SpottingChanged : GameEvent
            {
                public static GameObject Target;
                public static AiClient Client;

                public SpottingChanged(AiClient client, GameObject target)
                {
                    Client = client;
                    Target = target;
                }
            }
        }
    }
}