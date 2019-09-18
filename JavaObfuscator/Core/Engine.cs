using JavaResolver.Class.TypeSystem;
using System.Collections.Generic;

namespace JavaObfuscator.Core
{
    public class Engine
    {

        public Context context { get; private set; }
        public List<MethodDefinition> UntouchableMethods;

        private List<IProtection> GetProtections;

        public Engine(Context context)
        {
            this.context = context;
            this.UntouchableMethods = new List<MethodDefinition>();

            this.GetProtections = new List<IProtection>()
            {
                new Protections.Strings.StringsProtection(),
                new Protections.Outliner.OutlinerProtection(),
                new Protections.ProxyCalls.ProxyCallsProtection()
            };
        }

        public void Executor()
        {
            foreach(IProtection protection in GetProtections)
            {
                context.Logger.Info("Executing -> " + protection.ProtectionName + ".");
                try
                {
                    protection.Execute(context);
                    context.Logger.Successed("Protected with -> " + protection.ProtectionName);
                }
                catch (JavaProtectionException javaProtectionException)
                {
                    context.Logger.Failed("Protection: " + protection.ProtectionName + ", " + javaProtectionException.Message);
                }
            }
        }
    }
}
