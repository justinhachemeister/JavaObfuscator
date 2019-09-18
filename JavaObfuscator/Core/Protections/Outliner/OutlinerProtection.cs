using JavaObfuscator.Core.Utils;
using JavaResolver.Class.Code;
using JavaResolver.Class.TypeSystem;
using System;
using System.Linq;

namespace JavaObfuscator.Core.Protections.Outliner
{
    public class OutlinerProtection : IProtection
    {
        public override string ProtectionName => "Outliner";

        public override string Description => "Create a new method for each constant(strings, int, float).";

        public override string Auth => "CodeOfDark";

        public override void Execute(Context context)
        {
            OutlinerUtils outlinerUtils = new OutlinerUtils(context);

            OutlinerFlags flags = OutlinerFlags.Strings | OutlinerFlags.Ints | OutlinerFlags.Floats | OutlinerFlags.Doubles;

            foreach (MethodDefinition method in context.Class.Methods.ToArray())
            {
                if (context.Engine.UntouchableMethods.Contains(method)) continue;
                for (int i = 0; i < method.Body.Instructions.Count; i++)
                {
                    ByteCodeInstruction byteCodeInstruction = method.Body.Instructions[i];
                    if (byteCodeInstruction.OpCode.Equal(ByteOpCodes.Ldc))
                    {
                        MethodDefinition newMethod = null;
                        object operand = byteCodeInstruction.Operand;
                        Type operandType = operand.GetType();
                        if (operandType == typeof(string))
                        {
                            if (flags.HasFlag(OutlinerFlags.Strings))
                                newMethod = outlinerUtils.Create((string)operand, OutlinerTarget.String);
                        }
                        else if (operandType == typeof(int))
                        {
                            if (flags.HasFlag(OutlinerFlags.Ints))
                                newMethod = outlinerUtils.Create((int)operand, OutlinerTarget.Int);
                        }
                        else if (operandType == typeof(double))
                        {
                            if (flags.HasFlag(OutlinerFlags.Doubles))
                                newMethod = outlinerUtils.Create((double)operand, OutlinerTarget.Double);
                        }
                        else if (operandType == typeof(float))
                        {
                            if (flags.HasFlag(OutlinerFlags.Floats))
                                newMethod = outlinerUtils.Create((float)operand, OutlinerTarget.Float);
                        }
                        if (newMethod == null) continue;
                        context.Engine.UntouchableMethods.Add(newMethod);
                        context.Class.Methods.Add(newMethod);

                        method.Body.Instructions[i] = new ByteCodeInstruction(ByteOpCodes.InvokeStatic, new MethodReference(newMethod.Name, context.Class, newMethod.Descriptor));

                    }
                }
            }

            context.Engine.UntouchableMethods.Clear();
        }
    }
}
