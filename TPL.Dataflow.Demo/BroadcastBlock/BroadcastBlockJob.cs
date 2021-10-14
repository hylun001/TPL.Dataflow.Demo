using System;
using System.Threading.Tasks.Dataflow;

namespace TPL.Dataflow.Demo
{
    /// <summary>
    /// 广播事件
    /// BroadcastBlock 只保存一条数据，并且下一条数据覆盖上一条数据
    /// EventBus
    /// </summary>
    public class BroadcastBlockJob
    {
        private static BroadcastBlock<IDataflowJob> _jobs = new BroadcastBlock<IDataflowJob>(job => job);

        /// <summary>
        /// 订阅事件
        /// 必须在发送之前，进行事件订阅，因为只保存一条数据，之前的事件数据都将无法订阅
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void RegisterHandler<T>(Action<T> action)
        {
            ActionBlock<IDataflowJob> targetAction = new ActionBlock<IDataflowJob>(job => action((T)job), new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 10//目标线程数
            });

            _jobs.LinkTo(targetAction, job => job is T);
        }

        /// <summary>
        /// 数据入队列
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public void Enqueue(IDataflowJob job)
        {
            Console.WriteLine($"send  {job.Id} - {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}");
            _jobs.Post(job);
        }
    }
}
