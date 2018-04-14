using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnowledgeBase.Annotations;
using KnowledgeBase.Globals;

namespace KnowledgeBase
{
    public class UserSystemDialog
    {
        private readonly RichTextBox _chatRichTextBox = null;
        private readonly TextBox _userTextBox = null;

        public TableGraph CurrentTableGraph = null;
        public List<TableLog> TableLogs = null;

        public UserSystemDialog([NotNull] RichTextBox chatRichTextBoxIn, [NotNull] TextBox userTextBoxIn)
        {
            _chatRichTextBox = chatRichTextBoxIn; _userTextBox = userTextBoxIn;
            CurrentTableGraph = null;
            TableLogs = new List<TableLog>();
            _chatRichTextBox.Clear(); _userTextBox.Clear();
        }      

        private void PrintSystemTextToChat(string systemAnswerIn, [CanBeNull] string consultationTextIn)
        {
            _chatRichTextBox.SelectionColor = Color.Green;
            _chatRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
            _chatRichTextBox.AppendText("ExpertSystem: " + systemAnswerIn);
            _chatRichTextBox.AppendText(Environment.NewLine);

            if (!String.IsNullOrWhiteSpace(consultationTextIn))
            {
                _chatRichTextBox.AppendText(consultationTextIn);
                _chatRichTextBox.AppendText(Environment.NewLine);
            }
        }

        private void PrintUserTextToChat(string userAnswerIn)
        {
            _chatRichTextBox.SelectionColor = Color.Blue;
            _chatRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
            _chatRichTextBox.AppendText("User: " + userAnswerIn);
            _chatRichTextBox.AppendText(Environment.NewLine);
        }

        public void ClearUserTextBox()
        {
            _userTextBox.Clear();
        }

        public void ShowNextQuestion([CanBeNull] string userAnswerIn, List<TableGraph> tableGraphsIn)
        {
            TableGraph resTableGraph = null;

            if (tableGraphsIn != null && tableGraphsIn.Count > 0)
            {
                if (!String.IsNullOrEmpty(userAnswerIn))
                {
                    PrintUserTextToChat(userAnswerIn);
                    var userAnswer = userAnswerIn.ToUpper();

                    if (CurrentTableGraph != null)
                    {
                        var childsEnumerable = from v in tableGraphsIn where v.ParentIds.Contains(CurrentTableGraph.Id) select v;
                        bool isFindAnswer = false;
                        foreach (TableGraph tableGraph in childsEnumerable)
                        {
                            string[] masStrings = tableGraph.UserAnswer.ToUpper().Split(';');
                            foreach (string st in masStrings)
                            {
                                if (userAnswer.Contains(st))
                                {
                                    resTableGraph = tableGraph;
                                    isFindAnswer = true;
                                    break;
                                }
                            }
                            if (isFindAnswer) break;
                        }

                        if (!isFindAnswer)
                        {
                            PrintSystemTextToChat("Я вас не понял, повторите ответ.", null);
                        }
                    }
                }
                else if (CurrentTableGraph == null)
                {
                    //Начальный граф
                    resTableGraph = tableGraphsIn[0];
                }
            }

            if (resTableGraph != null)
            {
                CurrentTableGraph = resTableGraph;

                PrintSystemTextToChat(resTableGraph.Question, resTableGraph.IsShowConsultation ? resTableGraph.Consultation : null);

                TableLog tableLog = new TableLog()
                {
                    UserAnswer = userAnswerIn,
                    SystemTableGraph = resTableGraph
                };

                TableLogs.Add(tableLog);
            }
        }
    }
}
