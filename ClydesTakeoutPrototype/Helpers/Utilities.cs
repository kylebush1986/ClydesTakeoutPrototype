using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClydesTakeoutPrototype.Helpers
{
    /// <summary>
    /// Useful Helper functions
    /// </summary>
    internal static class Utilities
    {
        /// <summary>
        /// Generate DJB2 hash from a string.
        /// </summary>
        /// <param name="inputString">A string</param>
        /// <returns>A hash</returns>
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

        /// <summary>
        /// Get a new unique ID.
        /// </summary>
        /// <returns>A unique ID</returns>
        internal static string GetNewUniqueID()
        {
            string returnValue = Guid.NewGuid().ToString("N");
            return returnValue;
        }

        /// <summary>
        /// Generate a GUID.
        /// </summary>
        /// <returns>A unique ID</returns>
        internal static ulong GenerateGuid()
        {
            ulong id = 0;
            string guid = Utilities.GetNewUniqueID();
            while(id == 0)
            {
                id = Utilities.GenerateDjb264Hash(guid);
            }
            return id;
        }
    }
}
