using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnowledgeBase
{
    public partial class FormMain : Form
    {
        private Globals.TreeViewSerialize _treeViewSerialize = null;
        private List<Globals.TableGraph> _listTableGraphs = null;
        private UserSystemDialog _userSystemDialog = null;

        public FormMain()
        {
            InitializeComponent();
        }

        #region UserDialog

        private void InitializeDialog()
        {            
            _userSystemDialog = new UserSystemDialog(RichTextBoxChat, TextBoxUserText);
            ShowNextQuestion(null);
        }

        private void ShowNextQuestion(string userAnswerIn)
        {
            _userSystemDialog?.ShowNextQuestion(userAnswerIn, _listTableGraphs);
        }

        #endregion



        private void MenuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuItemBuildKnowledgeBase_Click(object sender, EventArgs e)
        {            
            Globals.Forms.CreateFormConstructor( _treeViewSerialize, _listTableGraphs,_userSystemDialog, true);
            Globals.Forms.FormConstructor.Show(Globals.Forms.FormMain);
        }

        private void MenuItemLookResult_Click(object sender, EventArgs e)
        {
            Globals.Forms.CreateFormRusult(_treeViewSerialize, _listTableGraphs,_userSystemDialog, false);
            Globals.Forms.FormResult.Show(Globals.Forms.FormMain);
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Globals.Forms.DestroyFormMain();
        }

        private void MenuItemPathToBase_Click(object sender, EventArgs e)
        {
            Globals.Settings.LoadDataFromFile(ref _treeViewSerialize, ref _listTableGraphs);
            InitializeDialog();
        }

        private void TextBoxUserText_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    {
                        e.SuppressKeyPress = true;
                        ShowNextQuestion(TextBoxUserText.Text);
                        _userSystemDialog?.ClearUserTextBox();
                    }
                    break;

                case Keys.Enter | Keys.Shift:
                    {

                    }
                    break;

                default: break;
            }
        }
    }
}
