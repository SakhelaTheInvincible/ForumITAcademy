using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Exceptions
{
    public class UserBannedException : Exception
    {
        public UserBannedException() : base() { }
        public UserBannedException(string message) : base(message) { }

        public readonly string Code = "This User Is Banned";
    }
}
