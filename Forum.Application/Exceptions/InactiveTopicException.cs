using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Exceptions
{
    public class InactiveTopicException : Exception
    {
        public InactiveTopicException() : base() { }
        public InactiveTopicException(string message) : base(message) { }

        public readonly string Code = "Topic Is Inactive, Writing Comments Is Impossible";
    }
}
