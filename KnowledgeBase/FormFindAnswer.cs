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
using KnowledgeBase.Globals;

namespace KnowledgeBase
{
    public partial class FormFindAnswer : Form
    {
        private readonly List<Globals.TableGraph> _tableGraphs = null;
        private readonly UserSystemDialog _userSystemDialog = null;

        public FormFindAnswer(List<Globals.TableGraph> tableGraphsIn, UserSystemDialog userSystemDialogInOut)
        {
            InitializeComponent();

            _tableGraphs = tableGraphsIn; _userSystemDialog = userSystemDialogInOut;
            SetDataToDataGridView("");
        }

        private void SetDataToDataGridView(string filterIn)
        {
            if (_tableGraphs != null)
            {
                foreach (var tableGraph in _tableGraphs)
                {
                    if (tableGraph.UserAnswers == null) continue;
                    foreach (var answer in tableGraph.UserAnswers)
                    {                        
                        var regex = new Regex(filterIn,RegexOptions.IgnoreCase);
                        if(!regex.IsMatch(answer)) continue;
                        var index = DataGridView.Rows.Add(0, answer);
                        DataGridView.Rows[index].Tag = tableGraph;
                    }
                }
            }
        }

        private void FormFindAnswer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Globals.Forms.DestroyFormFindAnswer();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView.Rows[e.RowIndex].Cells["Number"].Value = e.RowIndex + 1;
        }

        private void TextBoxFind_TextChanged(object sender, EventArgs e)
        {
            DataGridView.Rows.Clear();
            SetDataToDataGridView(TextBoxFind.Text);
        }

        private void DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var row = DataGridView.Rows[e.RowIndex];
            _userSystemDialog.MoveToTableGraph(row.Cells["Answer"].Value.ToString(), (TableGraph)row.Tag);
            Close();
        }
    }
}
