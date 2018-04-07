namespace KnowledgeBase
{
    partial class FormConstructor
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
            this.TableLayoutPanelBottom = new System.Windows.Forms.TableLayoutPanel();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.LabelCaptionP3 = new System.Windows.Forms.Label();
            this.ButtonRemoveLayerP3 = new System.Windows.Forms.Button();
            this.ButtonAddLayerP3 = new System.Windows.Forms.Button();
            this.TreeViewP3 = new System.Windows.Forms.TreeView();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.TextBoxAnnotationP1 = new System.Windows.Forms.TextBox();
            this.LabelAnnotationP1 = new System.Windows.Forms.Label();
            this.CheckBoxConsultationP1 = new System.Windows.Forms.CheckBox();
            this.TextBoxConsultationP1 = new System.Windows.Forms.TextBox();
            this.LabelConsultationP1 = new System.Windows.Forms.Label();
            this.TextBoxNameObjectP1 = new System.Windows.Forms.TextBox();
            this.TextBoxIdP1 = new System.Windows.Forms.TextBox();
            this.LabelNameObjectP1 = new System.Windows.Forms.Label();
            this.LabelIdP1 = new System.Windows.Forms.Label();
            this.TextBoxAnswerP1 = new System.Windows.Forms.TextBox();
            this.LabelAnswerP1 = new System.Windows.Forms.Label();
            this.LabelQuestionP1 = new System.Windows.Forms.Label();
            this.TextBoxQuestionP1 = new System.Windows.Forms.TextBox();
            this.Panel0 = new System.Windows.Forms.Panel();
            this.ButtonScaleMinus = new System.Windows.Forms.Button();
            this.ButtonScalePlus = new System.Windows.Forms.Button();
            this.TableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.ButtonAddGrapthP2 = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.File = new System.Windows.Forms.ToolStripMenuItem();
            this.Save = new System.Windows.Forms.ToolStripMenuItem();
            this.Panels = new System.Windows.Forms.ToolStripMenuItem();
            this.Layers = new System.Windows.Forms.ToolStripMenuItem();
            this.Objects = new System.Windows.Forms.ToolStripMenuItem();
            this.Properties = new System.Windows.Forms.ToolStripMenuItem();
            this.TableLayoutPanelBottom.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.Panel0.SuspendLayout();
            this.TableLayoutPanelMain.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanelBottom
            // 
            this.TableLayoutPanelBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanelBottom.ColumnCount = 3;
            this.TableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.00383F));
            this.TableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.99708F));
            this.TableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.9991F));
            this.TableLayoutPanelBottom.Controls.Add(this.Panel3, 0, 0);
            this.TableLayoutPanelBottom.Controls.Add(this.Panel1, 2, 0);
            this.TableLayoutPanelBottom.Controls.Add(this.Panel0, 1, 0);
            this.TableLayoutPanelBottom.Location = new System.Drawing.Point(3, 33);
            this.TableLayoutPanelBottom.Name = "TableLayoutPanelBottom";
            this.TableLayoutPanelBottom.RowCount = 1;
            this.TableLayoutPanelBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelBottom.Size = new System.Drawing.Size(889, 392);
            this.TableLayoutPanelBottom.TabIndex = 0;
            // 
            // Panel3
            // 
            this.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel3.BackColor = System.Drawing.SystemColors.Info;
            this.Panel3.Controls.Add(this.LabelCaptionP3);
            this.Panel3.Controls.Add(this.ButtonRemoveLayerP3);
            this.Panel3.Controls.Add(this.ButtonAddLayerP3);
            this.Panel3.Controls.Add(this.TreeViewP3);
            this.Panel3.Location = new System.Drawing.Point(3, 3);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(127, 386);
            this.Panel3.TabIndex = 0;
            // 
            // LabelCaptionP3
            // 
            this.LabelCaptionP3.AutoSize = true;
            this.LabelCaptionP3.Location = new System.Drawing.Point(3, 7);
            this.LabelCaptionP3.Name = "LabelCaptionP3";
            this.LabelCaptionP3.Size = new System.Drawing.Size(32, 13);
            this.LabelCaptionP3.TabIndex = 3;
            this.LabelCaptionP3.Text = "Слои";
            // 
            // ButtonRemoveLayerP3
            // 
            this.ButtonRemoveLayerP3.Location = new System.Drawing.Point(99, 3);
            this.ButtonRemoveLayerP3.Name = "ButtonRemoveLayerP3";
            this.ButtonRemoveLayerP3.Size = new System.Drawing.Size(25, 23);
            this.ButtonRemoveLayerP3.TabIndex = 2;
            this.ButtonRemoveLayerP3.Text = "-";
            this.ButtonRemoveLayerP3.UseVisualStyleBackColor = true;
            this.ButtonRemoveLayerP3.Click += new System.EventHandler(this.ButtonRemoveLayerP3_Click);
            // 
            // ButtonAddLayerP3
            // 
            this.ButtonAddLayerP3.Location = new System.Drawing.Point(68, 3);
            this.ButtonAddLayerP3.Name = "ButtonAddLayerP3";
            this.ButtonAddLayerP3.Size = new System.Drawing.Size(25, 23);
            this.ButtonAddLayerP3.TabIndex = 1;
            this.ButtonAddLayerP3.Text = "+";
            this.ButtonAddLayerP3.UseVisualStyleBackColor = true;
            this.ButtonAddLayerP3.Click += new System.EventHandler(this.ButtonAddLayerP3_Click);
            // 
            // TreeViewP3
            // 
            this.TreeViewP3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeViewP3.CheckBoxes = true;
            this.TreeViewP3.LabelEdit = true;
            this.TreeViewP3.Location = new System.Drawing.Point(3, 27);
            this.TreeViewP3.Name = "TreeViewP3";
            this.TreeViewP3.Size = new System.Drawing.Size(121, 356);
            this.TreeViewP3.TabIndex = 0;
            this.TreeViewP3.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewP3_AfterCheck);
            this.TreeViewP3.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewP3_NodeMouseDoubleClick);
            this.TreeViewP3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeViewP3_KeyDown);
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Panel1.Controls.Add(this.TextBoxAnnotationP1);
            this.Panel1.Controls.Add(this.LabelAnnotationP1);
            this.Panel1.Controls.Add(this.CheckBoxConsultationP1);
            this.Panel1.Controls.Add(this.TextBoxConsultationP1);
            this.Panel1.Controls.Add(this.LabelConsultationP1);
            this.Panel1.Controls.Add(this.TextBoxNameObjectP1);
            this.Panel1.Controls.Add(this.TextBoxIdP1);
            this.Panel1.Controls.Add(this.LabelNameObjectP1);
            this.Panel1.Controls.Add(this.LabelIdP1);
            this.Panel1.Controls.Add(this.TextBoxAnswerP1);
            this.Panel1.Controls.Add(this.LabelAnswerP1);
            this.Panel1.Controls.Add(this.LabelQuestionP1);
            this.Panel1.Controls.Add(this.TextBoxQuestionP1);
            this.Panel1.Location = new System.Drawing.Point(713, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(173, 386);
            this.Panel1.TabIndex = 1;
            // 
            // TextBoxAnnotationP1
            // 
            this.TextBoxAnnotationP1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxAnnotationP1.Location = new System.Drawing.Point(3, 291);
            this.TextBoxAnnotationP1.Multiline = true;
            this.TextBoxAnnotationP1.Name = "TextBoxAnnotationP1";
            this.TextBoxAnnotationP1.Size = new System.Drawing.Size(167, 50);
            this.TextBoxAnnotationP1.TabIndex = 12;
            // 
            // LabelAnnotationP1
            // 
            this.LabelAnnotationP1.AutoSize = true;
            this.LabelAnnotationP1.Location = new System.Drawing.Point(3, 275);
            this.LabelAnnotationP1.Name = "LabelAnnotationP1";
            this.LabelAnnotationP1.Size = new System.Drawing.Size(80, 13);
            this.LabelAnnotationP1.TabIndex = 11;
            this.LabelAnnotationP1.Text = "Комментарий:";
            // 
            // CheckBoxConsultationP1
            // 
            this.CheckBoxConsultationP1.AutoSize = true;
            this.CheckBoxConsultationP1.Location = new System.Drawing.Point(90, 205);
            this.CheckBoxConsultationP1.Name = "CheckBoxConsultationP1";
            this.CheckBoxConsultationP1.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxConsultationP1.TabIndex = 10;
            this.CheckBoxConsultationP1.UseVisualStyleBackColor = true;
            // 
            // TextBoxConsultationP1
            // 
            this.TextBoxConsultationP1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxConsultationP1.Location = new System.Drawing.Point(3, 222);
            this.TextBoxConsultationP1.Multiline = true;
            this.TextBoxConsultationP1.Name = "TextBoxConsultationP1";
            this.TextBoxConsultationP1.Size = new System.Drawing.Size(167, 50);
            this.TextBoxConsultationP1.TabIndex = 9;
            // 
            // LabelConsultationP1
            // 
            this.LabelConsultationP1.AutoSize = true;
            this.LabelConsultationP1.Location = new System.Drawing.Point(3, 205);
            this.LabelConsultationP1.Name = "LabelConsultationP1";
            this.LabelConsultationP1.Size = new System.Drawing.Size(81, 13);
            this.LabelConsultationP1.TabIndex = 8;
            this.LabelConsultationP1.Text = "Консультация:";
            // 
            // TextBoxNameObjectP1
            // 
            this.TextBoxNameObjectP1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxNameObjectP1.Location = new System.Drawing.Point(3, 43);
            this.TextBoxNameObjectP1.Name = "TextBoxNameObjectP1";
            this.TextBoxNameObjectP1.Size = new System.Drawing.Size(167, 20);
            this.TextBoxNameObjectP1.TabIndex = 7;
            // 
            // TextBoxIdP1
            // 
            this.TextBoxIdP1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxIdP1.Location = new System.Drawing.Point(20, 4);
            this.TextBoxIdP1.Name = "TextBoxIdP1";
            this.TextBoxIdP1.Size = new System.Drawing.Size(150, 20);
            this.TextBoxIdP1.TabIndex = 6;
            // 
            // LabelNameObjectP1
            // 
            this.LabelNameObjectP1.AutoSize = true;
            this.LabelNameObjectP1.Location = new System.Drawing.Point(3, 27);
            this.LabelNameObjectP1.Name = "LabelNameObjectP1";
            this.LabelNameObjectP1.Size = new System.Drawing.Size(77, 13);
            this.LabelNameObjectP1.TabIndex = 5;
            this.LabelNameObjectP1.Text = "Имя объекта:";
            // 
            // LabelIdP1
            // 
            this.LabelIdP1.AutoSize = true;
            this.LabelIdP1.Location = new System.Drawing.Point(3, 7);
            this.LabelIdP1.Name = "LabelIdP1";
            this.LabelIdP1.Size = new System.Drawing.Size(19, 13);
            this.LabelIdP1.TabIndex = 4;
            this.LabelIdP1.Text = "Id:";
            // 
            // TextBoxAnswerP1
            // 
            this.TextBoxAnswerP1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxAnswerP1.Location = new System.Drawing.Point(3, 152);
            this.TextBoxAnswerP1.Multiline = true;
            this.TextBoxAnswerP1.Name = "TextBoxAnswerP1";
            this.TextBoxAnswerP1.Size = new System.Drawing.Size(167, 50);
            this.TextBoxAnswerP1.TabIndex = 3;
            // 
            // LabelAnswerP1
            // 
            this.LabelAnswerP1.AutoSize = true;
            this.LabelAnswerP1.Location = new System.Drawing.Point(3, 135);
            this.LabelAnswerP1.Name = "LabelAnswerP1";
            this.LabelAnswerP1.Size = new System.Drawing.Size(40, 13);
            this.LabelAnswerP1.TabIndex = 2;
            this.LabelAnswerP1.Text = "Ответ:";
            // 
            // LabelQuestionP1
            // 
            this.LabelQuestionP1.AutoSize = true;
            this.LabelQuestionP1.Location = new System.Drawing.Point(3, 66);
            this.LabelQuestionP1.Name = "LabelQuestionP1";
            this.LabelQuestionP1.Size = new System.Drawing.Size(47, 13);
            this.LabelQuestionP1.TabIndex = 1;
            this.LabelQuestionP1.Text = "Вопрос:";
            // 
            // TextBoxQuestionP1
            // 
            this.TextBoxQuestionP1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxQuestionP1.Location = new System.Drawing.Point(3, 82);
            this.TextBoxQuestionP1.Multiline = true;
            this.TextBoxQuestionP1.Name = "TextBoxQuestionP1";
            this.TextBoxQuestionP1.Size = new System.Drawing.Size(167, 50);
            this.TextBoxQuestionP1.TabIndex = 0;
            // 
            // Panel0
            // 
            this.Panel0.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel0.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Panel0.Controls.Add(this.ButtonScaleMinus);
            this.Panel0.Controls.Add(this.ButtonScalePlus);
            this.Panel0.Location = new System.Drawing.Point(136, 3);
            this.Panel0.Name = "Panel0";
            this.Panel0.Size = new System.Drawing.Size(571, 386);
            this.Panel0.TabIndex = 2;
            this.Panel0.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel0_Paint);
            this.Panel0.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Panel0_MouseClick);
            this.Panel0.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel0_MouseDown);
            this.Panel0.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel0_MouseMove);
            // 
            // ButtonScaleMinus
            // 
            this.ButtonScaleMinus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonScaleMinus.Location = new System.Drawing.Point(493, 27);
            this.ButtonScaleMinus.Name = "ButtonScaleMinus";
            this.ButtonScaleMinus.Size = new System.Drawing.Size(75, 23);
            this.ButtonScaleMinus.TabIndex = 1;
            this.ButtonScaleMinus.Text = "Масштаб -";
            this.ButtonScaleMinus.UseVisualStyleBackColor = true;
            this.ButtonScaleMinus.Click += new System.EventHandler(this.ButtonScaleMinus_Click);
            // 
            // ButtonScalePlus
            // 
            this.ButtonScalePlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonScalePlus.Location = new System.Drawing.Point(493, 3);
            this.ButtonScalePlus.Name = "ButtonScalePlus";
            this.ButtonScalePlus.Size = new System.Drawing.Size(75, 23);
            this.ButtonScalePlus.TabIndex = 0;
            this.ButtonScalePlus.Text = "Масштаб +";
            this.ButtonScalePlus.UseVisualStyleBackColor = true;
            this.ButtonScalePlus.Click += new System.EventHandler(this.ButtonScalePlus_Click);
            // 
            // TableLayoutPanelMain
            // 
            this.TableLayoutPanelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanelMain.ColumnCount = 1;
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelMain.Controls.Add(this.TableLayoutPanelBottom, 0, 1);
            this.TableLayoutPanelMain.Controls.Add(this.Panel2, 0, 0);
            this.TableLayoutPanelMain.Location = new System.Drawing.Point(12, 27);
            this.TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            this.TableLayoutPanelMain.RowCount = 2;
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelMain.Size = new System.Drawing.Size(895, 428);
            this.TableLayoutPanelMain.TabIndex = 1;
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Panel2.Controls.Add(this.ButtonAddGrapthP2);
            this.Panel2.Location = new System.Drawing.Point(3, 3);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(889, 24);
            this.Panel2.TabIndex = 1;
            // 
            // ButtonAddGrapthP2
            // 
            this.ButtonAddGrapthP2.Location = new System.Drawing.Point(3, 0);
            this.ButtonAddGrapthP2.Name = "ButtonAddGrapthP2";
            this.ButtonAddGrapthP2.Size = new System.Drawing.Size(94, 23);
            this.ButtonAddGrapthP2.TabIndex = 2;
            this.ButtonAddGrapthP2.Text = "Добавить граф";
            this.ButtonAddGrapthP2.UseVisualStyleBackColor = true;
            this.ButtonAddGrapthP2.Click += new System.EventHandler(this.ButtonGrapth_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File,
            this.Panels});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(919, 24);
            this.MainMenu.TabIndex = 2;
            this.MainMenu.Text = "MainMenu";
            // 
            // File
            // 
            this.File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Save});
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(48, 20);
            this.File.Text = "Файл";
            // 
            // Save
            // 
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(132, 22);
            this.Save.Text = "Сохранить";
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Panels
            // 
            this.Panels.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Layers,
            this.Objects,
            this.Properties});
            this.Panels.Name = "Panels";
            this.Panels.Size = new System.Drawing.Size(61, 20);
            this.Panels.Text = "Панели";
            // 
            // Layers
            // 
            this.Layers.Checked = true;
            this.Layers.CheckOnClick = true;
            this.Layers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Layers.Name = "Layers";
            this.Layers.Size = new System.Drawing.Size(125, 22);
            this.Layers.Text = "Слои";
            this.Layers.CheckedChanged += new System.EventHandler(this.Layers_CheckedChanged_1);
            // 
            // Objects
            // 
            this.Objects.Checked = true;
            this.Objects.CheckOnClick = true;
            this.Objects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Objects.Name = "Objects";
            this.Objects.Size = new System.Drawing.Size(125, 22);
            this.Objects.Text = "Объекты";
            this.Objects.CheckedChanged += new System.EventHandler(this.Objects_CheckedChanged);
            // 
            // Properties
            // 
            this.Properties.Checked = true;
            this.Properties.CheckOnClick = true;
            this.Properties.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Properties.Name = "Properties";
            this.Properties.Size = new System.Drawing.Size(125, 22);
            this.Properties.Text = "Свойства";
            this.Properties.CheckedChanged += new System.EventHandler(this.Properties_CheckedChanged);
            // 
            // FormConstructor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 467);
            this.Controls.Add(this.TableLayoutPanelMain);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "FormConstructor";
            this.Text = "Окно редактирования базы данных";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormConstructor_FormClosed);
            this.TableLayoutPanelBottom.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel0.ResumeLayout(false);
            this.TableLayoutPanelMain.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TableLayoutPanelBottom;
        private System.Windows.Forms.Panel Panel3;
        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Panel Panel0;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanelMain;
        private System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.TreeView TreeViewP3;
        private System.Windows.Forms.Button ButtonAddLayerP3;
        private System.Windows.Forms.Button ButtonRemoveLayerP3;
        private System.Windows.Forms.Button ButtonAddGrapthP2;
        private System.Windows.Forms.TextBox TextBoxQuestionP1;
        private System.Windows.Forms.Label LabelQuestionP1;
        private System.Windows.Forms.Label LabelAnswerP1;
        private System.Windows.Forms.TextBox TextBoxAnswerP1;
        private System.Windows.Forms.Label LabelNameObjectP1;
        private System.Windows.Forms.Label LabelIdP1;
        private System.Windows.Forms.TextBox TextBoxIdP1;
        private System.Windows.Forms.TextBox TextBoxNameObjectP1;
        private System.Windows.Forms.Label LabelConsultationP1;
        private System.Windows.Forms.TextBox TextBoxConsultationP1;
        private System.Windows.Forms.CheckBox CheckBoxConsultationP1;
        private System.Windows.Forms.Button ButtonScaleMinus;
        private System.Windows.Forms.Button ButtonScalePlus;
        private System.Windows.Forms.Label LabelAnnotationP1;
        private System.Windows.Forms.TextBox TextBoxAnnotationP1;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem File;
        private System.Windows.Forms.ToolStripMenuItem Save;
        private System.Windows.Forms.ToolStripMenuItem Panels;
        private System.Windows.Forms.ToolStripMenuItem Layers;
        private System.Windows.Forms.ToolStripMenuItem Objects;
        private System.Windows.Forms.ToolStripMenuItem Properties;
        private System.Windows.Forms.Label LabelCaptionP3;
    }
}