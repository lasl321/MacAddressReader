using System;
using System.Linq;
using System.Management;
using System.Text;

namespace MacAddressReader
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(GetMacAddress());
        }

        public static string GetMacAddress()
        {
            var managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var instances = managementClass.GetInstances();
            var managementObjects = instances
                .Cast<ManagementObject>()
                .First(x => HasMacAddress(x) && IsIpEnabled(x));

            return GetMacAddress(managementObjects);
        }

        private static bool IsIpEnabled(ManagementBaseObject x)
        {
            return Convert.ToBoolean(x["IPEnabled"]);
        }

        private static bool HasMacAddress(ManagementBaseObject x)
        {
            return GetMacAddress(x) != null;
        }

        private static string GetMacAddress(ManagementBaseObject x)
        {
            return (string) x["MACAddress"];
        }
    }
}