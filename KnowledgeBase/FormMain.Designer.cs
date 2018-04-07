namespace KnowledgeBase
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MenuMain = new System.Windows.Forms.MenuStrip();
            this.MenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPathToBase = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemGuide = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemBuildKnowledgeBase = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemLookResult = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.TextBoxUserText = new System.Windows.Forms.TextBox();
            this.RichTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.MenuMain.SuspendLayout();
            this.TableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuMain
            // 
            this.MenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile,
            this.MenuItemBuildKnowledgeBase,
            this.MenuItemLookResult,
            this.MenuItemExit});
            this.MenuMain.Location = new System.Drawing.Point(0, 0);
            this.MenuMain.Name = "MenuMain";
            this.MenuMain.Size = new System.Drawing.Size(525, 24);
            this.MenuMain.TabIndex = 0;
            this.MenuMain.Text = "MenuMain";
            // 
            // MenuItemFile
            // 
            this.MenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemPathToBase,
            this.MenuItemGuide});
            this.MenuItemFile.Name = "MenuItemFile";
            this.MenuItemFile.Size = new System.Drawing.Size(48, 20);
            this.MenuItemFile.Text = "Файл";
            // 
            // MenuItemPathToBase
            // 
            this.MenuItemPathToBase.Name = "MenuItemPathToBase";
            this.MenuItemPathToBase.Size = new System.Drawing.Size(178, 22);
            this.MenuItemPathToBase.Text = "Указать путь к базе";
            this.MenuItemPathToBase.Click += new System.EventHandler(this.MenuItemPathToBase_Click);
            // 
            // MenuItemGuide
            // 
            this.MenuItemGuide.Name = "MenuItemGuide";
            this.MenuItemGuide.Size = new System.Drawing.Size(178, 22);
            this.MenuItemGuide.Text = "Руководство";
            // 
            // MenuItemBuildKnowledgeBase
            // 
            this.MenuItemBuildKnowledgeBase.Name = "MenuItemBuildKnowledgeBase";
            this.MenuItemBuildKnowledgeBase.Size = new System.Drawing.Size(184, 20);
            this.MenuItemBuildKnowledgeBase.Text = "Проектирование базы знаний";
            this.MenuItemBuildKnowledgeBase.Click += new System.EventHandler(this.MenuItemBuildKnowledgeBase_Click);
            // 
            // MenuItemLookResult
            // 
            this.MenuItemLookResult.Name = "MenuItemLookResult";
            this.MenuItemLookResult.Size = new System.Drawing.Size(191, 20);
            this.MenuItemLookResult.Text = "Просмотр результата решения";
            this.MenuItemLookResult.Click += new System.EventHandler(this.MenuItemLookResult_Click);
            // 
            // MenuItemExit
            // 
            this.MenuItemExit.Name = "MenuItemExit";
            this.MenuItemExit.Size = new System.Drawing.Size(53, 20);
            this.MenuItemExit.Text = "Выход";
            this.MenuItemExit.Click += new System.EventHandler(this.MenuItemExit_Click);
            // 
            // TableLayoutPanel
            // 
            this.TableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel.AutoSize = true;
            this.TableLayoutPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.TableLayoutPanel.ColumnCount = 1;
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel.Controls.Add(this.TextBoxUserText, 0, 1);
            this.TableLayoutPanel.Controls.Add(this.RichTextBoxChat, 0, 0);
            this.TableLayoutPanel.Location = new System.Drawing.Point(9, 53);
            this.TableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            this.TableLayoutPanel.Padding = new System.Windows.Forms.Padding(5);
            this.TableLayoutPanel.RowCount = 2;
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TableLayoutPanel.Size = new System.Drawing.Size(504, 379);
            this.TableLayoutPanel.TabIndex = 3;
            // 
            // TextBoxUserText
            // 
            this.TextBoxUserText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxUserText.Location = new System.Drawing.Point(8, 303);
            this.TextBoxUserText.Multiline = true;
            this.TextBoxUserText.Name = "TextBoxUserText";
            this.TextBoxUserText.Size = new System.Drawing.Size(488, 68);
            this.TextBoxUserText.TabIndex = 2;
            this.TextBoxUserText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxUserText_KeyDown);
            // 
            // RichTextBoxChat
            // 
            this.RichTextBoxChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextBoxChat.Location = new System.Drawing.Point(8, 8);
            this.RichTextBoxChat.Name = "RichTextBoxChat";
            this.RichTextBoxChat.Size = new System.Drawing.Size(488, 289);
            this.RichTextBoxChat.TabIndex = 3;
            this.RichTextBoxChat.Text = "";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 432);
            this.Controls.Add(this.TableLayoutPanel);
            this.Controls.Add(this.MenuMain);
            this.MainMenuStrip = this.MenuMain;
            this.Name = "FormMain";
            this.Text = "Консультативное окно";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.MenuMain.ResumeLayout(false);
            this.MenuMain.PerformLayout();
            this.TableLayoutPanel.ResumeLayout(false);
            this.TableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuMain;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPathToBase;
        private System.Windows.Forms.ToolStripMenuItem MenuItemGuide;
        private System.Windows.Forms.ToolStripMenuItem MenuItemBuildKnowledgeBase;
        private System.Windows.Forms.ToolStripMenuItem MenuItemLookResult;
        private System.Windows.Forms.ToolStripMenuItem MenuItemExit;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        private System.Windows.Forms.TextBox TextBoxUserText;
        private System.Windows.Forms.RichTextBox RichTextBoxChat;
    }
}

