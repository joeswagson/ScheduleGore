using ScheduleGore.Blood;
using ScheduleGore.Blood.Generics;
using ScheduleOne.Combat;
using ScheduleOne.NPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGore.Helpers
{
    internal class GunshotController
    {
        public static void Hit(NPC impacted, Impact impact)
        {
            impacted.GetComponentInParent<VisualDamageController>()?.ImpactRecieved(impacted, impact);
            impacted.GetComponentInParent<SplatController>()?.ImpactRecieved(impacted, impact);
            impacted.GetComponentInParent<SoundController>()?.ImpactRecieved(impacted, impact);
            impacted.GetComponentInParent<PoolController>()?.ImpactRecieved(impacted, impact);
        }
    }
}
