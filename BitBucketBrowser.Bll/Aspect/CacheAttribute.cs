namespace BitBucketBrowser.Bll.Aspect
{
    using System;
    using System.Runtime.Caching;

    using PostSharp.Aspects;

    [Serializable]
    public sealed class CacheAttribute : OnMethodBoundaryAspect
    {
        private readonly int seconds;

        public CacheAttribute(int seconds)
        {
            this.seconds = seconds;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            ObjectCache cache = MemoryCache.Default;
            string cacheKey = this.GetCacheKey(args);

            if (cache[cacheKey] != null)
            {
                args.ReturnValue = cache[cacheKey];
                return;
            }

            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            ObjectCache cache = MemoryCache.Default;
            string cacheKey = this.GetCacheKey(args);

            cache.Add(cacheKey, args.ReturnValue, DateTimeOffset.Now.AddSeconds(this.seconds));

            base.OnExit(args);
        }

        private string GetCacheKey(MethodExecutionArgs args)
        {
            return string.Format("{0}.{1}", args.Method.DeclaringType, args.Method.Name);
        }
    }
}
