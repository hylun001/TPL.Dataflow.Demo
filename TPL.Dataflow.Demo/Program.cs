using System;
using System.Threading;
using System.Threading.Tasks;
using TPL.Dataflow.Demo.BufferBlock;

namespace TPL.Dataflow.Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //BroadcastBlock();
            BufferBlock();

            (string name, int id, double score) res = ValueTuple.Create("您好", 1, 10.1);
            int id = res.id;
            double score = res.score;
            string name = res.name;


            Console.ReadLine();
        }

        private static void BufferBlock()
        {
            BufferBlockJob blockJob = new BufferBlockJob();



            for (int i = 1; i < 100; i++)
            {
                blockJob.Enqueue(new MessageDataflow() { Id = i, Name = i.ToString() });
                //blockJob.Enqueue(new PersonDataflow() { Id = i, Name = i.ToString(), Sex = i % 2 == 0 });
            }


            Thread.Sleep(2000);
            blockJob.RegisterHandler<MessageDataflow>((msg) =>
            {
                if (msg == null)
                {
                    return;
                }
                Console.WriteLine($"receive1: {msg.Id} - {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}");

            });

            blockJob.RegisterHandler<MessageDataflow>((msg) =>
            {
                if (msg == null)
                {
                    return;
                }

                Console.WriteLine($"receive2: {msg.Id} - {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}");
            });



            blockJob.RegisterHandler<PersonDataflow>((msg) =>
            {
                if (msg == null)
                {
                    return;
                }
                Console.WriteLine($"receive3: {msg.Id} - {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}");


            });
            //Parallel.For(1, 100, (item) =>
            //{
            //    blockJob.Enqueue(new MessageDataflow() { Id = item, Name = item.ToString() });
            //});
        }




        private static void BroadcastBlock()
        {
            BroadcastBlockJob blockJob = new BroadcastBlockJob();
            blockJob.RegisterHandler<MessageDataflow>((msg) =>
            {
                if (msg == null)
                {
                    return;
                }
                Console.WriteLine($"receive1 :{msg.Id} - {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}");

            });

            //blockJob.RegisterHandler<MessageDataflow>((msg) =>
            //{
            //    if (msg == null)
            //    {
            //        return;
            //    }
            //    Console.WriteLine($"receive2 {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}");
            //    Console.WriteLine($"2<<{Newtonsoft.Json.JsonConvert.SerializeObject(msg)}>>");

            //});



            blockJob.RegisterHandler<PersonDataflow>((msg) =>
            {
                if (msg == null)
                {
                    return;
                }
                Console.WriteLine($"receive3 :{msg.Id} - {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}");


            });


            for (int i = 1; i < 100; i++)
            {
                blockJob.Enqueue(new MessageDataflow() { Id = i, Name = i.ToString() });
                blockJob.Enqueue(new PersonDataflow() { Id = i, Name = i.ToString(), Sex = i % 2 == 0 });
            }

            //Parallel.For(1, 100, (item) =>
            //{
            //    blockJob.Enqueue(new MessageDataflow() { Id = item, Name = item.ToString() });
            //});
        }

    }


    public class MessageDataflow : IDataflowJob
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }


    public class PersonDataflow : IDataflowJob
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Sex { get; set; }
    }
}
