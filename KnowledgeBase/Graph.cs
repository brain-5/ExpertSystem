using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Configuration;
using System.Drawing.Drawing2D;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnowledgeBase.Globals;


namespace KnowledgeBase
{
    public class Graph
    {
        private Point _translatePoint;
        private float _scale;
        private Matrix _matrixTransform = null;

        public TableGraph SelectedTableGraph = null;
        public List<TableGraph> ListGraphs = null;
        public GraphStateEnum GraphState = GraphStateEnum.None;

        public Graph()
        {
            _translatePoint = new Point(0, 0);
            _scale = 1.0f;
        }

        public void DrawGraphs(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.High;

            graphics.ResetTransform();
            graphics.ScaleTransform(_scale, _scale);
            graphics.TranslateTransform(_translatePoint.X, _translatePoint.Y);

            _matrixTransform = graphics.Transform;

            foreach (var graph in ListGraphs)
            {
                if (!IsShowGraph(graph)) continue;

                Brush textBrush = new SolidBrush(graph.TextConstructorColor);
                Font graphFont = new Font(FontFamily.GenericSansSerif, 10f);
                StringFormat textFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                Brush graphBrush = null;

                if (graph.IsCurrentGraphForResult)
                    graphBrush = new SolidBrush(graph.CurrentGraphResultColor);
                else if (graph.IsPathForResult)
                    graphBrush = new SolidBrush(graph.GraphResultColor);
                else
                    graphBrush = new SolidBrush(graph.GraphConstructorColor);

                graphics.FillEllipse(graphBrush, graph.Rectangle);
                graphics.DrawString(graph.Id.ToString(), graphFont, textBrush, graph.Rectangle, textFormat);

                DrawConnectionLine(graph, graphics);


                graphBrush.Dispose(); graphBrush = null;
                textBrush.Dispose(); textBrush = null;
                graphFont.Dispose(); graphFont = null;
            }
        }

        private void DrawConnectionLine(TableGraph tableGraphIn, Graphics graphicsIn)
        {
            //Вывод соединительной линии
            foreach (var parentId in tableGraphIn.ParentIds)
            {
                TableGraph firstGraph = ListGraphs.FirstOrDefault(tg => tg.Id == parentId);
                if (firstGraph != null && IsShowGraph(firstGraph))
                {
                    Pen linePen = null;
                    if ((tableGraphIn.IsCurrentGraphForResult || tableGraphIn.IsPathForResult) && !firstGraph.IsPathForResult)
                        linePen = new Pen(tableGraphIn.LineConstructorColor);
                    else if ((tableGraphIn.IsCurrentGraphForResult || tableGraphIn.IsPathForResult) && firstGraph.IsPathForResult)
                        linePen = new Pen(tableGraphIn.LineResultColor);
                    else
                        linePen = new Pen(tableGraphIn.LineConstructorColor);

                    PointF firstLinePoint = new PointF(firstGraph.Rectangle.Left + firstGraph.Rectangle.Width / 2.0f,
                         firstGraph.Rectangle.Top + firstGraph.Rectangle.Height / 2.0f);
                    PointF secondLinePoint = new PointF(tableGraphIn.Rectangle.Left + tableGraphIn.Rectangle.Width / 2.0f,
                         tableGraphIn.Rectangle.Top + tableGraphIn.Rectangle.Height / 2.0f);
                    PointF vectorDirect = new PointF(secondLinePoint.X - firstLinePoint.X, secondLinePoint.Y - firstLinePoint.Y);

                    float vecDirectLen = GetVecLength(vectorDirect);

                    //Ищем точки пересечения окружностей и прямой, что бы прямая не перекрывала окружность
                    PointF firstCrossPoint = GetPointOnVector(firstLinePoint, secondLinePoint, TableGraph.GraphSize.Height / 2.0f);
                    PointF secondCrossPoint = GetPointOnVector(firstLinePoint, secondLinePoint, vecDirectLen - TableGraph.GraphSize.Height / 2.0f);

                    //Чертим стрелочки направления прямой
                    //Угол боковой стрелочки к прямой будет 45 градусов
                    //Точку стрелочки боковой берем из формул полярных систем координат
                    //x=r*cosY, y=r*sinY 
                    //Угол между прямой и началом координат через полярные систмы координат
                    //angle=arctan(y/x)

                    float radToDeg = (float)(180.0 / Math.PI);
                    float degToRad = (float)(Math.PI / 180.0);

                    float angleVecDirectRad = (float)Math.Atan2(vectorDirect.Y, vectorDirect.X) * radToDeg;

                    float arrowLen = Settings.LengthArrow;
                    float anglefirstArrowRad = (135.0f + angleVecDirectRad) * degToRad;
                    float anglesecondArrowRad = (225.0f + angleVecDirectRad) * degToRad;
                    PointF firstPointArrow = new PointF(arrowLen * (float)Math.Cos(anglefirstArrowRad), arrowLen * (float)Math.Sin(anglefirstArrowRad));
                    PointF secondPointArrow = new PointF(arrowLen * (float)Math.Cos(anglesecondArrowRad), arrowLen * (float)Math.Sin(anglesecondArrowRad));


                    //Смещаем относительно точки secondCrossPoint
                    firstPointArrow.X += secondCrossPoint.X; firstPointArrow.Y += secondCrossPoint.Y;
                    secondPointArrow.X += secondCrossPoint.X; secondPointArrow.Y += secondCrossPoint.Y;

                    graphicsIn.DrawLine(linePen, firstCrossPoint, secondCrossPoint);
                    graphicsIn.DrawLine(linePen, firstPointArrow, secondCrossPoint);
                    graphicsIn.DrawLine(linePen, secondPointArrow, secondCrossPoint);


                    linePen.Dispose(); linePen = null;
                }
            }
        }

        private float GetVecLength(PointF pointFIn)
        {
            return (float)Math.Sqrt(pointFIn.X * pointFIn.X + pointFIn.Y * pointFIn.Y);
        }

        private PointF GetPointOnVector(PointF startPointFIn, PointF endPointFIn, float lenPointFromStartPointIn)
        {
            //Ищем вектор направления B через сотношение длин и направляющих векторов
            // vec A=|A|    vectorDirect=vectorDirectLen
            //            =>                                => B=circlePointLen/vectorDirectLen*vectorDirect
            // vec B=|B|    B=circlePointLen
            //Параметрическое уравнение прямой при t=1
            // x=x0+Ux*t      x=startPointFIn.X+B.X
            //          => 
            // y=y0+Uy*t      y=startPointFIn.Y+B.Y


            PointF vectorDirect = new PointF(endPointFIn.X - startPointFIn.X, endPointFIn.Y - startPointFIn.Y);

            float vectorDirectLen = GetVecLength(vectorDirect);
            float ratioLengths = lenPointFromStartPointIn / vectorDirectLen;

            PointF vecRes = new PointF(vectorDirect.X * ratioLengths + startPointFIn.X, vectorDirect.Y * ratioLengths + startPointFIn.Y);

            return vecRes;
        }

        private PointF GetTransformPointFromMatrix(Matrix matrixIn, PointF pointFIn)
        {
            //Обратные действия преобразования масштабирования и перемещения для точки
            PointF translate = new PointF(pointFIn.X - matrixIn.Elements[4], pointFIn.Y - matrixIn.Elements[5]);
            PointF translateScale = new PointF(translate.X / matrixIn.Elements[0], translate.Y / matrixIn.Elements[3]);

            return translateScale;
        }

        /// <summary>
        /// Находит граф, который попадает в область mousePointIn и возвращает его.
        /// </summary>
        /// <param name="mousePointIn"></param>
        /// <returns></returns>
        public TableGraph SelectGraph(PointF mousePointIn)
        {
            TableGraph tableGraph = null;
            foreach (var graph in ListGraphs)
            {
                if (!IsShowGraph(graph)) continue;

                //Обратные действия преобразования масштабирования и перемещения для точки
                PointF translateScaleMousePoint = GetTransformPointFromMatrix(_matrixTransform, mousePointIn);

                if (graph.Rectangle.Contains(translateScaleMousePoint))
                {
                    tableGraph = graph; break;
                }
            }

            return tableGraph;
        }

        private bool IsShowGraph(TableGraph graph)
        {
            bool res = true;

            TreeNode node = graph.LayerTreeNodeOwner;
            while (node != null)
            {
                if (!node.Checked)
                {
                    res = false; break;
                }
                node = node.Parent;
            }

            return res;
        }

        public void MoveCamera(Point pointOffset)
        {
            _translatePoint.X -= pointOffset.X;
            _translatePoint.Y -= pointOffset.Y;
        }

        public void MoveGraph(TableGraph graph, PointF pointOffset)
        {
            PointF scaleTranslatePointF = GetTransformPointFromMatrix(_matrixTransform, pointOffset);
            graph.Rectangle.Location = scaleTranslatePointF;
        }

        public void ScaleIncreaseView(float scaleIncrease)
        {
            _scale += scaleIncrease;
        }
    }
}
