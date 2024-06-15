using System;

namespace Core.Scripts.DI
{
    public class DIRegistartion
    {
        public Func<DIContainer, object> Factory { get; set; }
        public bool IsSingleton { get; set; }
        public object Instance { get; set; }
    }
}