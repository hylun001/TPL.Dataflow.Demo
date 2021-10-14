# TPL.Dataflow.Demo
TPL Dataflow是微软面向高并发应用而推出的一个类库。TPL两个非常重要的接口，ISourceBlock&lt;T> 和ITargetBlock&lt;T>接口。继承于ISourceBlock&lt;T>的对象时作为提供数据的数据源对象-生产者，而继承于ITargetBlock&lt;T>接口类主要是扮演目标对象-消费者。在这个类库中，System.Threading.Tasks.Dataflow名称空间下，提供了很多以Block名字结尾的类，ActionBlock，BufferBlock，TransformBlock，BroadcastBlock等9个Block，我们在开发中常使用单播和多播实现一些功能，Demo中使用了BufferBlock和BroadcastBlock。仅供学习交流。。
