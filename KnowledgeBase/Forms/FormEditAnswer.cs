using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnowledgeBase.Annotations;

namespace KnowledgeBase
{
    public partial class FormEditAnswer : Form
    {
        private readonly List<string> _userAnswers = null;
        public FormEditAnswer([NotNull] List<string> userAnswersInOut)
        {
            InitializeComponent();

            _userAnswers = userAnswersInOut;

            for (int i = 0; i < _userAnswers.Count; i++)
            {
                DataGridView.Rows.Add(i, _userAnswers[i]);
            }
        }

        private void FormAnswer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Globals.Forms.DestroyFormEditAnswer();
        }

        private void DataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView.Rows[e.RowIndex].Cells["Number"].Value = e.RowIndex + 1;
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            _userAnswers.Clear();
            for (int i = 0; i < DataGridView.Rows.Count; i++)
            {
                var row = DataGridView.Rows[i];
                if (row.IsNewRow) continue;
                _userAnswers.Add(row.Cells["Answer"].Value.ToString());
            }
            Close();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
