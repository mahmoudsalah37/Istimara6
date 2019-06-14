using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astmara6.Classes
{
    static class TransferData
    {
        private static string semester;
        private static string year;

        public static string Semester { get => semester; set => semester = value; }
        public static string Year { get => year; set => year = value; }
    }
}
