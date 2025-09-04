namespace KeyFlexApp
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.outputLog = new System.Windows.Forms.TextBox();
            this.btnLogClear = new System.Windows.Forms.Button();
            this.checkOutputLog = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // outputLog
            // 
            this.outputLog.Enabled = false;
            this.outputLog.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.outputLog.Location = new System.Drawing.Point(12, 56);
            this.outputLog.Multiline = true;
            this.outputLog.Name = "outputLog";
            this.outputLog.Size = new System.Drawing.Size(400, 180);
            this.outputLog.TabIndex = 5;
            // 
            // btnLogClear
            // 
            this.btnLogClear.Enabled = false;
            this.btnLogClear.Font = new System.Drawing.Font("Meiryo UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnLogClear.Location = new System.Drawing.Point(328, 242);
            this.btnLogClear.Name = "btnLogClear";
            this.btnLogClear.Size = new System.Drawing.Size(75, 40);
            this.btnLogClear.TabIndex = 8;
            this.btnLogClear.Text = "クリア";
            this.btnLogClear.UseVisualStyleBackColor = true;
            this.btnLogClear.Click += new System.EventHandler(this.BtnLogClear_Click);
            // 
            // checkOutputLog
            // 
            this.checkOutputLog.AutoSize = true;
            this.checkOutputLog.Font = new System.Drawing.Font("Meiryo UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkOutputLog.Location = new System.Drawing.Point(16, 23);
            this.checkOutputLog.Name = "checkOutputLog";
            this.checkOutputLog.Size = new System.Drawing.Size(95, 27);
            this.checkOutputLog.TabIndex = 9;
            this.checkOutputLog.Text = "入力ログ";
            this.checkOutputLog.UseVisualStyleBackColor = true;
            this.checkOutputLog.CheckedChanged += new System.EventHandler(this.CheckOutputLog_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(423, 291);
            this.Controls.Add(this.checkOutputLog);
            this.Controls.Add(this.btnLogClear);
            this.Controls.Add(this.outputLog);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KeyFlexApp - キー割り当て";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox outputLog;
        private System.Windows.Forms.Button btnLogClear;
        private System.Windows.Forms.CheckBox checkOutputLog;
    }
}

