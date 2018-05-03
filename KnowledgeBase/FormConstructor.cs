using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using KnowledgeBase.Globals;

namespace KnowledgeBase
{
    public partial class FormConstructor : Form
    {
        private readonly Graph _graph = null;
        private Point _mouseRightClickPoint;
        private readonly bool _isFormConstructor = false;
        private readonly UserSystemDialog _userSystemDialog = null;

        public FormConstructor(TreeViewSerialize treeViewSerializeIn, List<TableGraph> listTableGraphIn, UserSystemDialog userSystemDialogIn, bool isFormConstructorIn)
        {
            InitializeComponent();

            _graph = new Graph();

            _isFormConstructor = isFormConstructorIn;
            _userSystemDialog = userSystemDialogIn;

            _graph.ListGraphs = new List<TableGraph>();
            if (listTableGraphIn != null)
                foreach (TableGraph tg in listTableGraphIn)
                {
                    _graph.ListGraphs.Add(tg.Clone() as TableGraph);
                }
            if (treeViewSerializeIn != null)
            {
                var nodes = TreeViewP3.Nodes;
                Settings.FromTreeViewSerialize(treeViewSerializeIn, ref nodes, ref _graph.ListGraphs);
            }



            if (isFormConstructorIn) InitializeFormConstructor();
            else InitializeFormResult();
        }

        private void InitializeFormResult()
        {
            Text = "Окно просмотра приятия решения";

            MainMenu.Visible = false;
            TableLayoutPanelMain.RowStyles[0].Height = 0.0f;
            ButtonAddLayerP3.Visible = false;
            ButtonRemoveLayerP3.Visible = false;
            ButtonShowAnswer.Enabled = false;

            //Раскрашивание пройденного пути ответов пользователя и системы
            if (_userSystemDialog != null)
            {
                //Текущий граф
                TableGraph curTg = _graph.ListGraphs.Find(x => x.Id == _userSystemDialog.CurrentTableGraph.Id);
                if (curTg != null)
                {
                    curTg.IsCurrentGraphForResult = true;
                    curTg.LayerTreeNodeOwner.BackColor = Color.LightSeaGreen;
                }
                //Путь
                foreach (var tableLog in _userSystemDialog.TableLogs)
                {
                    TableGraph tg = _graph.ListGraphs.Find(x => x.Id == tableLog.SystemTableGraph.Id);
                    if (tg != null) tg.IsPathForResult = true;
                }
            }
        }

        private void InitializeFormConstructor()
        {
            TextBoxIdP1.TextChanged += GraphEditEventHandler;
            TextBoxNameObjectP1.TextChanged += GraphEditEventHandler;
            TextBoxQuestionP1.TextChanged += GraphEditEventHandler;            
            TextBoxConsultationP1.TextChanged += GraphEditEventHandler;
            CheckBoxConsultationP1.CheckedChanged += GraphEditEventHandler;
            TextBoxAnnotationP1.TextChanged += GraphEditEventHandler;
        }

        private void FormConstructor_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_isFormConstructor)
            {
                Forms.DestroyFormConstructor();
            }
            else
            {
                Forms.DestroyFormResult();
            }
        }


        #region Menu

        private void Save_Click(object sender, EventArgs e)
        {
            foreach (var listGraph in _graph.ListGraphs)
            {
                if (listGraph.IsShowConsultation && String.IsNullOrWhiteSpace(listGraph.Consultation))
                {
                    MessageBox.Show($"Для объекта {listGraph.Id}-{listGraph.NameObject} заполните текстовое поле «Консультация».");
                    return;
                }
            }
            Settings.SaveDataToFile(TreeViewP3, _graph.ListGraphs);
        }

        private void Layers_CheckedChanged_1(object sender, EventArgs e)
        {
            if (Layers.Checked)
            {
                TableLayoutPanelBottom.ColumnStyles[0].Width = 15.0f;
                TableLayoutPanelBottom.ColumnStyles[0].SizeType = SizeType.Percent;
            }
            else
            {
                TableLayoutPanelBottom.ColumnStyles[0].Width = 0.0f;
                TableLayoutPanelBottom.ColumnStyles[0].SizeType = SizeType.Percent;
            }
        }

        private void Objects_CheckedChanged(object sender, EventArgs e)
        {
            if (Objects.Checked)
            {
                TableLayoutPanelMain.RowStyles[0].Height = 30.0f;
            }
            else
            {
                TableLayoutPanelMain.RowStyles[0].Height = 0.0f;
            }
        }

        private void Properties_CheckedChanged(object sender, EventArgs e)
        {
            if (Properties.Checked)
            {
                TableLayoutPanelBottom.ColumnStyles[2].Width = 20.0f;
                TableLayoutPanelBottom.ColumnStyles[2].SizeType = SizeType.Percent;
            }
            else
            {
                TableLayoutPanelBottom.ColumnStyles[2].Width = 0.0f;
                TableLayoutPanelBottom.ColumnStyles[2].SizeType = SizeType.Percent;
            }
        }

        #endregion

        #region Layers

        private int GetLastNumberNode(TreeNodeCollection treeNodeCollectionIn)
        {
            int res = 0;
            foreach (TreeNode node in treeNodeCollectionIn)
            {
                res++;
                res += GetLastNumberNode(node.Nodes);
            }

            return res;
        }

        private void AddNewLayer()
        {
            int number = GetLastNumberNode(TreeViewP3.Nodes);
            string textForNewNode = "Node" + number;
            if (TreeViewP3.SelectedNode != null)
            {
                TreeViewP3.SelectedNode.Nodes.Add(textForNewNode);
            }
            else
            {
                TreeViewP3.SelectedNode = TreeViewP3.Nodes.Add(textForNewNode);
                TreeViewP3.SelectedNode.Checked = true;
            }
        }

        private TreeNode AddObjectToLayer(string nameIn)
        {
            if (TreeViewP3.Nodes.Count == 0)
            {
                AddNewLayer();
            }

            TreeNode node = TreeViewP3?.SelectedNode?.Nodes.Add(nameIn);
            if (node != null)
            {
                node.Checked = true;
            }

            return node;
        }

        #endregion


        #region Panel0

        private void Panel0_Paint(object sender, PaintEventArgs e)
        {
            _graph.DrawGraphs(e.Graphics);
        }

        private void ButtonScalePlus_Click(object sender, EventArgs e)
        {
            _graph.ScaleIncreaseView(Settings.ScaleFactor);
            Panel0.Invalidate();
        }

        private void ButtonScaleMinus_Click(object sender, EventArgs e)
        {
            _graph.ScaleIncreaseView(-Settings.ScaleFactor);
            Panel0.Invalidate();
        }

        private void Panel0_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _graph.SelectedTableGraph = _graph.SelectGraph(e.Location);
                SetDataToControlsP1(_graph.SelectedTableGraph);
            }
        }

        private void Panel0_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                //Перемещение камеры вида
                _graph.MoveCamera(new Point(_mouseRightClickPoint.X - e.X, _mouseRightClickPoint.Y - e.Y));

                _mouseRightClickPoint = e.Location;

                Panel0.Invalidate();
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (_graph.SelectedTableGraph != null && _graph.GraphState == GraphStateEnum.Move)
                {
                    _graph.MoveGraph(_graph.SelectedTableGraph, e.Location);
                    Panel0.Invalidate();
                }
            }
        }

        private void Panel0_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _graph.GraphState == GraphStateEnum.None)
            {
                if (_graph.SelectGraph(e.Location) != null)
                {
                    _graph.GraphState = GraphStateEnum.Move;
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                _mouseRightClickPoint = e.Location;
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (_graph.SelectedTableGraph != null)
                {
                    _graph.GraphState = GraphStateEnum.ContextMenu;

                    TableGraph selectSecondGraph = _graph.SelectGraph(e.Location);
                    if (selectSecondGraph != null && selectSecondGraph != _graph.SelectedTableGraph)
                    {
                        ContextMenu c = new ContextMenu();
                        c.MenuItems.Add("Создать линию", (x, a) =>
                        {
                            if (!selectSecondGraph.ParentIds.Contains(_graph.SelectedTableGraph.Id))
                            {
                                selectSecondGraph.ParentIds.Add(_graph.SelectedTableGraph.Id); Panel0.Invalidate();
                            }
                        });
                        c.Show(Panel0, e.Location);
                    }
                    else
                    {
                        ContextMenu c = new ContextMenu();
                        c.MenuItems.Add("Удалить граф", (x, a) =>
                        {
                            foreach (TableGraph tg in _graph.ListGraphs)
                                if (tg.ParentIds.Contains(_graph.SelectedTableGraph.Id))
                                    tg.ParentIds.Remove(_graph.SelectedTableGraph.Id);

                            if (selectSecondGraph != null)
                                TreeViewP3.Nodes.Remove(selectSecondGraph.LayerTreeNodeOwner);

                            _graph.ListGraphs.Remove(_graph.SelectedTableGraph);

                            _graph.SelectedTableGraph = null;
                            SetDataToControlsP1(_graph.SelectedTableGraph);

                            Panel0.Invalidate();
                        });
                        c.MenuItems.Add("Удалить линии с этим графом", (x, a) =>
                        {
                            foreach (TableGraph tg in _graph.ListGraphs)
                                if (tg.ParentIds.Contains(_graph.SelectedTableGraph.Id))
                                    tg.ParentIds.Remove(_graph.SelectedTableGraph.Id);

                            Panel0.Invalidate();
                        });
                        c.Show(Panel0, e.Location);
                    }
                }
            }
        }

        private void Panel0_MouseUp(object sender, MouseEventArgs e)
        {
            _graph.GraphState = GraphStateEnum.None;
        }

        #endregion

        #region Panel1

        private void GraphEditEventHandler(object sender, EventArgs eventArgs)
        {
            if (_graph.SelectedTableGraph != null)
            {
                if (sender == TextBoxNameObjectP1)
                {
                    _graph.SelectedTableGraph.NameObject = (sender as TextBox)?.Text;
                }
                else if (sender == TextBoxQuestionP1)
                {
                    _graph.SelectedTableGraph.Question = (sender as TextBox)?.Text;
                }                
                else if (sender == CheckBoxConsultationP1)
                {
                    _graph.SelectedTableGraph.IsShowConsultation = ((CheckBox)sender).Checked;
                }
                else if (sender == TextBoxConsultationP1)
                {
                    _graph.SelectedTableGraph.Consultation = (sender as TextBox)?.Text;
                }
                else if (sender == TextBoxAnnotationP1)
                {
                    _graph.SelectedTableGraph.Annotation = (sender as TextBox)?.Text;
                }
            }
        }

        private void SetDataToControlsP1(TableGraph tableGraph)
        {
            if (tableGraph != null)
            {
                TextBoxIdP1.Text = tableGraph.Id.ToString();
                TextBoxNameObjectP1.Text = tableGraph.NameObject;
                TextBoxQuestionP1.Text = tableGraph.Question;                
                TextBoxConsultationP1.Text = tableGraph.Consultation;
                CheckBoxConsultationP1.Checked = tableGraph.IsShowConsultation;
                TextBoxAnnotationP1.Text = tableGraph.Annotation;
            }
            else
            {
                TextBoxIdP1.Text = "";
                TextBoxNameObjectP1.Text = "";
                TextBoxQuestionP1.Text = "";                
                TextBoxConsultationP1.Text = "";
                CheckBoxConsultationP1.Checked = false;
                TextBoxAnnotationP1.Text = "";
            }
        }

        private void ButtonShowAnswer_Click(object sender, EventArgs e)
        {
            if (_graph.SelectedTableGraph != null)
            {
                if (_graph.SelectedTableGraph.UserAnswers == null) _graph.SelectedTableGraph.UserAnswers = new List<string>();

                Globals.Forms.CreateFormEditAnswer(_graph.SelectedTableGraph.UserAnswers);                
            }
            else MessageBox.Show("Сначала надо выбрать граф.");
        }

        #endregion

        #region Panel2

        private void ButtonGrapth_Click(object sender, EventArgs e)
        {
            TableGraph tableGraph = null;

            var maxItem = (from v in _graph.ListGraphs orderby v.Id descending select v).FirstOrDefault();
            if (maxItem != null)
            {
                tableGraph = new TableGraph()
                {
                    Id = maxItem.Id + 1,
                    Rectangle = new RectangleF(
                        new PointF(maxItem.Rectangle.X + TableGraph.GraphSize.Width * 2,
                        maxItem.Rectangle.Y + TableGraph.GraphSize.Height * 2),
                        TableGraph.GraphSize)
                };
            }
            else
            {
                tableGraph = new TableGraph()
                {
                    Rectangle = new Rectangle(Point.Empty, TableGraph.GraphSize)
                };
            }

            tableGraph.LayerTreeNodeOwner = AddObjectToLayer(tableGraph.Id.ToString());
            tableGraph.LayerTreeNodeOwner.Tag = tableGraph.Id;

            _graph.ListGraphs.Add(tableGraph);

            //Update object for repaint
            Panel0.Invalidate();
        }


        #endregion

        #region Panel3

        private void ButtonAddLayerP3_Click(object sender, EventArgs e)
        {
            AddNewLayer();
        }

        private void ButtonRemoveLayerP3_Click(object sender, EventArgs e)
        {
            if (TreeViewP3.SelectedNode != null)
            {
                if (TreeViewP3.SelectedNode?.Tag == null)
                {
                    bool containsChildGraph = false;
                    foreach (TableGraph tg in _graph.ListGraphs)
                    {
                        containsChildGraph |= TreeViewP3.SelectedNode.Nodes.Contains(tg.LayerTreeNodeOwner);
                    }
                    if (!containsChildGraph)
                    {
                        TreeViewP3.Nodes.Remove(TreeViewP3.SelectedNode);
                    }
                    else
                    {
                        MessageBox.Show("В подчинении есть графы. Удалять слой нельзя.");
                    }
                }
                else
                {
                    MessageBox.Show("Удалять можно только слои, а не графы.");
                }
            }
        }

        private void TreeViewP3_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var localPosition = TreeViewP3.PointToClient(Cursor.Position);
            var hitTestInfo = TreeViewP3.HitTest(localPosition);
            if (hitTestInfo.Location == TreeViewHitTestLocations.StateImage) return;

            if (e.Button == MouseButtons.Left)
            {
                e.Node.BeginEdit();
            }
        }

        private void TreeViewP3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TreeViewP3?.SelectedNode?.BeginEdit();
            }
        }

        private void TreeViewP3_AfterCheck(object sender, TreeViewEventArgs e)
        {
            Panel0.Invalidate();
        }


        #endregion

    }
}