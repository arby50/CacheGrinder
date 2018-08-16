using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using cacher;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace WinCacheGrind2
{
    public partial class FDoc : UserControl
    {
        private TCacheGrind cache = new TCacheGrind();
        private List<TProfInstance> FListLBL = new List<TProfInstance>();
        private List<TProfFunc> FListMerged = new List<TProfFunc>();
        private List<TProfInstance> FMergedInstances = new List<TProfInstance>();
        public ListViewColumnSorter lvLBLColumnSorter = new ListViewColumnSorter();
        public ListViewColumnSorter lvMergedColumnSorter = new ListViewColumnSorter();
        public ListViewColumnSorter lvMergedInstancesColumnSorter = new ListViewColumnSorter();
        public Config config = new Config();

        public FDoc(TCacheGrind _cache, Config _config)
        {
            this.cache = _cache;
            this.config = _config;
            InitializeComponent();
            tv.NodeMouseClick += Tv_NodeMouseClick;
            //set events for listviews
            lvLBL.ColumnClick += LvLBL_ColumnClick;
            lvLBLColumnSorter.SortColumn = 2;  //3rd column: Cum
            lvLBLColumnSorter.Order = SortOrder.Descending;
            lvLBL.ListViewItemSorter = lvLBLColumnSorter;
            lvMerged.ColumnClick += LvMerged_ColumnClick;
            lvMergedColumnSorter.SortColumn = 4;   //5th column: TotalCum
            lvMergedColumnSorter.Order = SortOrder.Descending;
            lvMerged.ListViewItemSorter = lvMergedColumnSorter;
            lvMergedInstances.ColumnClick += LvMergedInstances_ColumnClick;
            lvMergedInstancesColumnSorter.SortColumn = 0;   //1st column: num
            lvMergedInstancesColumnSorter.Order = SortOrder.Ascending;
            lvMergedInstances.ListViewItemSorter = lvMergedInstancesColumnSorter;
            cbRE.Click += cbREClick;
            cbFind.SelectedIndexChanged += CbFind_SelectedIndexChanged;
            cbFind.TextChanged += CbFind_TextChanged;
            //imagelist for tb actionbar
            tb.ImageList = this.imageListtb;

            //seed the treeview with a parent node            
            SyncTree();
            tv.SelectedNode = tv.Nodes[0].FirstNode;
            Tv_NodeMouseClick(null, new TreeNodeMouseClickEventArgs(tv.Nodes[0], new MouseButtons(), 0, 0, 0));
        }

        private void CbFind_TextChanged(object sender, EventArgs e)
        {
            TProfInstance Inst = null;
            FindInst(tv.SelectedNode.Text, cache.Root, ref Inst);
            RefreshListMerged(Inst);
        }

        private void CbFind_SelectedIndexChanged(object sender, EventArgs e)
        {
            CbFind_TextChanged(sender, e);
        }

        private void cbREClick(object sender, EventArgs e)
        {
            CbFind_TextChanged(sender, e);
        }

        private void Tv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tv.Focus();
            tv.Select();
            TreeNode node = e.Node;
            if (node.Parent == null)
            {
                tv.SelectedNode = node.FirstNode;
                node.SelectedImageIndex = node.ImageIndex;
                node = node.FirstNode; //the parent is a placeholder and has no Inst so will error
            }
            node.SelectedImageIndex = node.ImageIndex;
            TProfInstance Inst = null;
            FindInst(node.Text, cache.Root, ref Inst);
            if (Inst != null)
            {
                lInfoName.Text = Inst.Name;
                lInfoFileName.Text = "File: " + Inst.FileName;
                lInfo.Text = "Self time: " + Inst.SelfTime.ToString() + " (" + FormatPercent(Inst.SelfPercent) + ")"
                        + "   Cumulative time: " + Inst.CumTime.ToString() + " (" + FormatPercent(Inst.CumPercent) + ")";

                RefreshLists(Inst);
            }
        }

        private void RefreshLists(TProfInstance Inst)
        {
            RefreshListLBL(Inst);
            RefreshListMerged(Inst);
            RefreshListMergedInstances(Inst);
        }


        private void RefreshListLBL(TProfInstance Parent)
        {
            TProfInstance Inst;
            string LastInst;
            bool Pass = true;
            LastInst = string.Empty;
            if (lvLBL.SelectedItems.Count > 0)
            {
                LastInst = lvLBL.SelectedItems[0].Name;
            }
            FListLBL.Clear();
            for (int i = 0; i < Parent.CallCount - 1; i++)
            {
                Inst = Parent.Calls[i];
                Pass = true;
                if (config.HideLibFuncs && (Inst.Kind == TFuncKind.fkLibFunc))
                {
                    Pass = false;
                }
                else if ((config.HideFastFuncs) && (Inst.CumTime < config.FastThreshold))
                {
                    Pass = false;
                }
                if (Pass)
                {
                    FListLBL.Add(Inst);//FlistLBL needs to be a datasource
                }
            }
            //TODO: sort functions
            /*
            if (ListLBLSort <> lsLine)
            {
                FListLBL.Sort(LBLSort);
            }
            */
            //lvLBL.Items.Count = FListLBL.Count;
            //SelectListItem(lvLBL, 0);
            SelectLBLInstance(LastInst);
            lvLBLInvalidate();   //rebind            
        }

        private void ExportListMerged()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);

            foreach (TProfFunc func in FListMerged)
            {
                XmlNode newBook = doc.ImportNode(SerializeToXmlElement(func), true);
                doc.DocumentElement.AppendChild(newBook);
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML Document|*.xml";
            saveFileDialog1.Title = "Save an XML File";
            saveFileDialog1.FileName = string.Concat(cache.Root.ShortFileName, ".xml");
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.  
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                doc.Save(fs);
                fs.Close();
            }
        
    }

        public XmlElement SerializeToXmlElement(object o)
        {
            XmlDocument doc = new XmlDocument();
            XmlAttributeOverrides overrides = new XmlAttributeOverrides();
            XmlAttributes attribs = new XmlAttributes();
            attribs.XmlIgnore = true;
            attribs.XmlElements.Add(new XmlElementAttribute("YourElementName"));
            overrides.Add(typeof(TProfFunc), "CacheGrind", attribs);
            overrides.Add(typeof(TProfFunc), "Instances", attribs);
            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                new XmlSerializer(o.GetType(), overrides).Serialize(writer, o);
            }

            return doc.DocumentElement;
        }

        private void RefreshListMerged(TProfInstance Parent)
        {
            TProfFunc Func;
            string LastInst;
            string Q;
            bool Delete;
            System.Text.RegularExpressions.Regex RE = null;
            double SumSelf;
            double SumSelfPercent;
            int SumCalls;
            //lFind.Caption := '';  //TODO: set the text/caoption
            LastInst = string.Empty;
            if (lvMerged.SelectedItems.Count > 0)
            {
                LastInst = lvMerged.SelectedItems[0].Name;
            }

            FListMerged.Clear();
            SumSelf = 0;
            SumSelfPercent = 0;
            SumCalls = 0;
            Parent.GetMerged(ref FListMerged);
            // filter

            if (cbRE.Checked)
            {
                Q = cbFind.Text;
                if (!String.IsNullOrEmpty(Q))
                {
                    try
                    {
                        RE = new System.Text.RegularExpressions.Regex(Q, RegexOptions.IgnoreCase);
                    }
                    catch (Exception)
                    {
                        Q = string.Empty;
                        //TODO: show proper error in lFind
                        //lFind.Caption := 'Pattern invalid';
                        //lFind.Visible := True;
                    }
                }
            }
            else
            {
                Q = cbFind.Text.Trim().ToLower();
            }
            for (int i = FListMerged.Count - 1; i > -1; i--)
            {
                Func = FListMerged[i];
                // filtering
                Delete = false;
                // filter for hide funcs
                if (config.HideLibFuncs && (Func.Kind == TFuncKind.fkLibFunc))
                {
                    Delete = true;
                }
                // hide fast funcs
                else if (config.HideFastFuncs && (Func.TotCumTime < config.FastThreshold))
                {
                    Delete = true;
                }
                // find
                else if (Q != "")
                {
                    if (cbRE.Checked)
                    {
                        Match m = RE.Match(Func.Name);
                        if (!m.Success)
                        {
                            Delete = true;
                        }
                    }
                    else
                    {
                        if (Func.Name.ToLower().IndexOf(Q) <= 0)
                        {
                            Delete = true;
                        }
                    }
                }
                if (Delete)
                {
                    FListMerged.RemoveAt(i);
                }
                else
                {
                    // calculate sum
                    SumSelf = SumSelf + Func.TotSelfTime;
                    SumSelfPercent = SumSelfPercent + Func.TotSelfPercent;
                    SumCalls = SumCalls + Func.InstanceCount;
                }
            }

            // ok, continue
            //FListMerged.Sort(MergedSort); //TODO: sort
            //lvMerged.Items.Count := FListMerged.Count;
            // select last function if available
            //SelectListItem(lvMerged, 0);
            SelectMergedFunc(LastInst);
            lvMergedInvalidate();
            lMerged.Text = " Sum of total self time: " + FormatMs(SumSelf) + " (" + FormatPercent(SumSelfPercent) + ")"
              + "   Sum of calls: " + string.Format("{0:0}", (SumCalls * 1.0));
        }

        private void RefreshListMergedInstances(TProfInstance Parent)
        {
            TProfFunc Func = Parent.Func;

            FMergedInstances.Clear();// ClearListMergedInstances;
            if (lvMergedInstances.SelectedItems.Count > 0)
            {
                Func = (TProfFunc)FMergedInstances[lvMergedInstances.SelectedItems[0].Index].Func;
            }
            for (int i = 0; i < Func.InstanceCount; i++)
            {
                FMergedInstances.Add(Func.Instances[i]);
            }

            lvMergedInstancesInvalidate();
        }

        private void lvMergedInvalidate()
        {
            lvMerged.Items.Clear();
            lvMergedData();
        }

        private void lvMergedInstancesInvalidate()
        {
            lvMergedInstances.Items.Clear();
            lvMergedInstancesData();
        }


        private void SelectMergedFunc(string AInst)
        {
            for (int i = 0; i < lvMerged.Items.Count; i++)
            {
                if (lvMerged.Items[i].Name == AInst)
                {
                    lvMerged.Items[i].Selected = true;
                }
            }
        }

        private void SelectLBLInstance(string AInst)
        {
            for (int i = 0; i < lvLBL.Items.Count; i++)
            {
                if (lvLBL.Items[i].Name == AInst)
                {
                    lvLBL.Items[i].Selected = true;
                }
            }
        }

        private void lvLBLInvalidate()
        {
            lvLBL.Items.Clear();
            lvLBLData();
        }

        private void lvLBLData()
        {
            bool UseShortName = !config.ShowFullPath;
            string S = string.Empty;
            foreach (TProfInstance Inst in FListLBL)
            {
                ListViewItem Item = new ListViewItem();
                if (UseShortName)
                {
                    Item.Text = (Inst.ShortName);
                }
                else
                {
                    Item.Text = Inst.Name;
                }
                switch (config.TimeDisplay)
                {
                    case (TTimeDisplay.tdMs):
                        {
                            Item.SubItems.Add(FormatMs(Inst.SelfTime));
                            Item.SubItems.Add(FormatMs(Inst.CumTime));
                            break;
                        }
                    case (TTimeDisplay.tdPercent):
                        {
                            Item.SubItems.Add(FormatPercent(Inst.SelfPercent));
                            Item.SubItems.Add(FormatPercent(Inst.CumPercent));
                            break;
                        }
                    default:
                        {
                            throw new Exception("Unknown time display option.");
                        }
                }

                if (UseShortName)
                {
                    S = Inst.ShortFileName;
                }
                else
                {
                    S = Inst.FileName;
                }
                Item.SubItems.Add(S);
                if (UseShortName)
                {
                    S = Inst.Caller.ShortFileName;
                }
                else
                {
                    S = Inst.Caller.FileName;
                }
                // Inst.Line is the line number of the *CALLING* function
                if (Inst.Line > 0)
                {
                    S = S + " (" + string.Format("{0:0}", (Inst.Line * 1.0)) + ")";
                }
                Item.SubItems.Add(S);
                // TODO: set image
                Item.ImageIndex = GetImageIndex(Inst.Kind);
                lvLBL.Items.Add(Item);
            }
        }
        private int GetImageIndex(TFuncKind Kind)
        {
            return (int)Kind;
        }

        private void lvMergedData()
        {
            bool UseShortName = !config.ShowFullPath;
            foreach (TProfFunc Func in FListMerged)
            {
                ListViewItem Item = new ListViewItem();
                //Item.Data := Func;
                if (UseShortName)
                {
                    Item.Text = Func.ShortName;
                }
                else
                {
                    Item.Text = Func.Name;
                }

                switch (config.TimeDisplay)
                {
                    case (TTimeDisplay.tdMs):
                        {
                            Item.SubItems.Add(FormatMs(Func.AvgSelfTime));
                            Item.SubItems.Add(FormatMs(Func.AvgCumTime));
                            Item.SubItems.Add(FormatMs(Func.TotSelfTime));
                            Item.SubItems.Add(FormatMs(Func.TotCumTime));
                            break;
                        }
                    case (TTimeDisplay.tdPercent):
                        {
                            Item.SubItems.Add(FormatPercent(Func.AvgSelfPercent));
                            Item.SubItems.Add(FormatPercent(Func.AvgCumPercent));
                            Item.SubItems.Add(FormatPercent(Func.TotSelfPercent));
                            Item.SubItems.Add(FormatPercent(Func.TotCumPercent));
                            break;
                        }
                }
                Item.SubItems.Add(string.Format("{0:0}", (Func.InstanceCount * 1.0)));
                // set image
                Item.ImageIndex = GetImageIndex(Func.Kind);
                lvMerged.Items.Add(Item);
            }
        }


        private void lvMergedInstancesData()
        {
            bool UseShortName = !config.ShowFullPath;
            TProfInstance Cur;
            string S = string.Empty;
            foreach (TProfInstance Inst in FMergedInstances)
            {
                ListViewItem Item = new ListViewItem();
                Item.Text = string.Format("{0:0}", (Inst.Index + 1) * 1.0);
                // set image
                Item.ImageIndex = GetImageIndex(Inst.Kind);
                switch (config.TimeDisplay)
                {
                    case (TTimeDisplay.tdMs):
                        {
                            Item.SubItems.Add(FormatMs(Inst.SelfTime));
                            Item.SubItems.Add(FormatMs(Inst.CumTime));
                            break;
                        }
                    case (TTimeDisplay.tdPercent):
                        {
                            Item.SubItems.Add(FormatPercent(Inst.SelfPercent));
                            Item.SubItems.Add(FormatPercent(Inst.CumPercent));
                            break;
                        }
                    default:
                        {
                            throw new Exception("Unknown time display option.");
                        }
                }

                if (UseShortName)
                {
                    Item.SubItems.Add(Inst.Caller == null ? Inst.ShortName : Inst.Caller.ShortName);
                }
                else
                {
                    Item.SubItems.Add(Inst.Caller == null ? Inst.Name : Inst.Caller.Name);
                }
                if (UseShortName)
                {
                    S = Inst.Caller == null ? Inst.ShortFileName : Inst.Caller.ShortFileName;
                }
                else
                {
                    S = Inst.Caller == null ? Inst.FileName : Inst.Caller.FileName;
                }
                // Inst.Line is the line number of the *CALLING* function
                if (Inst.Line > 0)
                {
                    S = S + " (" + string.Format("{0:0}", Inst.Line * 1.0) + ")";
                }
                Item.SubItems.Add(S);
                // stack trace euy!!! :-P
                S = string.Empty;
                Cur = Inst.Caller;
                while (Cur != null)
                {
                    if (Cur.Caller == null)
                    {
                        break;
                    }
                    if (S != "") S = S + " « ";
                    S = S + Cur.ShortName;
                    Cur = Cur.Caller;
                }
                Item.SubItems.Add(S);
                lvMergedInstances.Items.Add(Item);
            }
        }

        private string FormatMs(double Ms)
        {
            string Result = string.Empty;
            if (Ms < 0.1)
            {
                Result = "-";
            }
            else if (Ms < 10)
            {
                Result = string.Format("{0:0.00}", Ms);
            }
            else
            {
                Result = string.Format("{0:0.00}", Ms);
            }
            return Result;
        }

        private string FormatPercent(double Percent)
        {
            string Result = string.Empty;
            if (Percent < 0.01)
            {
                Result = "-";
            }
            else
            {
                Result = string.Format("{0:P2}", (Percent / 100));
            }
            return Result;
        }

        private void FindInst(string name, TProfInstance Inst, ref TProfInstance returnInst)
        {
            if (Inst.Name == name)
            {
                returnInst = Inst;
            }
            else
            {
                foreach (TProfInstance child in Inst.Calls)
                {
                    FindInst(name, child, ref returnInst);
                }
            }

        }

        private void AddToTree(TreeNode Parent, TProfInstance Inst, bool Cascade)
        {
            bool UseShortName = !config.ShowFullPath;
            string S = string.Empty;
            if (UseShortName)
            {
                S = Inst.ShortName;
            }
            else
            {
                S = Inst.Name;
            }
            TreeNode node = new TreeNode(S);
            node.ImageIndex = (int)Inst.Kind;
            Parent.Nodes.Add(node);
            if (Cascade)
            {
                foreach (TProfInstance child in Inst.Calls)
                {
                    AddToTree(node, child, true);
                }
            }
        }

        private void SyncTree()
        {
            TreeNode nodeSelected = tv.SelectedNode;
            tv.Nodes.Clear();
            TreeNode node = new TreeNode(cache.Cmd);
            if (!config.ShowFullPath)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(cache.Cmd);
                node.Text = fileInfo.Name;
            }
            node.ImageIndex = (int)cache.Root.Kind;
            AddToTree(node, cache.Root, true);
            tv.Nodes.Add(node);
            if (nodeSelected == null)
            {
                tv.SelectedNode = tv.Nodes.Count > 0 ? tv.Nodes[0] : null;
            }
            else
            {
                tv.SelectedNode = nodeSelected;
                ExpandTreeNode(nodeSelected);
            }
            return;
            /* TODO: ported code using pascal renames each Node, which seems not to work in winforms
            tv.BeginUpdate();
            for (int i = 1; i < tv.Nodes.Count; i++)
            {
                SyncTreeNode(tv.Nodes[i]);
            }
            tv.EndUpdate();
            tv.Refresh();
            */
        }
        private void ExpandTreeNode(TreeNode node)
        {
            //TODO: doesnt work
            node.Expand();
            if (node.Parent != null) ExpandTreeNode(node.Parent);
        }

        private void SyncTreeNode(TreeNode ANode)
        {
            bool UseShortName = !config.ShowFullPath;
            TProfInstance Inst = null;
            FindInst(ANode.Text, cache.Root, ref Inst);
            if (UseShortName)
            {
                ANode.Text = Inst.ShortName;
            }
            else
            {
                ANode.Text = Inst.Name;
            }
            for (int i = 0; i < ANode.Nodes.Count; i++)
            {
                SyncTreeNode(ANode.Nodes[i]);
            }
        }

        private void LvLBL_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvLBLColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvLBLColumnSorter.Order == SortOrder.Ascending)
                {
                    lvLBLColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvLBLColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvLBLColumnSorter.SortColumn = e.Column;
                lvLBLColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvLBL.Sort();
        }

        private void LvMerged_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvMergedColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvMergedColumnSorter.Order == SortOrder.Ascending)
                {
                    lvMergedColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvMergedColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvMergedColumnSorter.SortColumn = e.Column;
                lvMergedColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvMerged.Sort();
        }

        private void LvMergedInstances_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvMergedInstancesColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvMergedInstancesColumnSorter.Order == SortOrder.Ascending)
                {
                    lvMergedInstancesColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvMergedInstancesColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvMergedInstancesColumnSorter.SortColumn = e.Column;
                lvMergedInstancesColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvMerged.Sort();
        }

        public void aViewPercent_Click(object sender, EventArgs e)
        {
            switch (config.TimeDisplay)
            {
                case (TTimeDisplay.tdMs):
                    {
                        config.TimeDisplay = TTimeDisplay.tdPercent;
                        aViewPercent.ImageIndex = (int)tActionBarImageIndex.imgPercentOff;
                        aViewMS.ImageIndex = (int)tActionBarImageIndex.imgMSOn;
                        break;
                    }
                case (TTimeDisplay.tdPercent):
                    {
                        config.TimeDisplay = TTimeDisplay.tdMs;
                        aViewPercent.ImageIndex = (int)tActionBarImageIndex.imgPercentOn;
                        aViewMS.ImageIndex = (int)tActionBarImageIndex.imgMSOff;
                        break;
                    }
                default:
                    {
                        throw new Exception("Unknown time display option.");
                    }
            }
            lvLBLInvalidate();
            lvMergedInvalidate();
            lvMergedInstancesInvalidate();
        }

        public void aViewMS_Click(object sender, EventArgs e)
        {
            aViewPercent_Click(sender, e);
        }

        public void aViewFullPath_Click(object sender, EventArgs e)
        {
            aViewFullPath.ImageIndex = config.ShowFullPath ? (int)tActionBarImageIndex.imgFileOn : (int)tActionBarImageIndex.imgFileOff;
            config.ShowFullPath = !config.ShowFullPath;
            SyncTree();
            lvLBLInvalidate();
            lvMergedInvalidate();
            lvMergedInstancesInvalidate();
        }

        public void aViewHideFastFuncs_Click(object sender, EventArgs e)
        {
            aViewHideFastFuncs.ImageIndex = config.HideFastFuncs ? (int)tActionBarImageIndex.imgHideFastOn : (int)tActionBarImageIndex.imgHideFastOff;
            config.HideFastFuncs = !config.HideFastFuncs;
            TProfInstance Inst = null;
            FindInst(tv.SelectedNode.Text, cache.Root, ref Inst);
            if (Inst != null)
            {
                RefreshLists(Inst);
            }
        }

        public void aViewHideLibFuncs_Click(object sender, EventArgs e)
        {
            aViewHideLibFuncs.ImageIndex = config.HideLibFuncs ? (int)tActionBarImageIndex.imgHideLibOn : (int)tActionBarImageIndex.imgHideLibOff;
            config.HideLibFuncs = !config.HideLibFuncs;
            TProfInstance Inst = null;
            FindInst(tv.SelectedNode.Text, cache.Root, ref Inst);
            if (Inst != null)
            {
                RefreshLists(Inst);
            }
        }

        public void aViewGoToUpOneLevel_Click(object sender, EventArgs e)
        {
            TProfInstance LastInst = null;
            TProfInstance CurrentInst = null;
            if ((tv.SelectedNode != null) && (tv.SelectedNode.Parent != null))
            {
                FindInst(tv.SelectedNode.Text, cache.Root, ref LastInst);
                tv.SelectedNode = tv.SelectedNode.Parent;
                FindInst(tv.SelectedNode.Text, cache.Root, ref CurrentInst);
                RefreshLists(CurrentInst);
                //SelectLBLInstance(LastInst);
            }
        }

        public void aTreeGoToRoot_Click(object sender, EventArgs e)
        {
            TProfInstance CurrentInst = null;
            tv.SelectedNode = tv.Nodes[0];
            FindInst(tv.SelectedNode.Text, cache.Root, ref CurrentInst);
            RefreshLists(CurrentInst);
        }

        public void Export_Click(object sender, EventArgs e)
        {
            ExportListMerged();
        }
    }
    public enum tActionBarImageIndex
    {
        imgMSOn, imgMSOff,
        imgPercentOn, imgPercentOff,
        imgFileOn, imgFileOff,
        imgHideFastOn, imgHideFastOff,
        imgHideLibOn, imgHideLibOff
    };
}
