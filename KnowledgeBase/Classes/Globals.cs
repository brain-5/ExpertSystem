using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnowledgeBase.Annotations;
using Newtonsoft.Json;

namespace KnowledgeBase
{
    namespace Globals
    {
        public class TableLog
        {
            public TableGraph SystemTableGraph = null;
            public string UserAnswer = null;
        }

        public class TableGraph : ICloneable
        {
            public int Id = 0;
            public List<int> ParentIds = null;
            public string Question = null;
            public string Consultation = null;
            public List<string> UserAnswers = null;
            public string NameObject = null;
            public string Annotation = null;
            public bool IsShowConsultation = false;

            #region Graphics

            public static Size GraphSize = new Size(40, 40);
            /// <summary>
            /// //Contains GraphSize
            /// </summary>
            public RectangleF Rectangle;
            public Color GraphConstructorColor;
            public Color LineConstructorColor;
            public Color TextConstructorColor;

            public Color GraphResultColor;
            public Color LineResultColor;
            public Color TextResultColor;
            public Color CurrentGraphResultColor;

            #endregion

            #region Layers

            [JsonIgnore]
            public TreeNode LayerTreeNodeOwner = null;

            #endregion

            #region FinishPath

            [JsonIgnore]
            public bool IsPathForResult = false;
            [JsonIgnore]
            public bool IsCurrentGraphForResult = false;

            #endregion


            public TableGraph()
            {
                ParentIds = new List<int>();
                GraphConstructorColor = Color.CornflowerBlue; LineConstructorColor = Color.Black; TextConstructorColor = Color.White;
                GraphResultColor = Color.DarkSeaGreen; LineResultColor = Color.DarkSeaGreen; TextResultColor = Color.White;
                CurrentGraphResultColor = Color.LightSeaGreen;
            }

            public object Clone()
            {
                return MemberwiseClone();
            }
        }

        public class TreeViewSerialize
        {
            public string Text = null;
            public bool IsCheked = false;
            public int GraphId = -1;
            public List<TreeViewSerialize> NodeList = null;
        }


        /// <summary>
        /// Данные для хранения в файле
        /// </summary>
        public class SaveLoadDataSerialize
        {
            public TreeViewSerialize TreeViewSerialize = null;
            public List<TableGraph> TableGraphsList = null;

            public SaveLoadDataSerialize()
            {
                TreeViewSerialize = new TreeViewSerialize();
                TableGraphsList = new List<TableGraph>();
            }

            public SaveLoadDataSerialize(List<TableGraph> tableGraphsIn)
            {
                TreeViewSerialize = new TreeViewSerialize();
                TableGraphsList = new List<TableGraph>(tableGraphsIn);
            }
        }

        public class Settings
        {
            public static float ScaleFactor = 0.1f;
            public static float LengthArrow = 10.0f;

            private static string _fileFilter = "(*.json)|*.json";

            public static void ToTreeViewSerialize(ref TreeViewSerialize treeViewSerialize, TreeNodeCollection treeNodeCollection)
            {
                if (treeNodeCollection.Count > 0)
                {
                    treeViewSerialize.NodeList = new List<TreeViewSerialize>();
                    foreach (TreeNode node in treeNodeCollection)
                    {
                        TreeViewSerialize tvSerialize = new TreeViewSerialize()
                        {
                            GraphId = (int)(node.Tag ?? -1),
                            IsCheked = node.Checked,
                            Text = node.Text
                        };

                        treeViewSerialize.NodeList.Add(tvSerialize);
                        ToTreeViewSerialize(ref tvSerialize, node.Nodes);
                    }
                }
            }

            public static void FromTreeViewSerialize(TreeViewSerialize treeViewSerializeIn, ref TreeNodeCollection treeNodeCollectionOut, ref List<TableGraph> listTableGraphOut)
            {
                TreeNodeCollection nodes = treeNodeCollectionOut;
                if (treeViewSerializeIn?.Text != null)
                {
                    TreeNode node = treeNodeCollectionOut.Add(treeViewSerializeIn.Text);
                    node.Checked = treeViewSerializeIn.IsCheked;
                    node.Tag = treeViewSerializeIn.GraphId;
                    nodes = node.Nodes;

                    var tableGraph = listTableGraphOut.FirstOrDefault(x => x.Id == treeViewSerializeIn.GraphId);
                    if (tableGraph != null) tableGraph.LayerTreeNodeOwner = node;
                }

                if (treeViewSerializeIn?.NodeList != null)
                {
                    foreach (TreeViewSerialize treeViewSerialize in treeViewSerializeIn.NodeList)
                    {
                        FromTreeViewSerialize(treeViewSerialize, ref nodes, ref listTableGraphOut);
                    }
                }
            }

            public static string JsonPrettify(string jsonIn)
            {
                using (var stringReader = new StringReader(jsonIn))
                using (var stringWriter = new StringWriter())
                {
                    var jsonReader = new JsonTextReader(stringReader);
                    var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                    jsonWriter.WriteToken(jsonReader);
                    return stringWriter.ToString();
                }
            }

            public static bool SaveDataToFile(TreeView treeView, List<TableGraph> listTableGraph)
            {
                bool res = true;

                try
                {
                    SaveLoadDataSerialize dataSerialize = new SaveLoadDataSerialize(listTableGraph);
                    ToTreeViewSerialize(ref dataSerialize.TreeViewSerialize, treeView.Nodes);

                    string dataString = JsonConvert.SerializeObject(dataSerialize);
                    dataString = JsonPrettify(dataString);

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = _fileFilter;
                        saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.OpenFile()))
                            {
                                streamWriter.Write(dataString);
                                streamWriter.Close();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    res = false;
                    MessageBox.Show(e.Message, "Ошибка!");
                }

                return res;
            }

            public static bool LoadDataFromFile(ref TreeViewSerialize treeViewSerializeOut, ref List<TableGraph> listTableGraphOut, string previosBasePathIn = null)
            {
                bool res = true;

                try
                {
                    string filePath = previosBasePathIn;
                    if (String.IsNullOrWhiteSpace(filePath))
                    {
                        using (OpenFileDialog openFileDialog = new OpenFileDialog())
                        {
                            openFileDialog.Filter = _fileFilter;
                            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                Properties.Settings.Default.PreviousBasePath = openFileDialog.FileName;
                                Properties.Settings.Default.Save();

                                filePath = openFileDialog.FileName;
                            }
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(filePath))
                    {
                        using (StreamReader streamReader = new StreamReader(filePath))
                        {
                            string textFromFile = streamReader.ReadToEnd();
                            SaveLoadDataSerialize dataSerialize = JsonConvert.DeserializeObject<SaveLoadDataSerialize>(textFromFile);

                            treeViewSerializeOut = dataSerialize.TreeViewSerialize;
                            listTableGraphOut = dataSerialize.TableGraphsList;

                            streamReader.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    res = false;
                    MessageBox.Show(e.Message, "Ошибка!");
                }

                return res;
            }
        }

        public class Forms
        {
            public static FormMain FormMain { get; private set; } = null;
            public static FormConstructor FormConstructor { get; private set; } = null;
            public static FormConstructor FormResult { get; private set; } = null;
            public static FormEditAnswer FormEditAnswer { get; private set; } = null;
            public static FormFindAnswer FormFindAnswer { get; private set; } = null;

            public static void CreateFormMain()
            {
                if (FormMain == null)
                {
                    FormMain = new FormMain();
                    FormMain.Show();
                }
            }            

            public static void DestroyFormMain()
            {
                FormMain?.Dispose();
                FormMain = null;
            }

            public static void CreateFormConstructor(TreeViewSerialize treeViewSerializeIn, List<TableGraph> listTableGraphIn, UserSystemDialog userSystemDialogIn, bool isFormConstructor)
            {
                if (FormConstructor == null)
                {
                    FormConstructor = new FormConstructor(treeViewSerializeIn, listTableGraphIn, userSystemDialogIn, isFormConstructor);
                    FormConstructor.Show();
                }
            }

            public static void DestroyFormConstructor()
            {
                FormConstructor?.Dispose();
                FormConstructor = null;
            }

            public static void CreateFormRusult(TreeViewSerialize treeViewSerializeIn, List<TableGraph> listTableGraphIn, UserSystemDialog userSystemDialogIn, bool isFormConstructor)
            {
                if (FormResult == null)
                {
                    FormResult = new FormConstructor(treeViewSerializeIn, listTableGraphIn, userSystemDialogIn, isFormConstructor);
                    FormResult.Show();
                }
            }

            public static void DestroyFormResult()
            {
                FormResult?.Dispose();
                FormResult = null;
            }

            public static void CreateFormEditAnswer([NotNull] List<string> userAnswersInOut)
            {
                if (FormEditAnswer == null)
                {
                    FormEditAnswer = new FormEditAnswer(userAnswersInOut);
                    FormEditAnswer.Show();
                }
            }

            public static void DestroyFormEditAnswer()
            {
                FormEditAnswer?.Dispose();
                FormEditAnswer = null;
            }

            public static void CreateFormFindAnswer(List<Globals.TableGraph> tableGraphsIn, UserSystemDialog userSystemDialogInOut)
            {
                if (FormFindAnswer == null)
                {
                    FormFindAnswer = new FormFindAnswer(tableGraphsIn, userSystemDialogInOut);
                    FormFindAnswer.Show();
                }
            }

            public static void DestroyFormFindAnswer()
            {
                FormFindAnswer?.Dispose();
                FormFindAnswer = null;
            }
        }
    }
}