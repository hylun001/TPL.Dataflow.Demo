using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace TPL.Dataflow.Demo.BufferBlock
{


    /// <summary>
    /// 将数据存储在缓冲区
    /// 一对一订阅
    /// 重启进程，缓冲区数据才会将丢失，否则，一直存储
    /// </summary>
    public class BufferBlockJob
    {
        private readonly BufferBlock<IDataflowJob> _jobs = new BufferBlock<IDataflowJob>();

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void RegisterHandler<T>(Action<T> action)
        {
            ActionBlock<IDataflowJob> targetBlock = new ActionBlock<IDataflowJob>(job => action((T)job), new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 10 //目标线程数
            });

            _jobs.LinkTo(targetBlock, job => job is T);
        }


        /// <summary>
        /// 数据入队列
        /// </summary>
        /// <param name="job"></param>
        public void Enqueue(IDataflowJob job)
        {
            Console.WriteLine($"send  {job.Id} - {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}");
            _jobs.Post(job);
        }
    }
}
