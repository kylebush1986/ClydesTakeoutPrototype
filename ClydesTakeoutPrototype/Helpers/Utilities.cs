using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClydesTakeoutPrototype.Helpers
{
    internal static class Utilities
    {
        internal static ulong GenerateDjb264Hash(string inputString)
        {
            int hash = 5381;
            int hash2 = 52711;

            for (int i = inputString.Length - 1; i >= 0; i--)
            {
                int c = inputString[i];
                hash = (hash * 33) ^ c;
                hash2 = (hash2 * 33) ^ c;
            }

            ulong partOne = ((uint)hash >> 0);
            ulong partTwo = (uint)hash2 >> 0;

            return (partOne * 4096) + partTwo;
        }

        internal static string GetSessionKey()
        {
            string returnValue = Guid.NewGuid().ToString("N");
            return returnValue;
        }

        internal static ulong GenerateGuid()
        {
            ulong id = 0;
            string guid = Utilities.GetSessionKey();
            while(id == 0)
            {
                id = Utilities.GenerateDjb264Hash(guid);
            }
            return id;
        }
    }
}
