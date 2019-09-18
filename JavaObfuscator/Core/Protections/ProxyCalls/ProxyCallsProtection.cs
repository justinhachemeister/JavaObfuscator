using JavaObfuscator.Core.Utils;
using JavaResolver.Class.Code;
using JavaResolver.Class.TypeSystem;
using System.Linq;

namespace JavaObfuscator.Core.Protections.ProxyCalls
{
    public class ProxyCallsProtection : IProtection
    {
        public override string ProtectionName => "ProxyCalls";

        public override string Description => "This protection hides references in to new methods.";

        public override string Auth => "CodeOfDark";

        public override void Execute(Context context)
        {
            ProxyCallsUtils proxyCallsUtils = new ProxyCallsUtils(context);

            CallFlags flags = CallFlags.InvokeStatic | CallFlags.InvokeVirtual;

            foreach (MethodDefinition method in context.Class.Methods.ToArray())
            {
                if (context.Engine.UntouchableMethods.Contains(method)) continue;
                for (int i = 0; i < method.Body.Instructions.Count; i++)
                {
                    ByteCodeInstruction byteCodeInstruction = method.Body.Instructions[i];
                    MethodDefinition newMethod = null;
                    IMethod operand = null;
                    if (byteCodeInstruction.OpCode.Equal(ByteOpCodes.InvokeVirtual))
                    {
                        operand = (IMethod)byteCodeInstruction.Operand;
                        if (flags.HasFlag(CallFlags.InvokeVirtual))
                            newMethod = proxyCallsUtils.Create(operand, byteCodeInstruction, true);
                    }
                    else if (byteCodeInstruction.OpCode.Equal(ByteOpCodes.InvokeStatic))
                    {
                        operand = (IMethod)byteCodeInstruction.Operand;
                        if (flags.HasFlag(CallFlags.InvokeStatic))
                            newMethod = proxyCallsUtils.Create(operand, byteCodeInstruction, false);
                    }
                    if (newMethod == null) continue;

                    context.Engine.UntouchableMethods.Add(newMethod);
                    context.Class.Methods.Add(newMethod);
                    method.Body.Instructions[i] = new ByteCodeInstruction(ByteOpCodes.InvokeStatic, new MethodReference(newMethod.Name, context.Class, newMethod.Descriptor));

                }
            }

            context.Engine.UntouchableMethods.Clear();
        }
    }
}
