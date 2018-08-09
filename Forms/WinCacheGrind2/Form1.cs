using cacher;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinCacheGrind2
{
    public partial class Form1 : Form
    {
        private TCacheGrind cache = new TCacheGrind();
        private List<TExplorerData> ExplorerList = new List<TExplorerData>();
        public Config config = new Config();
        public ListViewColumnSorter lvExplorerColumnSorter = new ListViewColumnSorter();

        public Form1()
        {
            InitializeComponent();
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            lvExplorer.ColumnClick += LvExplorer_ColumnClick;
            lvExplorerColumnSorter.SortColumn = 2;   //3rd column: DateModified
            lvExplorerColumnSorter.Order = SortOrder.Descending;
            lvExplorer.ListViewItemSorter = lvExplorerColumnSorter;
            lvExplorer.DoubleClick += lvExplorerDblClick;
            config.HideFastFuncs = false;
            config.HideLibFuncs = false;
            config.FastThreshold = 100;
            config.TimeDisplay = TTimeDisplay.tdPercent;
            config.ShowFullPath = false;
            tabControl2.MouseUp += TabControl2_MouseUp;            
        }

        private void TabControl2_MouseUp(object sender, MouseEventArgs e)
        {
            Rectangle r = tabControl2.GetTabRect(this.tabControl2.SelectedIndex);
            Rectangle closeButton = new Rectangle(r.Left + 10, r.Top + 4, 10, 10);
            if (closeButton.Contains(e.Location))
            {
                aFileCloseExecute();
            }
        }

        private void aFileCloseExecute()
        {
            this.tabControl2.TabPages.Remove(this.tabControl2.SelectedTab);
        }

        private void aFileCloseAllExecute()
        {
            tabControl2.TabPages.Clear();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openToolStripButton_Click(sender, e);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aFileCloseAllExecute();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aFileCloseExecute();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MillisecondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _toolStripMenuItemClick("aViewMS_Click", sender, e);
        }

        private void FindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _toolStripMenuItemClick("aViewMS_Click", sender, e);
        }

        private void PercentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _toolStripMenuItemClick("aViewPercent_Click", sender, e);
        }

        private void ShowFullPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _toolStripMenuItemClick("aViewFullPath_Click", sender, e);
        }

        private void HideFastFunctionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _toolStripMenuItemClick("aViewHideFastFuncs_Click", sender, e);
        }

        private void HideLibraryFunctionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _toolStripMenuItemClick("aViewHideLibFuncs_Click", sender, e);
        }

        private void UpOneLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
             _toolStripMenuItemClick("aViewGoToUpOneLevel_Click", sender, e);
        }

        private void GoToRootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _toolStripMenuItemClick("aTreeGoToRoot_Click", sender, e);
        }

        private void ToogleExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvExplorer.Visible = !lvExplorer.Visible;
        }

        private void RefreshExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshExplorer();
        }

        private void _toolStripMenuItemClick(string _event, object sender, EventArgs e)
        {
            TabPage tp = tabControl2.SelectedTab;
            if (tp == null)
            {
            }
            else
            {
                foreach (Control control in tp.Controls)
                {
                    if (control.ToString() == "WinCacheGrind2.FDoc")
                    {
                        FDoc fDoc = (FDoc)control;
                        switch (_event)
                        {
                            case "aTreeGoToRoot_Click":
                                fDoc.aTreeGoToRoot_Click(sender, e);
                                break;
                            case "aViewGoToUpOneLevel_Click":
                                fDoc.aViewGoToUpOneLevel_Click(sender, e);
                                break;
                            case "aViewHideLibFuncs_Click":
                                fDoc.aViewHideLibFuncs_Click(sender, e);
                                break;
                            case "aViewHideFastFuncs_Click":
                                fDoc.aViewHideFastFuncs_Click(sender, e);
                                break;
                            case "aViewFullPath_Click":
                                fDoc.aViewFullPath_Click(sender, e);
                                break;
                            case "aViewPercent_Click":
                                fDoc.aViewPercent_Click(sender, e);
                                break;
                            case "aViewMS_Click":
                                fDoc.aViewMS_Click(sender, e);
                                break;
                        }
                    }
                }
            }
        }

        private void lvExplorerDblClick(object sender, EventArgs e)
        {
            Open(System.IO.Path.Combine(config.WorkingDir, lvExplorer.SelectedItems[0].Text));
        }

        private void UpdateProgress(int ProgressPercentage, string sText)  
        {
            pBar1.Value = ProgressPercentage;
        }

        private void Open(string AFileName)
        {   
            // Display the ProgressBar control.
            pBar1.Visible = true;
            // Set Minimum to 0 to represent the first file being copied.
            pBar1.Minimum = 0;
            // Set Maximum to the total number of files to copy.
            pBar1.Maximum = 100;
            // Set the initial value of the ProgressBar.
            pBar1.Value = 1;

            cache.UpdateProgress += UpdateProgress;
            cache.Load(AFileName, false);
            cache.ReAnalyze();
            config.HideFastFuncs = false;
            config.HideLibFuncs = false;
            config.FastThreshold = 100;
            config.TimeDisplay = TTimeDisplay.tdPercent;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(AFileName);
            config.WorkingDir = fileInfo.DirectoryName;
            //now that configs are set, refresh
            RefreshExplorer();

            //new stuff
            FDoc fDoc = new FDoc(cache, config);
            TabPage tp = new TabPage();
            System.IO.FileInfo fileInfo2 = new System.IO.FileInfo(cache.Cmd);
            tp.Text = fileInfo2.Name;
            tp.ImageIndex = (int)tForm1ImageIndex.imgClose;
            tp.Controls.Add(fDoc);
            this.tabControl2.TabPages.Add(tp);
            pBar1.Visible = false;
        }

        private void RefreshExplorer()
        {
            System.IO.FileInfo F;
            string[] Code;
            TCacheGrind CG;
            TExplorerData ED;
            //  sb.SimplePanel := True;
            string ApplicationHint = "Refreshing Explorer list. Please wait...";
            ClearExplorer();
            if (config.WorkingDir != string.Empty)
            {
                CG = new TCacheGrind();

                Code = System.IO.Directory.GetFiles(config.WorkingDir, @"cachegrind.out.*");
                foreach (string dir in Code)
                {
                    F = new System.IO.FileInfo(dir);
                    ED = new TExplorerData();
                    ED.FileName = config.WorkingDir + @"\" + F.Name;
                    ED.Modified = F.LastWriteTime;
                    ED.Size = F.Length;
                    try
                    {
                        CG.Load(config.WorkingDir + @"\" + F.Name, true);
                        ED.Title = CG.Cmd;
                    }
                    catch (Exception)
                    {
                        ED.Title = "(Error: Cannot read file)";
                    }
                    ExplorerList.Add(ED);
                }

                // update list
                lvExplorerInvalidate();
                // register change notify
                /*TODO: do we need this?
                cn.Active := False;
                cn.Notifications[0].Directory := Config.WorkingDir;
                cn.Active := True;
                */
            }
            ApplicationHint = "";
        }

        private void ClearExplorer()
        {
            ExplorerList.Clear();
            lvExplorer.Items.Clear();
        }     

        private void lvExplorerInvalidate()
        {
            lvExplorer.Items.Clear();
            lvExplorerData();
        }

        private void lvExplorerData()
        {
            foreach (TExplorerData ED in ExplorerList)
            {
                ListViewItem Item = new ListViewItem();
                Item.Text = System.IO.Path.GetFileName(ED.FileName);
                Item.SubItems.Add(ED.Title);
                Item.SubItems.Add(FormatDateTime(ED.Modified));
                Item.SubItems.Add(string.Format("{0:0}", (ED.Size * 1.0)));
                Item.ImageIndex = 0;
                /* TODO: add this in
                for I := 0 to MDIChildCount - 1 do begin
      if MDIChildren[I] is TfDoc then
        if SameText((MDIChildren[I] as TfDoc).FileName, ED.FileName) then begin
          Item.ImageIndex := 1;
                Break;
                end;
                */
                lvExplorer.Items.Add(Item);
            }
        }

        private string FormatDateTime(DateTime dt)
        {
            string Result = string.Empty;
            if (dt == DateTime.MinValue || dt == DateTime.MaxValue)
            {
                Result = "-";
            }
            else
            {
                Result = dt.ToShortDateString() + " " + dt.ToShortTimeString();
            }
            return Result;
        }       

        private void LvExplorer_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvExplorerColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvExplorerColumnSorter.Order == SortOrder.Ascending)
                {
                    lvExplorerColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvExplorerColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvExplorerColumnSorter.SortColumn = e.Column;
                lvExplorerColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvExplorer.Sort();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Open(openFileDialog1.FileName);
            }
        }
    }

    public class Config
    {
        public bool ClearMRUOnExit;
        public int FastThreshold;
        public bool HideFastFuncs;
        public bool HideLibFuncs;
        public int MaxMRUCount;
        public List<string> MRU;
        public List<string> MRUTitles;
        public TTimeDisplay TimeDisplay;
        public bool TrackMRU;
        public bool ShowFullPath;
        public string WorkingDir;
        public string EditorPath;
    }

    public enum TTimeDisplay
    {
        tdMs, tdPercent
    };

    public class TExplorerData
    {
        public string FileName;
        public string Title;
        public DateTime Modified;
        public long Size;
    }

    public enum tForm1ImageIndex
    {
        imgClose
    };
}
