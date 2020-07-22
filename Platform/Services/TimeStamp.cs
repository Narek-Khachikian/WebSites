using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Services
{
    public interface ITimeStamp
    {
        string TimeStamp { get; } 
    }

    public class DefaultTimeStamp : ITimeStamp
    {
        public string TimeStamp { get => DateTime.Now.ToShortTimeString(); }
    }
}
