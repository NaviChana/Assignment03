using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment03
{
    public class SerializedFile : IComparable<SerializedFile>
    {
        public string? Name { get; set; }
        public long Size { get; set; }

        public int CompareTo(SerializedFile? other)
        {
            return Size.CompareTo(other.Size);
        }
    }
}