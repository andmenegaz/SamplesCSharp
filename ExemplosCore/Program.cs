using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace ExemplosCore
{
    internal class ExemplosCore
    {

        private const Decimal OneKiloByte = 1024M;
        private const Decimal OneMegaByte = OneKiloByte * 1024M;
        private const Decimal OneGigaByte = OneMegaByte * 1024M;

        static void Main(string[] args)
        {
            DriveInfo driveInfo = new DriveInfo(Assembly.GetEntryAssembly().Location);
            Console.WriteLine(GetFileSize(driveInfo.AvailableFreeSpace));
        }

        public static string GetFileSize(decimal size)
        {
            string suffix;
            if (size > OneGigaByte)
            {
                size /= OneGigaByte;
                suffix = "GB";
            }
            else if (size > OneMegaByte)
            {
                size /= OneMegaByte;
                suffix = "MB";
            }
            else if (size > OneKiloByte)
            {
                size /= OneKiloByte;
                suffix = "kB";
            }
            else
            {
                suffix = "B";
            }
            return size.ToString("N2") + suffix;
        }
    }
}

