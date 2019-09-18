
namespace JavaObfuscator.Core
{
    public abstract class IProtection
    {
        public abstract string ProtectionName { get; }
        public abstract string Description { get; }
        public abstract string Auth { get; }

        public abstract void Execute(Context context);
    }
}
