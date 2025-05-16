using ScheduleOne.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleGore.Blood.Generics
{
    public class CallbackInfo
    {
        public string note = "made this as a gag but i put time into making this so im keeping it. never gunna be used though lmao";
        public MethodInfo TargetMethod { get; private set; }
        public bool IsCancellable { get; private set; }

        public CallbackInfo(MethodInfo callback, bool isCancellable, bool cancelled=false)
        {
            TargetMethod = callback;
            IsCancellable = isCancellable;
            _cancelled = cancelled;
        }



        internal bool _cancelled = false;



        public void Cancel()
        {
            _cancelled = true;
        }

        public void Resume()
        {
            _cancelled = false;
        }

        public bool Status()
        {
            return _cancelled;
        }
    }
}
