using ScheduleGore.Blood;
using Il2CppScheduleOne.Combat;
using Il2CppScheduleOne.NPCs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleGore.Helpers
{
    internal class BluntController
    {
        public static void Hit(NPC impacted, Impact impact)
        {
            impacted.GetComponentInParent<VisualDamageController>()?.ImpactRecieved(impacted, impact);
            impacted.GetComponentInParent<SoundController>()?.ImpactRecieved(impacted, impact);
            impacted.GetComponentInParent<PoolController>()?.ImpactRecieved(impacted, impact);
        }
    }
}
