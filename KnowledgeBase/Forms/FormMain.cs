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

        private void LoadDatabase(string previosBasePathIn = null)
        {
            Globals.Settings.LoadDataFromFile(ref _treeViewSerialize, ref _listTableGraphs, previosBasePathIn);
            InitializeDialog();
        }

        private void ShowFormFindAnswer()
        {
            List<Globals.TableGraph> list = null;
            if (_userSystemDialog.CurrentTableGraph != null)
                list = _listTableGraphs.Where(x => x.ParentIds.Contains(_userSystemDialog.CurrentTableGraph.Id)).ToList();

            Globals.Forms.CreateFormFindAnswer(list, _userSystemDialog);            
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

        #region Menu



        private void MenuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuItemBuildKnowledgeBase_Click(object sender, EventArgs e)
        {
            Globals.Forms.CreateFormConstructor(_treeViewSerialize, _listTableGraphs, _userSystemDialog, true);            
        }

        private void MenuItemLookResult_Click(object sender, EventArgs e)
        {
            Globals.Forms.CreateFormRusult(_treeViewSerialize, _listTableGraphs, _userSystemDialog, false);            
        }

        private void MenuItemPathToBase_Click(object sender, EventArgs e)
        {
            LoadDatabase();
        }

        #endregion

        private void TextBoxUserText_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    {
                        e.SuppressKeyPress = true;
                        ShowNextQuestion(TextBoxUserText.Text);
                        _userSystemDialog?.ClearUserTextBox();
                        if (_userSystemDialog?.UserMistakeCount >= 3) ShowFormFindAnswer();
                    }
                    break;

                case Keys.Enter | Keys.Shift:
                    {

                    }
                    break;

                default: break;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //Автозагрузка последней открытой базы
            if (!String.IsNullOrWhiteSpace(Properties.Settings.Default.PreviousBasePath))
            {
                LoadDatabase(Properties.Settings.Default.PreviousBasePath);
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Globals.Forms.DestroyFormMain();
        }

        private void ButtonShowAnswers_Click(object sender, EventArgs e)
        {
            ShowFormFindAnswer();
        }
    }
}
