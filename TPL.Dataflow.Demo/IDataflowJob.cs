using System;
using System.Collections.Generic;
using System.Text;

namespace TPL.Dataflow.Demo
{
    /// <summary>
    /// 数据流工作接口
    /// </summary>
    public interface IDataflowJob
    {
        int Id { get; set; }
    }
}
