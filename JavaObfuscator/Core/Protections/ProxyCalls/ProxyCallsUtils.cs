using JavaResolver.Class.Code;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.TypeSystem;

namespace JavaObfuscator.Core.Protections.ProxyCalls
{
    public class ProxyCallsUtils
    {
        private Context context;

        public ProxyCallsUtils(Context context)
        {
            this.context = context;
        }

        public MethodDefinition Create(IMethod method, ByteCodeInstruction instr, bool virtual_)
        {
            MethodDescriptor methodDescriptor = new MethodDescriptor(method.Descriptor.ReturnType, method.Descriptor.ParameterTypes);

            if (virtual_)
                methodDescriptor.ParameterTypes.Insert(0, new ObjectType(method.DeclaringClass.Name));

            MethodDefinition newMethod = new MethodDefinition(context.StringGenerator.Generate(), methodDescriptor)
            {
                AccessFlags = MethodAccessFlags.Public | MethodAccessFlags.Static
            };

            ByteCodeMethodBody methodBody = new ByteCodeMethodBody();

            for (int i = 0; i < newMethod.Descriptor.ParameterTypes.Count; i++)
            {
                var k = new LocalVariable("arg_" + i, new FieldDescriptor(newMethod.Descriptor.ParameterTypes[i]));
                methodBody.Variables.Add(k);
                methodBody.Instructions.Add(new ByteCodeInstruction(ByteOpCodes.ALoad, i));
                k.Start = methodBody.Instructions[0];
            }

            methodBody.Instructions.Add(instr);

            if (method.Descriptor.ReturnType.Prefix == 'V')
                methodBody.Instructions.Add(new ByteCodeInstruction(ByteOpCodes.Return));
            else
                methodBody.Instructions.Add(new ByteCodeInstruction(ByteOpCodes.AReturn));
            
            newMethod.Body = methodBody;

            return newMethod;
        }

    }
}
