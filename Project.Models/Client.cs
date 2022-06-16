using MessagePack;
using System;

namespace Project.Models
{
    [MessagePackObject]
    public class Client
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public string Name { get; set; }
        [Key(2)]
        public DateTime DateTime { get; set; }

    }
}
