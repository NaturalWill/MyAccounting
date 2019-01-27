using Snowflake.Core;

namespace WebMyAccount.Helper
{
    public static class IdHelper
    {
        static IdWorker worker = new IdWorker(1, 1);
        public static long Next() => worker.NextId();
    }
}
