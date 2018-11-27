namespace LibraryTestApp
{
    partial class AppStartForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_TcpClientStart = new System.Windows.Forms.Button();
            this.btn_TcpServerStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_TcpClientStart
            // 
            this.btn_TcpClientStart.Location = new System.Drawing.Point(12, 55);
            this.btn_TcpClientStart.Name = "btn_TcpClientStart";
            this.btn_TcpClientStart.Size = new System.Drawing.Size(439, 37);
            this.btn_TcpClientStart.TabIndex = 0;
            this.btn_TcpClientStart.Text = "TCP Client";
            this.btn_TcpClientStart.UseVisualStyleBackColor = true;
            this.btn_TcpClientStart.Click += new System.EventHandler(this.btn_TcpClientStart_Click);
            // 
            // btn_TcpServerStart
            // 
            this.btn_TcpServerStart.Location = new System.Drawing.Point(12, 12);
            this.btn_TcpServerStart.Name = "btn_TcpServerStart";
            this.btn_TcpServerStart.Size = new System.Drawing.Size(439, 37);
            this.btn_TcpServerStart.TabIndex = 1;
            this.btn_TcpServerStart.Text = "TCP Server";
            this.btn_TcpServerStart.UseVisualStyleBackColor = true;
            this.btn_TcpServerStart.Click += new System.EventHandler(this.btn_TcpServerStart_Click);
            // 
            // AppStartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 335);
            this.Controls.Add(this.btn_TcpServerStart);
            this.Controls.Add(this.btn_TcpClientStart);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppStartForm";
            this.ShowIcon = false;
            this.Text = "Daljin Library Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_TcpClientStart;
        private System.Windows.Forms.Button btn_TcpServerStart;
    }
}

