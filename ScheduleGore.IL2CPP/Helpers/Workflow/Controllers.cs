using ScheduleGore.Blood;
using ScheduleGore.Blood.Generics;
using Il2CppScheduleOne.NPCs;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGore.Helpers.Workflow
{
    internal class Controllers
    {
        public NPC NPC { get; set; } = null;
        Cacheable<VisualDamageController> VDC;
        Cacheable<SplatController> SC;
        Cacheable<SoundController> SFXC;
        Cacheable<PoolController> PC;
        public Controllers(NPC npc)
        {
            NPC = npc;

            VDC = new(this);
            SC = new(this);
            SFXC = new(this);
            PC = new(this);
        }

        public class Cacheable<T> where T : GenericImpactReciever
        {
            protected T? cached;
            private Controllers obj;
            public Cacheable(Controllers obj, T? instance=null)
            {
                this.obj = obj;
                cached = instance;
            }

            public struct Result
            {
                public T? instance;
                public bool success;
            }

            public Result GetInstance()
            {
                Result result;
                if (cached != null)
                {
                    result.success = true;
                    result.instance = cached;
                    return result;
                }
                T component = obj.NPC.gameObject.GetComponentInParent<T>();
                if (component)
                {
                    result.success = true;
                    result.instance = component;
                    return result;
                }

                result.success = false;
                result.instance = null;
                return result;
            }
        }

        public bool VisualDamageController(out VisualDamageController controller)
        {
            var result = VDC.GetInstance();

            controller = result.instance!;
            return result.success;
        }
        public bool SplatController(out SplatController controller)
        {
            var result = SC.GetInstance();

            controller = result.instance!;
            return result.success;
        }
        public bool SoundController(out SoundController controller)
        {
            var result = SFXC.GetInstance();

            controller = result.instance!;
            return result.success;
        }
        public bool PoolController(out PoolController controller)
        {
            var result = PC.GetInstance();

            controller = result.instance!;
            return result.success;
        }
    }
}
