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
        public TableGraph SelectedPreviousTableGraph = null;
        public List<TableGraph> ListGraphs = null;
        public GraphStateEnum GraphState = GraphStateEnum.None;
        public EditSizeStateEnum EditSizeState = EditSizeStateEnum.None;

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
                Font graphFont = new Font(FontFamily.GenericSansSerif, 12f,FontStyle.Bold);
                StringFormat textFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                Brush graphBrush = null;
                Pen graphPenBorder = new Pen(graph.GraphBorderColor, 1.0f);

                if (graph.IsCurrentGraphForResult)
                    graphBrush = new SolidBrush(graph.CurrentGraphResultColor);
                else if (graph.IsPathForResult)
                    graphBrush = new SolidBrush(graph.GraphResultColor);
                else
                    graphBrush = new SolidBrush(graph.GraphConstructorColor);

                if (graph.IsReference)
                {
                    PointF top = new PointF(graph.Rectangle.Left + graph.Rectangle.Width / 2.0f, graph.Rectangle.Top);
                    PointF left = new PointF(graph.Rectangle.Left, graph.Rectangle.Bottom - graph.Rectangle.Height / 2.0f);
                    PointF right = new PointF(graph.Rectangle.Right, graph.Rectangle.Bottom - graph.Rectangle.Height / 2.0f);
                    PointF bottom = new PointF(graph.Rectangle.Left + graph.Rectangle.Width / 2.0f, graph.Rectangle.Bottom);

                    graphics.FillPolygon(graphBrush, new[] { top, left, bottom, right });
                }
                else if (graph.IsShowConsultation)
                {
                    PointF top = new PointF(graph.Rectangle.Left + graph.Rectangle.Width / 2.0f, graph.Rectangle.Top);
                    PointF left = new PointF(graph.Rectangle.Left, graph.Rectangle.Bottom);
                    PointF right = new PointF(graph.Rectangle.Right, graph.Rectangle.Bottom);

                    graphics.FillPolygon(graphBrush, new[] { top, left, right });

                    if (graph.IsDrawGraphBorderLine) graphics.DrawPolygon(graphPenBorder, new[] { top, left, right });
                }
                else
                {
                    graphics.FillRectangle(graphBrush, graph.Rectangle);

                    if (graph.IsDrawGraphBorderLine)
                        graphics.DrawRectangle(graphPenBorder, graph.Rectangle.X, graph.Rectangle.Y,
                            graph.Rectangle.Width, graph.Rectangle.Height);
                }

                graphics.DrawString(graph.NameObject, graphFont, textBrush, graph.Rectangle, textFormat);

                if(!graph.IsReference) DrawConnectionLine(graph, graphics);

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
                    PointF firstCrossPoint = GetPointOnVector(firstLinePoint, secondLinePoint, firstGraph.Rectangle.Height / 2.0f);
                    PointF secondCrossPoint = GetPointOnVector(firstLinePoint, secondLinePoint, vecDirectLen - tableGraphIn.Rectangle.Height / 2.0f);

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
        /// <returns>TableGraph,null</returns>
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

        public void ShowCursorSizeAndAutosetEditState(Point mousePointIn)
        {
            if (SelectedTableGraph == null) return;

            PointF translateScaleMousePoint = GetTransformPointFromMatrix(_matrixTransform, mousePointIn);

            int k = 4;
            bool cursorChangeLeft = Math.Abs(Math.Abs(SelectedTableGraph.Rectangle.Left) - Math.Abs(translateScaleMousePoint.X)) <= k
                                    && Math.Abs(SelectedTableGraph.Rectangle.Top) < Math.Abs(translateScaleMousePoint.Y)
                                    && Math.Abs(SelectedTableGraph.Rectangle.Bottom) > Math.Abs(translateScaleMousePoint.Y);
            bool cursorChangeRight = Math.Abs(Math.Abs(SelectedTableGraph.Rectangle.Right) - Math.Abs(translateScaleMousePoint.X)) <= k
                                     && Math.Abs(SelectedTableGraph.Rectangle.Top) < Math.Abs(translateScaleMousePoint.Y)
                                     && Math.Abs(SelectedTableGraph.Rectangle.Bottom) > Math.Abs(translateScaleMousePoint.Y);
            bool cursorChangeTop = Math.Abs(Math.Abs(SelectedTableGraph.Rectangle.Top) - Math.Abs(translateScaleMousePoint.Y)) <= k
                                   && Math.Abs(SelectedTableGraph.Rectangle.Left) < Math.Abs(translateScaleMousePoint.X)
                                   && Math.Abs(SelectedTableGraph.Rectangle.Right) > Math.Abs(translateScaleMousePoint.X);
            bool cursorChangeBottom = Math.Abs(Math.Abs(SelectedTableGraph.Rectangle.Bottom) - Math.Abs(translateScaleMousePoint.Y)) <= k
                                      && Math.Abs(SelectedTableGraph.Rectangle.Left) < Math.Abs(translateScaleMousePoint.X)
                                      && Math.Abs(SelectedTableGraph.Rectangle.Right) > Math.Abs(translateScaleMousePoint.X);
            if (cursorChangeTop)
            {
                Cursor.Current = Cursors.SizeNS; EditSizeState = EditSizeStateEnum.Top;
            }
            else if (cursorChangeBottom)
            {
                Cursor.Current = Cursors.SizeNS; EditSizeState = EditSizeStateEnum.Bottom;
            }
            else if (cursorChangeLeft)
            {
                Cursor.Current = Cursors.SizeWE; EditSizeState = EditSizeStateEnum.Left;
            }
            else if (cursorChangeRight)
            {
                Cursor.Current = Cursors.SizeWE; EditSizeState = EditSizeStateEnum.Right;
            }
            else
            {
                EditSizeState = EditSizeStateEnum.None;
            }
        }

        public void ResizeGraph(Point firstMousePointIn, Point secondMousePointIn, SizeF oldSizeIn)
        {
            if (SelectedTableGraph == null) return;

            PointF translateScaleMousePointFirst = GetTransformPointFromMatrix(_matrixTransform, firstMousePointIn);
            PointF translateScaleMousePointSecond = GetTransformPointFromMatrix(_matrixTransform, secondMousePointIn);

            switch (EditSizeState)
            {
                case EditSizeStateEnum.Top:
                    {
                        float diff = Math.Abs(translateScaleMousePointFirst.Y) - Math.Abs(translateScaleMousePointSecond.Y);
                        if (oldSizeIn.Height + diff > TableGraph.GraphSize.Height)
                        {
                            SelectedTableGraph.Rectangle.Y = translateScaleMousePointSecond.Y;
                            SelectedTableGraph.Rectangle.Height = oldSizeIn.Height + diff;
                        }
                        else
                            SelectedTableGraph.Rectangle.Height = TableGraph.GraphSize.Height;

                        break;
                    }
                case EditSizeStateEnum.Bottom:
                    {
                        float diff = Math.Abs(translateScaleMousePointSecond.Y) - Math.Abs(translateScaleMousePointFirst.Y);
                        if (oldSizeIn.Height + diff > TableGraph.GraphSize.Height)
                            SelectedTableGraph.Rectangle.Height = oldSizeIn.Height + diff;
                        else
                            SelectedTableGraph.Rectangle.Height = TableGraph.GraphSize.Height;

                        break;
                    }
                case EditSizeStateEnum.Left:
                    {
                        float diff = Math.Abs(translateScaleMousePointFirst.X) - Math.Abs(translateScaleMousePointSecond.X);
                        if (oldSizeIn.Width + diff > TableGraph.GraphSize.Width)
                        {
                            SelectedTableGraph.Rectangle.X = translateScaleMousePointSecond.X;
                            SelectedTableGraph.Rectangle.Width = oldSizeIn.Width + diff;
                        }
                        else
                            SelectedTableGraph.Rectangle.Width = TableGraph.GraphSize.Width;

                        break;
                    }
                case EditSizeStateEnum.Right:
                    {
                        float diff = Math.Abs(translateScaleMousePointSecond.X) - Math.Abs(translateScaleMousePointFirst.X);
                        if (oldSizeIn.Width + diff > TableGraph.GraphSize.Width)
                            SelectedTableGraph.Rectangle.Width = oldSizeIn.Width + diff;
                        else
                            SelectedTableGraph.Rectangle.Width = TableGraph.GraphSize.Width;
                        break;
                    }
                default: break;
            }
        }
    }
}
