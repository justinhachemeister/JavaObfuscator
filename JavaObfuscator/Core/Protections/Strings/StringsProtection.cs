using JavaObfuscator.Core.Utils;
using JavaResolver.Class.Code;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.TypeSystem;
using System;
using System.Linq;
using System.Text;

namespace JavaObfuscator.Core.Protections.Strings
{
    public class StringsProtection : IProtection
    {
        public override string ProtectionName => "StringsEncoding";

        public override string Description => "Encode the string, based on Base64 method.";

        public override string Auth => "CodeOfDark";

        public override void Execute(Context context)
        {
            foreach (MethodDefinition method in context.Class.Methods.ToArray())
            {
                if (context.Engine.UntouchableMethods.Contains(method)) continue;
                for (int i = 0; i < method.Body.Instructions.Count; i++)
                {
                    ByteCodeInstruction byteCodeInstruction = method.Body.Instructions[i];
                    if (byteCodeInstruction.OpCode.Equal(ByteOpCodes.Ldc))
                    {
                        object operand = byteCodeInstruction.Operand;
                        Type operandType = operand.GetType();

                        if (operandType == typeof(string))
                        {
                            string value = (string)operand;
                            string encodedValue = Encode(value);

                            var newString = new ClassReference("java/lang/String");
                            var getDecoder = new MethodReference("getDecoder", new ClassReference("java/util/Base64"), 
                                new MethodDescriptor(new ObjectType("java/util/Base64$Decoder")));
                            var decode = new MethodReference("decode", new ClassReference("java/util/Base64$Decoder"), 
                                new MethodDescriptor(new ArrayType(new BaseType(BaseTypeValue.Byte)), ObjectType.String));
                            var initString = new MethodReference("<init>", new ClassReference("java/lang/String"), 
                                new MethodDescriptor(BaseType.Void, new ArrayType(new BaseType(BaseTypeValue.Byte))));

                            method.Body.Instructions[i] = new ByteCodeInstruction(ByteOpCodes.New, newString);
                            method.Body.Instructions.Insert(i + 1, new ByteCodeInstruction(ByteOpCodes.Dup));
                            method.Body.Instructions.Insert(i + 2, new ByteCodeInstruction(ByteOpCodes.InvokeStatic, getDecoder));
                            method.Body.Instructions.Insert(i + 3, new ByteCodeInstruction(ByteOpCodes.Ldc, encodedValue));
                            method.Body.Instructions.Insert(i + 4, new ByteCodeInstruction(ByteOpCodes.InvokeVirtual, decode));
                            method.Body.Instructions.Insert(i + 5, new ByteCodeInstruction(ByteOpCodes.InvokeSpecial, initString));

                            i += 5;
                        }
                    }
                }
            }
        }

        private string Encode(string str)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(str));
        }
    }
}
