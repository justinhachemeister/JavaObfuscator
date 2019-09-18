using JavaResolver.Class.Code;
using JavaResolver.Class.Descriptors;
using System.Diagnostics;

namespace JavaObfuscator.Core.Utils
{
    public static class JavaResolverUtils
    {
        public static bool IsBaseType(this FieldType fieldType)
        {
            switch (fieldType.Prefix)
            {
                case 'B':
                case 'C':
                case 'D':
                case 'F':
                case 'I':
                case 'J':
                case 'S':
                case 'Z':
                case 'V':
                    return true;
                default:
                    return false;
            }
        }

        public static bool Equal(this ByteOpCode currectOpCode, ByteOpCode opCode)
        {
            return Debug.Equals(currectOpCode, opCode);
        }
    }
}
