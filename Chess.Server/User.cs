using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chess.Server
{
    public class User
    {
        public uint ID { get; private set; }
        public string Name { get; private set; }

        public User(uint id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
