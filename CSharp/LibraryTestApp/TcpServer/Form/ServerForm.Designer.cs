namespace TcpServer
{
    partial class ServerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Port = new System.Windows.Forms.TextBox();
            this.chlb_ClientList = new System.Windows.Forms.CheckedListBox();
            this.btn_OpenClose = new System.Windows.Forms.Button();
            this.btn_Disconnect = new System.Windows.Forms.Button();
            this.tb_ReceiveMsg = new System.Windows.Forms.TextBox();
            this.tb_SendMsg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "포트";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_Port
            // 
            this.tb_Port.Location = new System.Drawing.Point(57, 10);
            this.tb_Port.Name = "tb_Port";
            this.tb_Port.Size = new System.Drawing.Size(100, 21);
            this.tb_Port.TabIndex = 1;
            // 
            // chlb_ClientList
            // 
            this.chlb_ClientList.FormattingEnabled = true;
            this.chlb_ClientList.Location = new System.Drawing.Point(15, 37);
            this.chlb_ClientList.Name = "chlb_ClientList";
            this.chlb_ClientList.Size = new System.Drawing.Size(223, 372);
            this.chlb_ClientList.TabIndex = 2;
            // 
            // btn_OpenClose
            // 
            this.btn_OpenClose.Location = new System.Drawing.Point(163, 10);
            this.btn_OpenClose.Name = "btn_OpenClose";
            this.btn_OpenClose.Size = new System.Drawing.Size(75, 21);
            this.btn_OpenClose.TabIndex = 3;
            this.btn_OpenClose.Text = "열기";
            this.btn_OpenClose.UseVisualStyleBackColor = true;
            this.btn_OpenClose.Click += new System.EventHandler(this.btn_OpenClose_Click);
            // 
            // btn_Disconnect
            // 
            this.btn_Disconnect.Location = new System.Drawing.Point(15, 413);
            this.btn_Disconnect.Name = "btn_Disconnect";
            this.btn_Disconnect.Size = new System.Drawing.Size(75, 23);
            this.btn_Disconnect.TabIndex = 4;
            this.btn_Disconnect.Text = "연결 끊기";
            this.btn_Disconnect.UseVisualStyleBackColor = true;
            this.btn_Disconnect.Click += new System.EventHandler(this.btn_Disconnect_Click);
            // 
            // tb_ReceiveMsg
            // 
            this.tb_ReceiveMsg.Location = new System.Drawing.Point(245, 9);
            this.tb_ReceiveMsg.Multiline = true;
            this.tb_ReceiveMsg.Name = "tb_ReceiveMsg";
            this.tb_ReceiveMsg.ReadOnly = true;
            this.tb_ReceiveMsg.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tb_ReceiveMsg.Size = new System.Drawing.Size(543, 400);
            this.tb_ReceiveMsg.TabIndex = 5;
            // 
            // tb_SendMsg
            // 
            this.tb_SendMsg.Location = new System.Drawing.Point(245, 415);
            this.tb_SendMsg.Name = "tb_SendMsg";
            this.tb_SendMsg.Size = new System.Drawing.Size(543, 21);
            this.tb_SendMsg.TabIndex = 6;
            this.tb_SendMsg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_SendMsg_KeyPress);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 447);
            this.Controls.Add(this.tb_SendMsg);
            this.Controls.Add(this.tb_ReceiveMsg);
            this.Controls.Add(this.btn_Disconnect);
            this.Controls.Add(this.btn_OpenClose);
            this.Controls.Add(this.chlb_ClientList);
            this.Controls.Add(this.tb_Port);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerForm";
            this.ShowIcon = false;
            this.Text = "TCP Server Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Port;
        private System.Windows.Forms.CheckedListBox chlb_ClientList;
        private System.Windows.Forms.Button btn_OpenClose;
        private System.Windows.Forms.Button btn_Disconnect;
        private System.Windows.Forms.TextBox tb_ReceiveMsg;
        private System.Windows.Forms.TextBox tb_SendMsg;
    }
}

