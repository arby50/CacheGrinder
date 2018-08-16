
using System;
using System.Collections.Generic;

namespace cacher
{
    public class PCallBuffer
    {
        public string Name;
        public int Line;
        public int ParserLine;
        public double CumTime;
    }

    public class TCacheGrind
    {
        public int AnalyzePosition = 0;
        public int AnalyzeMax;
        public string Cmd;
        public int FuncCount;
        public List<TProfFunc> Funcs;
        public TProfInstance Main;
        public Dictionary<string, TProfFunc> Map;
        public List<string> MapTraits;
        //property OnAnalyzeProgress: TProgressEvent read FOnAnalyzeProgress write FOnAnalyzeProgress;
        //property OnLoadProgress: TProgressEvent read FOnLoadProgress write FOnLoadProgress;
        //property Owner: TObject read FOwner;
        public TProfInstance Root;
        public int SectionCount;
        public Dictionary<int, TProfInstance> dSections;
        public bool SummaryExists;
        public string Version;
        public delegate void UpdateProgressDelegate(int ProgressPercentage, string text);
        public event UpdateProgressDelegate UpdateProgress;

        public void Clear()
        {
            Cmd = "";
            Main = null;
            Root = null;
            Funcs.Clear();
            dSections.Clear();
            Map.Clear();
            Version = "";
        }

        public TCacheGrind()
        {
            Funcs = new List<TProfFunc>();
            dSections = new Dictionary<int, TProfInstance>();
            MapTraits = new List<string>();
            Map = new Dictionary<string, TProfFunc>();
        }

        public TProfFunc FindFunc(string AName)
        {
            if (Map.ContainsKey(AName))
            {
                return Map[AName];
            }
            else
            {
                return null;
            }
        }

        public void Reset()
        {
            AnalyzePosition = 0;
            AnalyzeMax = 0;
            for (int i = 0; i < Funcs.Count; i++)
            {
                //Inc(FAnalyzeMax, TProfFunc(FFuncs[I]).InstanceCount);
                Funcs[i].Reset();
            }
            if (Root != null)
            {
                Root.Reset();
            }
        }

        public void ReAnalyze()
        {
            Reset();
            // analyze instances
            if (Root != null)
            {
                Root.Analyze();
            }
            // analyze functions (a bit later)
            for (int i = 0; i < Funcs.Count; i++)
            {
                Funcs[i].Analyze();
            }
        }

        private string FormatInst(TProfInstance Inst)
        {
            return Inst.Name + " (" + Inst.FileName + ":" + Inst.ParserLine.ToString() + ")"
                + " Cum:" + String.Format("%.0f", (Inst.CumTime * 10000)) + " Self:" + String.Format("%.0f", (Inst.SelfTime * 10000));
        }

        private string Uncompress(string section, string raw)
        {
            string id;
            string key;

            /*
             * 
                var
                  id: string;
                key: string;
                _content: PString;
                
                  with TRegExpr.Create do try
    // check if this is an assignment of compression
    Expression:= '\((\d+)\)\s*(.*)';
                if Exec(raw) then begin
                      id:= Match[1];
                key:= section + '-' + id;
                if Match[2] <> '' then begin
                        // this is an assignment
        New(_content);
                _content ^ := Match[2];
                Compresseds.Add(key, _content);
                Result:= Match[2];
                end else begin
                  // this is an uncompression
                  Result := PString(Compresseds.Data[key]) ^;
                end;
                end else begin
                  // otherwise just return as-is
                  */
            return raw;
            /*
            end;
            */
        }

        public TProfInstance CreateInstance(string AName, string AFileName)
        {
            TProfFunc Func;
            TProfInstance Inst;
            Func = FindFunc(AName);
            if (Func == null)
            {
                Func = new TProfFunc(this, AName, AFileName);
                this.Funcs.Add(Func);
                this.Map.Add(Func.Name, Func);
            }
            Inst = Func.AddInstance();
            return Inst;
        }

        public void Load(string AFileName, bool HeaderOnly)
        {
            string State;// { stHeader, stEvents, stBody, stDone };
            System.IO.StreamReader F;
            string S, CurFL, CurFN, A, B;
            TProfInstance CurInst, LastInst;
            List<TProfInstance> Stack;
            List<PCallBuffer> Buffer;
            int P, ParserLine;
            Dictionary<string, string> Compresseds;




            /*
            procedure ClearBuffer;
            var
              I: Integer;
            begin
for I := 0 to Buffer.Count - 1 do
                    Dispose(PCallBuffer(Buffer[I]));
Buffer.Clear;
            end;

            function FreeHashData(AUserData: Pointer; AStr: string; var APtr: PString): Boolean;
            begin
Dispose(APtr);
            Result:= True;
            end;
        */
            PCallBuffer CurBuf;
            TProfInstance Target;
            int TargetIndex;
            Array textBuf;  //TextBuf: array[0..65535] of Char;
            F = new System.IO.StreamReader(AFileName);
            System.IO.FileInfo file = new System.IO.FileInfo(AFileName);
            State = "stHeader";
            CurFL = "";
            CurFN = "";
            CurInst = null;
            LastInst = null;
            CurBuf = null;
            this.SummaryExists = false;
            Stack = new List<TProfInstance>();
            Buffer = new List<PCallBuffer>();
            Compresseds = new Dictionary<string, string>();
            ParserLine = 1;

            try
            {
                while ((S = F.ReadLine()) != null)
                {
                    switch (State)
                    {
                        case "stHeader":
                            {
                                if (S.Length > 9 && S.Substring(0, 9) == "version: ")
                                {
                                    Version = S.Substring(9, S.Length - 9);
                                }
                                else if (S.Length > 5 && S.Substring(0, 5) == "cmd: ")
                                {
                                    Cmd = S.Substring(5, S.Length - 5);
                                }
                                else if (S == "")
                                {
                                    if (HeaderOnly)
                                    {
                                        State = "stDone";
                                    }
                                    else
                                    {
                                        State = "stEvents";
                                    }
                                }
                                break;
                            }
                        case "stEvents":
                            {
                                if (S == "")
                                {
                                    State = "stBody";
                                }
                                break;
                            }
                        case "stBody":
                            {
                                if (S.Length > 3 && S.Substring(0, 3) == "fl=")
                                {
                                    CurFL = Uncompress("fl", S.Substring(3, S.Length - 3));
                                }
                                else if (S.Length > 3 && S.Substring(0, 3) == "fn=")
                                {
                                    CurFN = Uncompress("fn", S.Substring(3, S.Length - 3));
                                    if (CurFL == "") throw new Exception("Parser error: fl is not valid.");
                                    if (CurFN == "") throw new Exception("Parser error: fn is not valid.");
                                    CurInst = CreateInstance(CurFN, CurFL); // TODO: SLOW!
                                    CurInst.ParserLine = ParserLine;
                                    Stack.Add(CurInst);
                                }
                                else if ((S.Length >= 3) && (System.Text.RegularExpressions.Regex.IsMatch(S[1].ToString(), "[0-9]")))
                                {
                                    P = S.IndexOf(" ");
                                    if (P <= 0) throw new Exception("Parser error: Invalid <line> <time> <???> statement.");
                                    A = S.Substring(0, P);
                                    B = S.Substring(P+1, S.Length - (P+1));
                                    // remove the "unknown", or whatever, if exists
                                    P = B.IndexOf(" ");
                                    if (P >= 1) B = B.Substring(0, P - 1);
                                    if (CurInst == null)
                                    {
                                        if (LastInst == Main)
                                        {
                                            CurInst = Main;
                                        }
                                        else
                                        {
                                            throw new Exception("Parser error: Parsing " + S + ": Current instance is NULL.");
                                        }
                                    }
                                    if (CurBuf != null)
                                    {
                                        CurBuf.Line = Int32.Parse(A);
                                        CurBuf.CumTime = Int64.Parse(B) / 10000;
                                    }
                                    else
                                    {
                                        CurInst.SelfTime = Int64.Parse(B) / 10000;
                                    }
                                }
                                else if (S.Length > 4 && S.Substring(0, 4) == "cfn=")
                                {
                                    // must have inst first
                                    if (CurInst == null) throw new Exception("Parser error: Parsing " + S + ": Current instance is NULL.");
                                    // get function name
                                    A = Uncompress("fn", S.Substring(4, S.Length - 4));
                                    // add to call buffer
                                    CurBuf = new PCallBuffer();
                                    CurBuf.Name = A;
                                    CurBuf.Line = 0;
                                    CurBuf.CumTime = 0;
                                    CurBuf.ParserLine = ParserLine;
                                    Buffer.Add(CurBuf);
                                }
                                else if (S.Length >= 8 && S.Substring(0, 8) == "summary:")
                                {
                                    // we should have the main instance right here
                                    if (LastInst == null) throw new Exception("Parser error: LastInst should contain main function instance now.");
                                    Main = LastInst;
                                    CurInst = null;
                                    SummaryExists = true;
                                    State = "stDone";   //TODO: not sure this should go here, just breaks when trying to parse summary
                                }
                                else if (S == "")
                                {
                                    // add pending buffers
                                    // TODO: this is also the slow point
                                    while (Buffer.Count > 0)
                                    {
                                        CurBuf = Buffer[Buffer.Count - 1];
                                        Target = null;
                                        TargetIndex = -1;
                                        // we start from Count - 2 because the last item is always
                                        // the current instance, so it can't possibly be the target
                                        for (int i = Stack.Count-2; i >= 0; i--)
                                        {
                                            if (Stack[i].Name == CurBuf.Name)
                                            {
                                                Target = Stack[i];
                                                TargetIndex = i;
                                                break;
                                            }
                                        }
                                        // no target?
                                        if (Target == null) throw new Exception("Cannot find call target.");
                                        // update target's CumTime: this one is the correct one
                                        Target.CumTime = CurBuf.CumTime;
                                        // set Line as well
                                        Target.Line = CurBuf.Line;
                                        // debug info
                                        Target.ParserCallLine = CurBuf.ParserLine;
                                        // looks OK
                                        // delete this buffer
                                        Buffer.Remove(CurBuf);
                                        CurBuf = null;
                                        // insert this target at the FIRST index
                                        // since we're doing stack rollup here, we actually
                                        // have to reverse things a lot (*confusing!!!*)
                                        CurInst.InsertCall(0, Target);
                                        // remove from stack
                                        Stack.RemoveAt(TargetIndex);
                                    };
                                    // then clear, ready for next statement
                                    CurFL = "";
                                    CurFN = "";
                                    // save CurInst, it may well be the main function instance
                                    if (CurInst != null)
                                    {
                                        LastInst = CurInst;
                                    }
                                    CurInst = null;
                                }
                                break;
                            }
                    }
                    // be a bit lazy on updating
                    /*
                    if ParserLine mod 1000 = 0 then begin
                          // call OnLoadProgress
          if Assigned(OnLoadProgress) then begin
                            if FileSize(F) > 0 then
                              OnLoadProgress(Self, FilePos(F), FileSize(F),
                Format('Parsing... %d%% complete.', [FilePos(F) * 100 div FileSize(F)]));
                    end;
                    end;
                    */
                    if (ParserLine % 1000 == 0)
                    {
                        Console.WriteLine("parsed:" + ParserLine.ToString());
                        if (file.Length > 0)
                        {
                            int iProgress = (int) ((F.BaseStream.Position * 100) / file.Length);
                            UpdateProgress(iProgress, string.Format("Parsing... {0:00} complete.", iProgress));
                        }
                    }
                    // post
                    ParserLine++;
                    // done?
                    if (State == "stDone")
                    {
                        break;
                    }
                }
                if (!HeaderOnly)
                {
                    // at this point buffer MUST be empty
                    if (Buffer.Count != 0) throw new Exception("Call buffer is not empty.");
                    // when we reach here the stack should contain only sections
                    // like the main function instance and exit procedures
                    if (Stack.Count < 1) throw new Exception("Parser error: At this point at least main instance is expected.");
                    //Root = new TProfInstance(Cmd, Cmd);
                    Root = CreateInstance("main", AFileName);
                    Root.Func.Kind = TFuncKind.fkRoot;
                    for (int i = Stack.Count - 1; i > -1; i--)
                    {
                        CurInst = Stack[i];

                        // they're sections
                        CurInst.Func.Kind = TFuncKind.fkSection;
                        // add this
                        Root.InsertCall(0, CurInst);
                    }
                    // bye stack
                    Stack.Clear();
                }

            }
            catch (Exception err)
            {

                string Msg = err.Message;

                //string S;
                //int I;
                TProfInstance Cur;
                PCallBuffer CB;


                S = "\r\n" + Msg;
                S += "\r\n" + "cachegrind.out line number: " + ParserLine.ToString();
                if (CurInst == null)
                {
                    S = S + "\r\n" + "CurInst: NULL";
                }
                else
                {
                    S = S + "\r\n" + "CurInst: " + FormatInst(CurInst);
                }

                if (Stack.Count == 0)
                {
                    S = S + "\r\n" + "Stack: empty";
                }
                else
                {
                    for (int i = 0; i < Stack.Count - 1; i++)
                    {
                        Cur = Stack[i];
                        S = S + "\r\n" + "Stack[" + i.ToString() + "]: " + FormatInst(Cur);
                    }
                }

                if (Buffer.Count == 0)
                {
                    S = S + "\r\n" + "Call buffer: empty";
                }
                else
                {
                    for (int i = 0; i < Buffer.Count - 1; i++)
                    {
                        CB = Buffer[i];
                        S = S + "\r\n" + "Call buffer[" + i.ToString() + "]: " + CB.Name + " Line:" + CB.Line.ToString() + " Cum:" + String.Format("%.0f", (CB.CumTime * 10000));
                    }
                }
                S = S + "\r\n";
                Console.WriteLine(S);
            }

            finally
            {
                F.Close();
            }
        }
    }
}

