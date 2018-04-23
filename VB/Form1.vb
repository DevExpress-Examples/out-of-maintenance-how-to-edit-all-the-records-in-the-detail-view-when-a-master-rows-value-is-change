Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid


Namespace WindowsApplication1
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class Form1
		Inherits System.Windows.Forms.Form
		Private gridControl1 As DevExpress.XtraGrid.GridControl
		Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
		Private components As System.ComponentModel.IContainer

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()

			'
			' TODO: Add any constructor code after InitializeComponent call
			'
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridControl1
			' 
			Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.gridControl1.MainView = Me.gridView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.Size = New System.Drawing.Size(480, 270)
			Me.gridControl1.TabIndex = 0
			Me.gridControl1.Text = "gridControl1"
			' 
			' gridView1
			' 
			Me.gridView1.GridControl = Me.gridControl1
			Me.gridView1.Name = "gridView1"
'			Me.gridView1.ShownEditor += New System.EventHandler(Me.gridView1_ShownEditor);
			' 
			' Form1
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(480, 270)
			Me.Controls.AddRange(New System.Windows.Forms.Control() { Me.gridControl1})
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
		#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread> _
		Shared Sub Main()
			Application.Run(New Form1())
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
			Dim mTable As New DataTable()
			mTable.Columns.Add("ID", GetType(Integer))
			mTable.Columns.Add("check", GetType(Boolean))
			mTable.Columns.Add("data", GetType(String))

			Dim dTable As New DataTable()
			dTable.Columns.Add("ID", GetType(Integer))
			dTable.Columns.Add("ChildID", GetType(Integer))
			dTable.Columns.Add("check", GetType(Boolean))
			dTable.Columns.Add("data", GetType(String))

			For i As Integer = 0 To 9
				mTable.Rows.Add(New Object() {i, False, "Row " & i.ToString()})
			Next i

			For i As Integer = 0 To 99
				dTable.Rows.Add(New Object() {i, i \10, False, "Row " & i.ToString()})
			Next i

			Dim ds As New DataSet()
			ds.Tables.Add(mTable)
			ds.Tables.Add(dTable)
			ds.Relations.Add(New DataRelation("myRelation", mTable.Columns("ID"), dTable.Columns("ChildID"), True))
			gridControl1.DataSource = ds.Tables(0)
		End Sub

		Private Sub gridView1_ShownEditor(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridView1.ShownEditor
			If TypeOf (TryCast(sender, GridView)).ActiveEditor Is CheckEdit Then
				Dim edit As CheckEdit = TryCast((TryCast(sender, GridView)).ActiveEditor, CheckEdit)
				AddHandler edit.CheckedChanged, AddressOf myEdit_CheckedChanged
			End If
		End Sub

		Private Sub myEdit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
			UpdateDetailView(gridView1.FocusedRowHandle, (TryCast(sender, CheckEdit)).Checked)
		End Sub

		Private Sub UpdateDetailView(ByVal rowHandle As Integer, ByVal state As Boolean)
			Dim dView As GridView = TryCast(gridView1.GetDetailView(rowHandle, 0), GridView)
			Dim aCollapsed As Boolean = dView Is Nothing
			If dView Is Nothing Then
				gridView1.ExpandMasterRow(rowHandle)
				dView = TryCast(gridView1.GetDetailView(rowHandle, 0), GridView)
			End If
			If dView IsNot Nothing Then
				For i As Integer = 0 To dView.DataRowCount - 1
					dView.SetRowCellValue(i, dView.Columns("check"), state)
				Next i
			End If
		End Sub
	End Class
End Namespace
