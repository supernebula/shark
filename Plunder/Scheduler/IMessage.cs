using System;


namespace Plunder.Scheduler
{

    public interface IMessage<T> : IMessage
    {
        T Body { get; }
    }

    public interface IMessage
    {
        Guid Id { get; set; }
        object Content { get; set; }

        DateTime Timestamp { get; set; }

        int Priority { get; set; }

        string Topic { get; set; }

        string HashCode { get; }

        string GetTypeName { get; set; }
    }

}
