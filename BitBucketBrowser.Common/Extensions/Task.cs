namespace BitBucketBrowser.Common.Extensions
{
    using System;
    using System.Threading.Tasks;

    public static class Task
    {
        public static void ContinueWithUi<TResult>(this Task<TResult> task, Action<Task<TResult>> action)
        {
            task.ContinueWith(action, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static void ContinueWithUi(this System.Threading.Tasks.Task task, Action<System.Threading.Tasks.Task> action)
        {
            task.ContinueWith(action, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}