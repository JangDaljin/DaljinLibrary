namespace TcpClient
{
    partial class TcpClientForm
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
            this.tb_ServerIP = new System.Windows.Forms.TextBox();
            this.tb_ServerPort = new System.Windows.Forms.TextBox();
            this.lab_ServerIP = new System.Windows.Forms.Label();
            this.lab_ServerPort = new System.Windows.Forms.Label();
            this.tb_ReadMsg = new System.Windows.Forms.TextBox();
            this.tb_SendMsg = new System.Windows.Forms.TextBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_ServerIP
            // 
            this.tb_ServerIP.Location = new System.Drawing.Point(118, 6);
            this.tb_ServerIP.Name = "tb_ServerIP";
            this.tb_ServerIP.Size = new System.Drawing.Size(179, 21);
            this.tb_ServerIP.TabIndex = 0;
            // 
            // tb_ServerPort
            // 
            this.tb_ServerPort.Location = new System.Drawing.Point(118, 34);
            this.tb_ServerPort.Name = "tb_ServerPort";
            this.tb_ServerPort.Size = new System.Drawing.Size(179, 21);
            this.tb_ServerPort.TabIndex = 1;
            // 
            // lab_ServerIP
            // 
            this.lab_ServerIP.Location = new System.Drawing.Point(12, 9);
            this.lab_ServerIP.Name = "lab_ServerIP";
            this.lab_ServerIP.Size = new System.Drawing.Size(100, 23);
            this.lab_ServerIP.TabIndex = 2;
            this.lab_ServerIP.Text = "SERVER IP";
            this.lab_ServerIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lab_ServerPort
            // 
            this.lab_ServerPort.Location = new System.Drawing.Point(12, 34);
            this.lab_ServerPort.Name = "lab_ServerPort";
            this.lab_ServerPort.Size = new System.Drawing.Size(100, 23);
            this.lab_ServerPort.TabIndex = 3;
            this.lab_ServerPort.Text = "SERVER PORT";
            this.lab_ServerPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_ReadMsg
            // 
            this.tb_ReadMsg.Location = new System.Drawing.Point(338, 6);
            this.tb_ReadMsg.Multiline = true;
            this.tb_ReadMsg.Name = "tb_ReadMsg";
            this.tb_ReadMsg.ReadOnly = true;
            this.tb_ReadMsg.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tb_ReadMsg.Size = new System.Drawing.Size(450, 405);
            this.tb_ReadMsg.TabIndex = 4;
            // 
            // tb_SendMsg
            // 
            this.tb_SendMsg.Enabled = false;
            this.tb_SendMsg.Location = new System.Drawing.Point(338, 417);
            this.tb_SendMsg.Name = "tb_SendMsg";
            this.tb_SendMsg.Size = new System.Drawing.Size(450, 21);
            this.tb_SendMsg.TabIndex = 5;
            this.tb_SendMsg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_SendMsg_KeyPress);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(180, 75);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(117, 23);
            this.btn_Connect.TabIndex = 8;
            this.btn_Connect.Text = "연결";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connection_Click);
            // 
            // TcpClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.tb_SendMsg);
            this.Controls.Add(this.tb_ReadMsg);
            this.Controls.Add(this.lab_ServerPort);
            this.Controls.Add(this.lab_ServerIP);
            this.Controls.Add(this.tb_ServerPort);
            this.Controls.Add(this.tb_ServerIP);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TcpClientForm";
            this.ShowIcon = false;
            this.Text = "TCP Client Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_ServerIP;
        private System.Windows.Forms.TextBox tb_ServerPort;
        private System.Windows.Forms.Label lab_ServerIP;
        private System.Windows.Forms.Label lab_ServerPort;
        private System.Windows.Forms.TextBox tb_ReadMsg;
        private System.Windows.Forms.TextBox tb_SendMsg;
        private System.Windows.Forms.Button btn_Connect;
    }
}

