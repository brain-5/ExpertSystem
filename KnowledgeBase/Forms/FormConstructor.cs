using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
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
        private Point _mouseLeftClickPoint;
        private SizeF _oldSizeGraph;

        /// <summary>
        /// Используется для ресайзинга TableLayoutPanelBottom
        /// </summary>
        private EditSizeStateEnum _editSizeStateTLPB = EditSizeStateEnum.None;

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
                TableLayoutPanelBottom.ColumnStyles[0].Width = 150.0f;
                TableLayoutPanelBottom.ColumnStyles[0].SizeType = SizeType.Absolute;
            }
            else
            {
                TableLayoutPanelBottom.ColumnStyles[0].Width = 0.0f;
                TableLayoutPanelBottom.ColumnStyles[0].SizeType = SizeType.Absolute;
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

        private List<TreeNode> GetTreeNodesLayer(bool isExcludeTopLayerIn)
        {
            List<TreeNode> list = GetTreeNodesLayerChild(TreeViewP3.Nodes);
            if (isExcludeTopLayerIn && TreeViewP3.Nodes.Count > 0) list.Remove(TreeViewP3.Nodes[0]);
            return list;
        }

        private List<TreeNode> GetTreeNodesLayerChild(TreeNodeCollection treeNodeCollectionIn)
        {
            List<TreeNode> treeNodes = new List<TreeNode>();
            foreach (TreeNode node in treeNodeCollectionIn)
            {
                treeNodes.Add(node);
                treeNodes = treeNodes.Concat(GetTreeNodesLayerChild(node.Nodes)).ToList<TreeNode>();
            }

            return treeNodes;
        }

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
            if (TreeViewP3.SelectedNode.Tag != null)
            {
                MessageBox.Show("Слои можно добавлять только для слоёв, а не для графов."); return;
            }

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

        private void ShowOneAndFocusLayer(TreeNode treeNodeIn)
        {
            List<TreeNode> listTreeNodes = GetTreeNodesLayer(true);
            foreach (var n in listTreeNodes)
            {
                if (n.Tag != null) continue;
                if (n == treeNodeIn)
                {
                    n.Checked = true;
                    n.EnsureVisible();
                }
                else n.Checked = false;
            }
        }

        private void DeleteGraph(TableGraph tableGraphIn)
        {
            foreach (TableGraph tg in _graph.ListGraphs)
                if (tg.ParentIds.Contains(_graph.SelectedTableGraph.Id))
                    tg.ParentIds.Remove(_graph.SelectedTableGraph.Id);

            if (tableGraphIn != null)
                TreeViewP3.Nodes.Remove(tableGraphIn.LayerTreeNodeOwner);

            _graph.ListGraphs.Remove(_graph.SelectedTableGraph);

            _graph.SelectedTableGraph = null;
            SetDataToControlsP1(_graph.SelectedTableGraph);
        }

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

        private void Panel0_DoubleClick(object sender, EventArgs e)
        {
            if (_graph.SelectedPreviousTableGraph != null)
            {
                _graph.SelectedPreviousTableGraph.LayerTreeNodeOwner.BackColor = Color.White;
                _graph.SelectedPreviousTableGraph.IsDrawGraphBorderLine = false;
                Panel0.Invalidate();
            }

            if (_graph.SelectedTableGraph != null && _graph.SelectedTableGraph.IsReference && _graph.SelectedTableGraph.ParentIds.Count > 0)
            {
                TableGraph tg = _graph.ListGraphs.Find(match => match.Id == _graph.SelectedTableGraph.ParentIds[0]);
                ShowOneAndFocusLayer(tg.LayerTreeNodeOwner.Parent);
            }
            else if (_graph.SelectedTableGraph != null)
            {
                _graph.SelectedTableGraph.LayerTreeNodeOwner.BackColor = Color.LightSeaGreen;
                _graph.SelectedTableGraph.IsDrawGraphBorderLine = true;
                _graph.SelectedTableGraph.LayerTreeNodeOwner.EnsureVisible();
                Panel0.Invalidate();
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
                if (_graph.SelectedTableGraph != null)
                {
                    if (_graph.GraphState == GraphStateEnum.Move)
                    {
                        //Перемещение графа
                        _graph.MoveGraph(_graph.SelectedTableGraph, e.Location);
                        Panel0.Invalidate();
                    }
                    else if (_graph.GraphState == GraphStateEnum.EditSize)
                    {
                        //Изменение размера у графа
                        _graph.ResizeGraph(_mouseLeftClickPoint, e.Location, _oldSizeGraph);
                        Panel0.Invalidate();
                    }
                }
            }
            else if (_graph.GraphState == GraphStateEnum.EditSize)
            {
                //Показать курсор изменения
                _graph.ShowCursorSizeAndAutosetEditState(e.Location);
            }
        }

        private void Panel0_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_graph.EditSizeState != EditSizeStateEnum.None)
                {
                    if (_graph.SelectedTableGraph != null)
                    {
                        _mouseLeftClickPoint = e.Location;
                        _oldSizeGraph = _graph.SelectedTableGraph.Rectangle.Size;
                    }
                }
                else if (_graph.GraphState == GraphStateEnum.SetReference)
                {
                    //Прописываем ссылку на другой слой
                    TableGraph graph = _graph.SelectGraph(e.Location);
                    if (graph != null)
                    {
                        if (graph.IsReference)
                        {
                            if (graph.ParentIds.Count > 0)
                            {
                                TableGraph tg = _graph.ListGraphs.Find(match => match.Id == graph.ParentIds[0]);
                                MessageBox.Show($"Выбранная ссылка уже содержит переход на слой '{tg.LayerTreeNodeOwner.Parent.FullPath}'");
                            }
                            else
                            {
                                _graph.SelectedTableGraph.ParentIds.Clear();
                                _graph.SelectedTableGraph.ParentIds.Add(graph.Id);
                                graph.ParentIds.Clear();
                                graph.ParentIds.Add(_graph.SelectedTableGraph.Id);
                            }
                        }
                        else MessageBox.Show("Граф должен быть ссылкой.");
                    }
                    _graph.GraphState = GraphStateEnum.None;
                }
                else
                {
                    //Выделение графа
                    var oldTableGraph = _graph.SelectedTableGraph;
                    _graph.SelectedTableGraph = _graph.SelectGraph(e.Location);
                    if (_graph.SelectedTableGraph != null && oldTableGraph != null && _graph.SelectedTableGraph.Id != oldTableGraph.Id
                                                          || oldTableGraph != null && _graph.SelectedTableGraph == null)
                        _graph.SelectedPreviousTableGraph = oldTableGraph;

                    SetDataToControlsP1(_graph.SelectedTableGraph);

                    if (_graph.SelectedTableGraph != null)
                    {
                        _graph.GraphState = GraphStateEnum.Move;
                    }
                    else _graph.GraphState = GraphStateEnum.None;
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

                    if (_graph.SelectedTableGraph.IsReference)
                    {
                        ContextMenu c = new ContextMenu();
                        c.MenuItems.Add("Переход на другой слой", (ob, ev) =>
                        {
                            ContextMenu contextSub = new ContextMenu();
                            List<TreeNode> list = GetTreeNodesLayer(true);

                            foreach (var node in list)
                            {
                                if (node.Tag != null) continue;
                                MenuItem item = contextSub.MenuItems.Add(node.Text, (obSubIn, evSubIn) =>
                                {
                                    TreeNode treeNode = (TreeNode)((MenuItem)obSubIn).Tag;
                                    ShowOneAndFocusLayer(treeNode);

                                    _graph.GraphState = GraphStateEnum.SetReference;
                                });
                                item.Tag = node;
                            }
                            contextSub.Show(Panel0, e.Location);
                        });
                        c.MenuItems.Add("Изменить размер", (ob, ev) =>
                        {
                            _graph.GraphState = GraphStateEnum.EditSize;
                        });
                        c.MenuItems.Add("Удалить граф", (ob, ev) =>
                        {
                            DeleteGraph(_graph.SelectedTableGraph);
                            Panel0.Invalidate();
                        });
                        c.Show(Panel0, e.Location);
                    }
                    else
                    {
                        TableGraph selectSecondGraph = _graph.SelectGraph(e.Location);
                        if (selectSecondGraph != null && selectSecondGraph != _graph.SelectedTableGraph)
                        {
                            ContextMenu c = new ContextMenu();
                            c.MenuItems.Add("Создать линию", (ob, ev) =>
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
                            c.MenuItems.Add("Изменить размер", (ob, ev) =>
                            {
                                _graph.GraphState = GraphStateEnum.EditSize;
                            });
                            c.MenuItems.Add("Удалить граф", (ob, ev) =>
                            {
                                DeleteGraph(_graph.SelectedTableGraph);
                                Panel0.Invalidate();
                            });
                            c.MenuItems.Add("Удалить линии с этим графом", (ob, ev) =>
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
        }

        private void Panel0_MouseUp(object sender, MouseEventArgs e)
        {
            _graph.EditSizeState = EditSizeStateEnum.None;
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

        private void AddGraph(bool isReferenceIn)
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
                        TableGraph.GraphSize),
                    IsReference = isReferenceIn
                };
            }
            else
            {
                tableGraph = new TableGraph()
                {
                    Rectangle = new Rectangle(Point.Empty, TableGraph.GraphSize),
                    IsReference = isReferenceIn
                };
            }

            //Получим номер графа в текущем дереве 
            var idNode = 0;
            var countNodes = TreeViewP3?.SelectedNode?.Nodes.Count;
            if (countNodes != null)
            {
                foreach (var node in TreeViewP3.SelectedNode.Nodes)
                {
                    if (node != null) idNode++;
                }
            }

            tableGraph.LayerTreeNodeOwner = AddObjectToLayer(idNode.ToString());
            tableGraph.LayerTreeNodeOwner.Tag = tableGraph.Id;

            _graph.ListGraphs.Add(tableGraph);

            //Update object for repaint
            Panel0.Invalidate();
        }

        private void ButtonGrapth_Click(object sender, EventArgs e)
        {
            AddGraph(false);
        }

        private void ButtonReference_Click(object sender, EventArgs e)
        {
            AddGraph(true);
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

        #region TableLayoutPanelBottom

        /// <summary>
        /// Показать курсор изменения размера у колонки со слоями (TableLayoutPanelBottom)
        /// </summary>
        /// <param name="locationIn">Позиция курсоса</param>
        /// <returns></returns>
        private bool IsShowResizeCursorTableLayoutPanelBottom(Point locationIn)
        {
            int k = 10;
            bool res = Math.Abs(TableLayoutPanelBottom.ColumnStyles[0].Width - locationIn.X) <= k
                && 0 < locationIn.Y
                && TableLayoutPanelBottom.Height > locationIn.Y;

            return res;
        }

        private void TableLayoutPanelBottom_MouseMove(object sender, MouseEventArgs e)
        {
            bool cursorChangeRight = IsShowResizeCursorTableLayoutPanelBottom(e.Location);
            if (cursorChangeRight) Cursor.Current = Cursors.SizeWE;

            if (e.Button == MouseButtons.Left && _editSizeStateTLPB == EditSizeStateEnum.Right)
            {
                float width = e.X;
                if (width >= 0.0f) TableLayoutPanelBottom.ColumnStyles[0].Width = width;
                else TableLayoutPanelBottom.ColumnStyles[0].Width = 0.0f;
                TableLayoutPanelBottom.ColumnStyles[0].SizeType = SizeType.Absolute;
            }
        }

        private void TableLayoutPanelBottom_MouseDown(object sender, MouseEventArgs e)
        {
            bool cursorChangeRight = IsShowResizeCursorTableLayoutPanelBottom(e.Location);
            if (cursorChangeRight)
            {
                _editSizeStateTLPB = EditSizeStateEnum.Right;
                _mouseLeftClickPoint = e.Location;
            }
        }

        private void TableLayoutPanelBottom_MouseUp(object sender, MouseEventArgs e)
        {
            _editSizeStateTLPB = EditSizeStateEnum.None;
        }


        #endregion


    }
}