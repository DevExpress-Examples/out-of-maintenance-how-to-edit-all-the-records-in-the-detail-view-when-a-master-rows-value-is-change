using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;


namespace WindowsApplication1
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(480, 270);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.Text = "gridControl1";
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.ShownEditor += new System.EventHandler(this.gridView1_ShownEditor);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(480, 270);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.gridControl1});
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e) {
            DataTable mTable = new DataTable();
            mTable.Columns.Add("ID", typeof(int));
            mTable.Columns.Add("check", typeof(bool));
            mTable.Columns.Add("data", typeof(string));

            DataTable dTable = new DataTable();
            dTable.Columns.Add("ID", typeof(int));
            dTable.Columns.Add("ChildID", typeof(int));
            dTable.Columns.Add("check", typeof(bool));
            dTable.Columns.Add("data", typeof(string));

            for(int i = 0; i < 10; i++)
                mTable.Rows.Add(new object[] {i, false, "Row " + i.ToString()});

            for(int i = 0; i < 100; i++)
                dTable.Rows.Add(new object[] {i, i /10, false, "Row " + i.ToString()});
            
            DataSet ds = new DataSet();
            ds.Tables.Add(mTable);
            ds.Tables.Add(dTable);
            ds.Relations.Add(new DataRelation("myRelation", mTable.Columns["ID"], dTable.Columns["ChildID"], true));
            gridControl1.DataSource = ds.Tables[0];
		}

        private void gridView1_ShownEditor(object sender, System.EventArgs e) {
            if((sender as GridView).ActiveEditor is CheckEdit) {
                CheckEdit edit = (sender as GridView).ActiveEditor as CheckEdit;
                edit.CheckedChanged += new System.EventHandler(myEdit_CheckedChanged);
            }
        }

        private void myEdit_CheckedChanged(object sender, System.EventArgs e) {
            UpdateDetailView(gridView1.FocusedRowHandle, (sender as CheckEdit).Checked);
        }

        private void UpdateDetailView(int rowHandle, bool state) {
            GridView dView = gridView1.GetDetailView(rowHandle, 0) as GridView;
            bool aCollapsed = dView == null;
            if (dView == null)
            {
                gridView1.ExpandMasterRow(rowHandle);
                dView = gridView1.GetDetailView(rowHandle, 0) as GridView;
            }
            if(dView != null)
                for(int i = 0; i < dView.DataRowCount; i++) 
                    dView.SetRowCellValue(i, dView.Columns["check"], state);
        }
	}
}
