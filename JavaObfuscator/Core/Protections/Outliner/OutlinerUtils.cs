using JavaResolver.Class.Code;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.TypeSystem;

namespace JavaObfuscator.Core.Protections.Outliner
{
    public class OutlinerUtils
    {
        private Context context;

        public OutlinerUtils(Context context)
        {
            this.context = context;
        }

        public MethodDefinition Create(object value, OutlinerTarget target)
        {
            MethodDescriptor methodDescriptor = new MethodDescriptor(FieldTarget(target));
            MethodDefinition newMethod = new MethodDefinition(context.StringGenerator.Generate(), methodDescriptor)
            {
                AccessFlags = MethodAccessFlags.Public | MethodAccessFlags.Static
            };

            ByteCodeMethodBody methodBody = new ByteCodeMethodBody()
            {
                Instructions =
                {
                    new ByteCodeInstruction(ByteOpCodes.Ldc, value),
                    new ByteCodeInstruction(OpCodeTarget(target))
                }
            };
            newMethod.Body = methodBody;
            return newMethod;
        }

        private FieldType FieldTarget(OutlinerTarget target)
        {
            switch (target)
            {
                case OutlinerTarget.String:
                    return ObjectType.String;
                case OutlinerTarget.Int:
                    return BaseType.Int;
                case OutlinerTarget.Double:
                    return BaseType.Double;
                case OutlinerTarget.Float:
                    return BaseType.Float;
                default:
                    return ObjectType.String;
            }
        }

        private ByteOpCode OpCodeTarget(OutlinerTarget target)
        {
            switch (target)
            {
                case OutlinerTarget.String:
                    return ByteOpCodes.AReturn;
                case OutlinerTarget.Int:
                    return ByteOpCodes.IReturn;
                case OutlinerTarget.Double:
                    return ByteOpCodes.DReturn;
                case OutlinerTarget.Float:
                    return ByteOpCodes.FReturn;
                default:
                    return ByteOpCodes.Nop;
            }
        }
    }
}
