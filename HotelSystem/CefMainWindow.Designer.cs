namespace HotelSystem
{
    partial class CefMainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.skinPanel1 = new CCWin.SkinControl.SkinPanel();
            this.CefFATabStrip = new FarsiLibrary.Win.FATabStrip();
            this.CefFaTabStripItem = new FarsiLibrary.Win.FATabStripItem();
            this.tabStripAdd = new FarsiLibrary.Win.FATabStripItem();
            this.skinPanel2 = new CCWin.SkinControl.SkinPanel();
            this.xcButton = new CCWin.SkinControl.SkinButton();
            this.TxtURL = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cookie = new CCWin.SkinControl.SkinButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.skinPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CefFATabStrip)).BeginInit();
            this.CefFATabStrip.SuspendLayout();
            this.skinPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.skinPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.skinPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.27586F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.72414F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1256, 725);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // skinPanel1
            // 
            this.skinPanel1.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel1.Controls.Add(this.CefFATabStrip);
            this.skinPanel1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinPanel1.DownBack = null;
            this.skinPanel1.Location = new System.Drawing.Point(3, 91);
            this.skinPanel1.MouseBack = null;
            this.skinPanel1.Name = "skinPanel1";
            this.skinPanel1.NormlBack = null;
            this.skinPanel1.Size = new System.Drawing.Size(1250, 631);
            this.skinPanel1.TabIndex = 0;
            // 
            // CefFATabStrip
            // 
            this.CefFATabStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CefFATabStrip.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CefFATabStrip.Items.AddRange(new FarsiLibrary.Win.FATabStripItem[] {
            this.CefFaTabStripItem,
            this.tabStripAdd});
            this.CefFATabStrip.Location = new System.Drawing.Point(0, 0);
            this.CefFATabStrip.Name = "CefFATabStrip";
            this.CefFATabStrip.SelectedItem = this.CefFaTabStripItem;
            this.CefFATabStrip.Size = new System.Drawing.Size(1250, 631);
            this.CefFATabStrip.TabIndex = 5;
            this.CefFATabStrip.Text = "faTabStrip1";
            this.CefFATabStrip.TabStripItemSelectionChanged += new FarsiLibrary.Win.TabStripItemChangedHandler(this.OnTabsChanged);
            // 
            // CefFaTabStripItem
            // 
            this.CefFaTabStripItem.IsDrawn = true;
            this.CefFaTabStripItem.Name = "CefFaTabStripItem";
            this.CefFaTabStripItem.Selected = true;
            this.CefFaTabStripItem.Size = new System.Drawing.Size(1248, 601);
            this.CefFaTabStripItem.TabIndex = 0;
            this.CefFaTabStripItem.Title = "Loading...";
            // 
            // tabStripAdd
            // 
            this.tabStripAdd.CanClose = false;
            this.tabStripAdd.IsDrawn = true;
            this.tabStripAdd.Name = "tabStripAdd";
            this.tabStripAdd.Size = new System.Drawing.Size(932, 591);
            this.tabStripAdd.TabIndex = 1;
            this.tabStripAdd.Title = "+";
            // 
            // skinPanel2
            // 
            this.skinPanel2.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel2.Controls.Add(this.cookie);
            this.skinPanel2.Controls.Add(this.xcButton);
            this.skinPanel2.Controls.Add(this.TxtURL);
            this.skinPanel2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinPanel2.DownBack = null;
            this.skinPanel2.Location = new System.Drawing.Point(3, 3);
            this.skinPanel2.MouseBack = null;
            this.skinPanel2.Name = "skinPanel2";
            this.skinPanel2.NormlBack = null;
            this.skinPanel2.Size = new System.Drawing.Size(1250, 82);
            this.skinPanel2.TabIndex = 1;
            // 
            // xcButton
            // 
            this.xcButton.BackColor = System.Drawing.Color.Transparent;
            this.xcButton.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.xcButton.DownBack = null;
            this.xcButton.Location = new System.Drawing.Point(3, 30);
            this.xcButton.MouseBack = null;
            this.xcButton.Name = "xcButton";
            this.xcButton.NormlBack = null;
            this.xcButton.Size = new System.Drawing.Size(75, 23);
            this.xcButton.TabIndex = 1;
            this.xcButton.Text = "携程";
            this.xcButton.UseVisualStyleBackColor = false;
            this.xcButton.Click += new System.EventHandler(this.xcButton_Click);
            // 
            // TxtURL
            // 
            this.TxtURL.Location = new System.Drawing.Point(0, 3);
            this.TxtURL.Name = "TxtURL";
            this.TxtURL.Size = new System.Drawing.Size(692, 21);
            this.TxtURL.TabIndex = 0;
            this.TxtURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtURL_KeyDown);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cookie
            // 
            this.cookie.BackColor = System.Drawing.Color.Transparent;
            this.cookie.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.cookie.DownBack = null;
            this.cookie.Location = new System.Drawing.Point(3, 56);
            this.cookie.MouseBack = null;
            this.cookie.Name = "cookie";
            this.cookie.NormlBack = null;
            this.cookie.Size = new System.Drawing.Size(75, 23);
            this.cookie.TabIndex = 2;
            this.cookie.Text = "Cookie";
            this.cookie.UseVisualStyleBackColor = false;
            this.cookie.Click += new System.EventHandler(this.cookie_Click);
            // 
            // CefMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 761);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CefMainWindow";
            this.Text = "CefMainWindow";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.skinPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CefFATabStrip)).EndInit();
            this.CefFATabStrip.ResumeLayout(false);
            this.skinPanel2.ResumeLayout(false);
            this.skinPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CCWin.SkinControl.SkinPanel skinPanel1;
        private CCWin.SkinControl.SkinPanel skinPanel2;
        private FarsiLibrary.Win.FATabStrip CefFATabStrip;
        private FarsiLibrary.Win.FATabStripItem CefFaTabStripItem;
        private FarsiLibrary.Win.FATabStripItem tabStripAdd;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox TxtURL;
        private CCWin.SkinControl.SkinButton xcButton;
        private CCWin.SkinControl.SkinButton cookie;
    }
}

