using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ScheduleGore.Embedded
{
    internal class EAB
    {
        public static AssetBundle? LoadFromAssembly(Assembly assembly, string name)
        {
            string[] manifestResources = assembly.GetManifestResourceNames();

            if (manifestResources.Contains(name))
            {
                byte[] bytes;
                using (Stream? str = assembly.GetManifestResourceStream(name))
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    str?.CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                }

                var temp = AssetBundle.LoadFromMemory(bytes);
                return temp;
            }

            return null;
        }
    }
}
