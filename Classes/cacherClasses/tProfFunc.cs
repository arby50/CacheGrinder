
using System;
using System.Collections.Generic;

namespace cacher
{
    public enum TFuncKind
    {
        fkUnknown, fkRoot, fkSection, fkFunc, fkConstructor, fkDestructor,
        fkPublicMethod, fkPrivateMethod, fkStaticMethod, fkInclude, fkLibFunc
    };

    public class TProfFunc
    {
        public double AvgCumPercent
        {
            get
            {
                if (CacheGrind != null && CacheGrind.Root != null && CacheGrind.Root.CumTime > 0)
                {
                    return AvgCumTime * 100.0 / CacheGrind.Root.CumTime;
                }
                else
                {
                    return 100.0;
                }
            }
        }
        public double AvgCumTime
        {
            get
            {
                return TotCumTime / InstanceCount;
            }
        }
        public double AvgSelfPercent
        {
            get
            {
                if (CacheGrind != null && CacheGrind.Root != null && CacheGrind.Root.CumTime > 0)
                {
                    return AvgSelfTime * 100.0 / CacheGrind.Root.CumTime;
                }
                else
                {
                    return 0.0;
                }
            }
        }
        public double AvgSelfTime
        {
            get
            {
                return TotSelfTime / InstanceCount;
            }
        }
        public TCacheGrind CacheGrind;
        public string FileName;
        public int InstanceCount
        {
            get
            {
                return Instances.Count;
            }
        }

        public List<TProfInstance> Instances;
        public TFuncKind Kind;
        public string Name;
        public string ShortFileName;
        public string ShortName;
        public double TotCumPercent
        {
            get
            {
                if (CacheGrind.Root.CumTime > 0)
                {
                    return TotCumTime * 100.0 / CacheGrind.Root.CumTime;
                }
                else
                {
                    return 100.0;
                }
            }
        }
        public double TotCumTime;
        public double TotSelfPercent
        {
            get
            {
                if (CacheGrind.Root.CumTime > 0)
                {
                    return TotSelfTime * 100.0 / CacheGrind.Root.CumTime;
                }
                else
                {
                    return 0.0;
                }
            }
        }
        public double TotSelfTime;
        //function AddInstance: TProfInstance;
        public void Analyze()
        {
            for (int i = 0; i < InstanceCount; i++)
            {

                TotCumTime = TotCumTime + Instances[i].CumTime;
                TotSelfTime = TotSelfTime + Instances[i].SelfTime;
            }
        }

        public TProfInstance AddInstance()
        {
            TProfInstance Inst;

            Inst = new TProfInstance(this, Instances.Count);
            Inst.Name = this.Name;
            //Inst.ShortName = GetShortName();  //ryan: handled in getter now
            Instances.Add(Inst);
            return Inst;
        }

        public TProfFunc(TCacheGrind ACacheGrind, string AName, string AFileName)
        {
            CacheGrind = ACacheGrind;
            Name = AName;
            FileName = AFileName;
            ShortFileName = GetShortFileName();
            ShortName = GetShortName();
            Instances = new List<TProfInstance>();
            // analyze Name to get Kind
            Kind = TFuncKind.fkFunc;
            if (AName.StartsWith("include::") || AName.StartsWith("include_once::") || AName.StartsWith("require::") || AName.StartsWith("require_once::"))
            {
                Kind = TFuncKind.fkInclude;
            }
            else if (AName.StartsWith("php::"))
            {
                Kind = TFuncKind.fkLibFunc;
            }
            else if (IsCons(AName))
            {
                Kind = TFuncKind.fkConstructor;
            }
            else if (IsDest(AName))
            {
                Kind = TFuncKind.fkDestructor;
            }
            else if (AName.Contains("->object") || AName.Contains("__construct") || AName.Contains("::pear"))
            {
                Kind = TFuncKind.fkConstructor;
            }
            else if (AName.Contains("__destruct") || AName.Contains("_object") || AName.Contains("::_pear"))
            {
                Kind = TFuncKind.fkDestructor;
            }
            else if (AName.Contains("::_") || AName.Contains("->_"))
            {
                Kind = TFuncKind.fkPrivateMethod;
            }
            else if (AName.Contains("::"))
            {
                Kind = TFuncKind.fkStaticMethod;
            }
            else if (AName.Contains("->"))
            {
                Kind = TFuncKind.fkPublicMethod;
            }
        }

        private bool IsCons(string S)
        {
            int I;
            string A;
            string B;

            I = S.IndexOf("->");
            if (I >= 1)
            {
                A = S.Substring(0, I);
                B = S.Substring(I + 2, S.Length - (I + 2));
                return A == B;
            }
            else
            {
                return false;
            }
        }

        private bool IsDest(string S)
        {
            int I;
            string A, B;

            I = S.IndexOf("->");
            if (I >= 1)
            {
                A = S.Substring(0, I);
                B = S.Substring(I + 2, S.Length - (I + 2));
                return B == ("_" + A);
            }
            else
            {
                return false;
            }
        }

        public int IndexOfInstance(TProfInstance AInst)
        {
            return Instances.IndexOf(AInst);
        }

        public void Reset()
        {
            TotCumTime = 0;
            TotSelfTime = 0;
        }

        public string GetShortFileName()
        {
            string Result = string.Empty;
            if (FileName.IndexOf("php:") != 1)
            {
                Result = System.IO.Path.GetFileName(FileName);
            }
            else
            {
                Result = FileName;
            }
            return Result;
        }

        public string GetShortName()
        {
            string Result = string.Empty;
            if (Kind == TFuncKind.fkRoot)
            {
                Result = System.IO.Path.GetFileName(Name);
            }
            else
            {
                if (Name.IndexOf("include::") == 0
                    || (Name.IndexOf("include_once::") == 0)
                    || (Name.IndexOf("require::") == 0)
                    || (Name.IndexOf("require_once::") == 0))
                {
                    int I = Name.IndexOf("::");
                    Result = Name.Substring(0, I + 2) + System.IO.Path.GetFileName(Name.Substring(I + 2, (Name.Length - (I + 2))));
                }
                else
                {
                    Result = Name;
                }
            }
            return Result;
        }
    }
}