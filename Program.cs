namespace VeeamTaskKiller
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskKiller task = new TaskKiller();
            task.StartKiller(args[0], args[1], args[2]);
        }
    }
}