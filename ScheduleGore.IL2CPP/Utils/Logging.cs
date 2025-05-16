using MelonLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGore.Utils
{
    internal class Logging
    {
        public static bool NullSanity([NotNullWhen(true)] object? inQuestion, string nameForObject="Something (Unspecified)")
        {
            if (inQuestion == null)
                Melon<ModMain>.Logger.Error($"{nameForObject} was null.");

            return inQuestion != null;
        }
    }
}
