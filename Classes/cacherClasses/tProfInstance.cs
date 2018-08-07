
using System;
using System.Collections.Generic;

namespace cacher
{
    public class TProfInstance
    {

        public TCacheGrind CacheGrind;
        public int CallCount;
        public TProfInstance Caller;
        public List<TProfInstance> Calls;
        public double CumPercent
        {
            get
            {
                double Result = 0;
                if ((Caller == null) || (Caller.CumTime <= 0))
                {
                    Result = 100.0;
                }
                else
                {
                    Result = CumTime * 100.0 / Caller.CumTime;
                }
                return Result;
            }
        }

        public double CumTime;

        //public int Data_NodeIndex;  //this was called 'data' in pascal but was used to store the TreeNode into which the instance was stored.
        public string FileName
        {
            get
            {
                return Func.FileName;
            }
        }
        public TProfFunc Func;
        public int Index;

        public TFuncKind Kind
        {
            get
            {
                return Func.Kind;
            }
        }

        public int Line;
        public string Name;
        public int ParserCallLine;
        public int ParserLine;
        public double SelfTime;
        public double SelfPercent
        {
            get
            {
                if ((Caller == null) || (Caller.CumTime <= 0))
                {
                    return 0.0;
                }
                else
                {
                    return SelfTime * 100.0 / Caller.CumTime;
                }
            }
        }
        public string ShortFileName
        {
            get
            {
                return Func.ShortFileName;
            }
        }
        public string ShortName
        {
            get
            {
                return Func.ShortName;
            }
        }


        public TProfInstance(TProfFunc AFunc, int AIndex)
        {
            Func = AFunc;
            Calls = new List<TProfInstance>();
            Index = AIndex;
        }

        public void GetMerged(ref List<TProfFunc> AList)
        {
            int I;

            for (I = 0; I < CallCount; I++)
            {
                if (AList.IndexOf(Calls[I].Func) < 0)
                {
                    AList.Add(Calls[I].Func);
                }
                Calls[I].GetMerged(ref AList);
            }
        }

        public void InsertCall(int AIndex, TProfInstance ATarget)
        {
            Calls.Add(ATarget);
            CallCount += 1;
            ATarget.Caller = this;
        }
        public void Reset()
        {
            // reset this instance

            // propagate
            for (int i = 0; i < CallCount; i++)
            {
                Calls[i].Reset();
            }
        }

        public void Analyze()
        {
            int I;

            // report
            //CacheGrind.AnalyzePosition += 1;
            // be a bit lazy so updates won't be too fast
            /*TODO:Later
            if (CacheGrind.AnalyzePosition % 1000 == 0)
            {
                if (Assigned(CacheGrind.OnAnalyzeProgress))
                {
                    CacheGrind.OnAnalyzeProgress(CacheGrind, CacheGrind.AnalyzePosition, CacheGrind.AnalyzeMax, "Analyzing " + Name + "...");
                }
            }
            */
            // propagate
            for (I = 0; I < CallCount; I++)
            {
                Calls[I].Analyze();
            }
            // special case
            if (Kind == TFuncKind.fkRoot || Kind == TFuncKind.fkSection)
            {
                CumTime = SelfTime;
                for (I = 0; I < CallCount; I++)
                {
                    CumTime = CumTime + Calls[I].CumTime;
                }
            }
        }
    }
}
