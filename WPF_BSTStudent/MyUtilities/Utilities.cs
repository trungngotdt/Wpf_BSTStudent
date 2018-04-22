﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WPF_BSTStudent.Model;

namespace WPF_BSTStudent.MyUtilities
{
    public class Utilities : IUtilities
    {
        private ITree<Student> tree;
        private IHelper helper;

        private int verticalMarging;
        private double heightGridBST;
        private double widthGridBST;
        private int maxHeight=5;

        public ITree<Student> Tree { get => tree; set => tree = value; }
        public int VerticalMarging { get => verticalMarging; set => verticalMarging = value; }
        public double WidthGridBST { get => widthGridBST; set => widthGridBST = value; }
        public double HeightGridBST { get => heightGridBST; set => heightGridBST = value; }
        public IHelper Helper { get => helper; set => helper = value; }
        public int MaxHeight { get => maxHeight; set => maxHeight = value; }

        #region Add a node to grid

        #region Draw line

        /// <summary>
        /// Draw a line from the button to the other button
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="isRightLeaf"></param>
        /// <param name="name"></param>
        private void DrawLine(Grid grid, double x1, double x2, double y1, double y2, bool isRightLeaf, string name)
        {
            Line l = new Line
            {
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 2.0,
                Name = name,
                X1 = x1 + (isRightLeaf ? 25 : 25),
                X2 = x1 + (isRightLeaf ? 25 : 25), //x2 + (isRightLeaf ? 0 : 50),
                Y1 = y1 + 50, //btn1Point.Y + a.ActualHeight / 2;
                Y2 = y1 + 50
            };
            AnimationGrowLine(x2 + (isRightLeaf ? 25 : 25), y2 /*+ 25*/, TimeSpan.FromSeconds(1), l);
            grid.Children.Add(l);
        }

        /// <summary>
        /// Grow a line to x2,y2
        /// </summary>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="time">The time the line will be grow</param>
        /// <param name="line"></param>
        private void AnimationGrowLine(double x2, double y2, TimeSpan time, Line line)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation animationX2 = new DoubleAnimation(x2, time);
            DoubleAnimation animationY2 = new DoubleAnimation(y2, time);
            Storyboard.SetTarget(animationX2, line);
            Storyboard.SetTargetProperty(animationX2, new PropertyPath("X2"));
            Storyboard.SetTarget(animationY2, line);
            Storyboard.SetTargetProperty(animationY2, new PropertyPath("Y2"));
            sb.Children.Add(animationX2);
            sb.Children.Add(animationY2);
            sb.Begin();
            /*line.BeginAnimation(Line.X2Property, animationX2);
            line.BeginAnimation(Line.Y2Property, animationY2);*/
        }
        #endregion

        /// <summary>
        /// To calculate new position and button will be added to Grid
        /// </summary>
        /// <param name="p">this is a grid which will be add a button</param>
        /// <param name="student"></param>
        public async Task AddButtonGridAsync(UIElement p, Student student)
        {
            double x = 0;
            double y = 0;
            if ((p as Grid).Children.OfType<Button>().Count<Button>() == 0)
            {
                Tree.Insert(new Node<Student>(student, (p as Grid).ActualWidth / 2, VerticalMarging));
                AddNode(p as Grid, student, new Thickness((p as Grid).ActualWidth / 2, VerticalMarging, 0, 0));
                return;
            }
            if (Tree.Contains(new Node<Student>(student)))
            {
                return;
            }
            var node = new Node<Student>(student, x, y);

            Tree.Insert(node);
            var checkExitsParent = Tree.FindParent(new Node<Student>(student));
            if (checkExitsParent.Item1 != null)
            {
                var oldX = checkExitsParent.Item1.X;
                var oldY = checkExitsParent.Item1.Y;
                if (checkExitsParent.Item2 == -1)
                {
                    x = oldX - (p as Grid).ActualWidth / Math.Pow(2, ((oldY + VerticalMarging) / VerticalMarging));
                    y = oldY + VerticalMarging;
                    AddNode(p as Grid, student, new Thickness(x, y, 0, 0));
                }
                else if (checkExitsParent.Item2 == 1)
                {
                    x = oldX + (p as Grid).ActualWidth / Math.Pow(2, ((oldY + VerticalMarging) / VerticalMarging));
                    y = oldY + VerticalMarging;
                    AddNode(p as Grid, student, new Thickness(x, y, 0, 0));
                }
                node.X = x;
                node.Y = y;
                if ((await Tree.HeightAsync())==MaxHeight)
                {
                    ResizeGrid();
                    (p as Grid).Width = WidthGridBST;
                    (p as Grid).Height = HeightGridBST;
                    await ReLayoutAllButtonAsync(p as Grid);
                    MaxHeight++;
                }
                DrawLine(p as Grid, checkExitsParent.Item1.X, node.X, checkExitsParent.Item1.Y, node.Y, checkExitsParent.Item2 > 0, $"{"Btn" + checkExitsParent.Item1.Data.Id.ToString() + "Btn" + node.Data.Id.ToString() }");
                                
            }

        }




        /// <summary>
        /// Add a node to Grid with location
        /// </summary>
        /// <param name="gridPanel"></param>
        /// <param name="data"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        private bool AddNode(UIElement gridPanel, Student data, Thickness thickness)
        {
            try
            {
                var grid = gridPanel as Grid;
                Button button = new Button() { Name = "Btn" + data.Id.ToString(), Content = data.Id.ToString() };

                button.SetValue(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Left);
                button.SetValue(Grid.VerticalAlignmentProperty, VerticalAlignment.Top);
                button.Margin = thickness;// new Thickness(1138.3 / 2, 20, 0, 0);
                button.Width = 50;
                button.Height = 50;
                RoundButton(button);
                grid.Children.Add(button);
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }

        #region Relayout for button

        /// <summary>
        /// Re-layout all buttons from grid
        /// </summary>
        /// <param name="grid"></param>
        private async Task ReLayoutAllButtonAsync(Grid grid)
        {
            List<Task> listTask = new List<Task>();
            grid.Children.OfType<Button>().ToList().ForEach(p =>
            {
                var task1 = Task.Factory.StartNew(() =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var Left = p.Margin.Left * 2;
                        var student = new Student(int.Parse(p.Content.ToString()));
                        Tree.FindNode(new Node<Student>(student)).X = Left;
                        p.Margin = new Thickness(Left, p.Margin.Top, p.Margin.Right, p.Margin.Bottom);
                    });
                });
                listTask.Add(task1);
            });
            grid.Children.OfType<Line>().ToList().ForEach(p =>
            {
                Task task2 = Task.Factory.StartNew(() =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var X2 = p.X2;
                        var Y2 = p.Y2;
                        var name = p.Name;//Type : Btn'firstnum'Btn'lastnum',Example :Btn1Btn2
                        var number1 = Regex.Split(name, "Btn")[1];//Get first number 
                        var number2 = Regex.Split(name, "Btn")[2];//Get last number
                        p.BeginAnimation(Line.X2Property, null);//Animation be removed
                        if (int.Parse(number2) > int.Parse(number1))//Right
                        {
                            p.X1 = p.X1 * 2 - 25;//- 50;
                            p.X2 = X2 * 2 - 25;
                        }
                        else//Left
                        {
                            p.X2 = X2 * 2 - 25;//- 50;
                            p.X1 = p.X1 * 2 - 25;
                        }
                        p.BeginAnimation(Line.Y2Property, null);//Animation be removed
                        p.Y2 = Y2;
                    });
                });
                listTask.Add(task2);
            });
            await Task.WhenAll(listTask);

        }
        /// <summary>
        /// Resize the grid (<seealso cref="HeightGridBST"/>+100; <seealso cref="WidthGridBST"/>*2)
        /// </summary>
        private void ResizeGrid()
        {
            WidthGridBST = WidthGridBST * 2;
            HeightGridBST = HeightGridBST + 100;
        }

        #endregion

        /// <summary>
        /// Draw the round button
        /// </summary>
        /// <param name="button"></param>
        private void RoundButton(Button button)
        {
            Style style = Application.Current.FindResource("MaterialDesignFloatingActionDarkButton") as Style;
            button.Style = style;
            button.ToolTip = button.Content;
        }
        #endregion

        #region Find a Node (button) in grid

        /// <summary>
        /// Function for btnFindNodeClickCommand
        /// It will find a node-button in grid and draw a circle around button 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="grid"></param>
        public void FindNodeInGrid(Node<Student> node, UIElement grid)
        {
            var nodeBeFind = Tree.FindNode(node);
            if (nodeBeFind == null)
            {
                return;
            }
            var button = (grid as Grid).Children.OfType<Button>().ToList()
                .Where(p => p.Content.Equals(nodeBeFind.Data.Id.ToString()))
                .FirstOrDefault();
            if (button == null)
            {
                return;
            }
            var point = button.TranslatePoint(new Point(0, 0), grid as Grid);
            CreateCircleAsync(point, grid);

        }

        /// <summary>
        /// Create a circle in 3 seconds
        /// </summary>
        /// <param name="point"></param>
        private async void CreateCircleAsync(Point point, UIElement grid)
        {
            Ellipse ellipse;
            await Task.Factory.StartNew(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ellipse = new Ellipse()
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(point.X - 5, point.Y - 5, 0, 0),
                        Stroke = new SolidColorBrush(Colors.Red),
                        Width = 60,
                        Height = 60,
                        StrokeThickness = 1.0
                    };
                    (grid as Grid).Children.Add(ellipse);

                });

                //.OfType<Ellipse>()
            });
            await Task.Factory.StartNew(() =>
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    await Task.Delay(3000);
                    (grid as Grid).Children.Remove((grid as Grid).Children.OfType<Ellipse>().FirstOrDefault());
                });
            });
        }
        #endregion

        #region Delete a node (button)

        /// <summary>
        /// Delete the student ( id is <paramref name="nodeDelete"/> ) in <paramref name="grid"/>
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="nodeDelete"></param>
        public async Task DeleteNodeInGridAsync(Grid grid, int nodeDelete)
        {
            int NumBeDelete = nodeDelete;
            var tup = FindButtonInGrid(grid, nodeDelete);
            var student = new Student(nodeDelete);
            //Task.Factory.ContinueWhenAll(tup.Result.Item1.ToArray(), p => { });
            await tup.ContinueWith(p =>
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    var nodeDe = Tree.FindNode(new Node<Student>(student));
                    if (nodeDe.Left != null && nodeDe.Right != null)
                    {
                        var successor = Tree.GetMin(nodeDe.Right);
                        var nodeSucc = Tree.FindNode(nodeDe, successor);
                        var buttonSucc = grid.Children.OfType<Button>().Where(s => s.Content.Equals(nodeSucc.Data.Id.ToString())).FirstOrDefault();
                        AnimationButtonMovetTo(nodeDe.X, nodeDe.Y, buttonSucc);//to move a successor to new position (the button will be deleted)
                        Node<Student> nodeDelPar = new Node<Student>();
                        Task taskFindParent = Task.Factory.StartNew(() => { nodeDelPar = Tree.FindParent(new Node<Student>(student)).Item1; });
                        Task task = Task.Factory.StartNew(() =>
                        {
                            Application.Current.Dispatcher.Invoke(async () => { await UpdateButtonAfterDeleteAsync(grid, nodeSucc.Data); });
                        });
                        grid.Children.Remove(p.Result.Item2);
                        await Task.Factory.ContinueWhenAll(new Task[] { task, taskFindParent }, t =>
                        {
                            Tree.Remove(new Student(NumBeDelete));
                            //Tree.Remove(new Node<int>(NumBeDelete));
                        });
                        grid.Children.OfType<Line>().Where(l => l.Name.Contains($"{"Btn" + nodeDelete.ToString()}")).ToList().ForEach((item) =>
                        {
                            item.Name = item.Name.Replace($"{"Btn" + nodeDelete.ToString()}", $"{"Btn" + nodeSucc.Data.Id.ToString()}");
                        });

                    }
                    else
                    {
                        Task task = Task.Factory.StartNew(() =>
                        {
                            Application.Current.Dispatcher.Invoke(async () => { await UpdateButtonAfterDeleteAsync(grid, student); });
                        });
                        await Task.Factory.ContinueWhenAll(new Task[] { task }, t =>
                        {
                            if (Tree.FindParent(new Node<Student>(student)).Item1 == null && nodeDe.Right == null && nodeDe.Left == null)
                            {
                                Tree = null;
                            }
                            else
                            {                                
                                Tree. Remove(new Student(NumBeDelete));                                
                            }
                        }); grid.Children.Remove(p.Result.Item2);
                    }
                    //AnimationButtonMovetTo(20, 20, p.Result.Item2);
                });
            });
        }

        

        /// <summary>
        /// The line be found in grid by name
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private Line FindLineInGrid(Grid grid, string name)
        {
            var line = grid.Children.OfType<Line>().Where(p => p.Name.Equals(name)).FirstOrDefault();
            return line;
        }

        /// <summary>
        /// Find the Button in grid  - async
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="match">name of the button</param>
        /// <returns></returns>
        public async Task<Tuple<List<Task>, Button>> FindButtonInGrid(Grid grid, object match)
        {
            Button button = null;
            List<Task> listTask = new List<Task>();
            var allButtonInGrid = grid.Children.OfType<Button>().ToList();
            for (int i = 0; i < allButtonInGrid.Count; i++)//Find a button 
            {
                int j = i;
                var task = Task.Factory.StartNew(() =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (allButtonInGrid[j].Content.Equals(match.ToString()))
                        {
                            button = allButtonInGrid[j];
                        }
                    });
                });
                listTask.Add(task);
            }
            await Task.WhenAll(listTask);
            //var result = new Tuple<List<Task>, Button>(listTask, button);
            return new Tuple<List<Task>, Button>(listTask, button);
        }

        /// <summary>
        /// Update X,Y for button (node)
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="nodeDelete"></param>
        async Task UpdateButtonAfterDeleteAsync(Grid grid, Student nodeDelete)
        {

            var nodeDel = Tree.FindNode(new Node<Student>(nodeDelete));
            //Delete button have one or none child
            var nodePa = Tree.FindParent(new Node<Student>(nodeDel.Data));//Parent of nodeDelete
            if (nodePa.Item1 != null)
            {
                var line = FindLineInGrid(grid, $"{"Btn" + nodePa.Item1.Data.Id.ToString() + "Btn" + nodeDel.Data.Id.ToString()}");
                grid.Children.Remove(line);
            }
            //Relayout line

            var lineL = nodeDel.Left == null ? null : FindLineInGrid(grid, $"{"Btn" + nodeDel.Data.Id.ToString() + "Btn" + nodeDel.Left.Data.Id.ToString()}");
            var lineR = nodeDel.Right == null ? null : FindLineInGrid(grid, $"{"Btn" + nodeDel.Data.Id.ToString() + "Btn" + nodeDel.Right.Data.Id.ToString()}");
            grid.Children.Remove(lineR);
            grid.Children.Remove(lineL);
            var nodeL = nodeDel.Left;
            var nodeR = nodeDel.Right;
            var nodeP = nodePa.Item1;
            var isRight = nodePa.Item2 > 0;
            var taskL = Task.Factory.StartNew(() =>
            {
                Application.Current.Dispatcher.Invoke(async () => { await RelayoutButtonAfterDeleteAsync(grid, nodeP, isRight, nodeL); });
            });
            var taskR = Task.Factory.StartNew(() =>
            {
                Application.Current.Dispatcher.Invoke(async () => { await RelayoutButtonAfterDeleteAsync(grid, nodeP, isRight, nodeR); });
            });
            await Task.WhenAll(new Task[] { taskL, taskR });
            //}

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="nodeParent"></param>
        /// <param name="isRight"></param>
        /// <param name="node"></param>
        async Task RelayoutButtonAfterDeleteAsync(Grid grid, Node<Student> nodeParent, bool isRight, Node<Student> node = null)
        {
            if (node == null)
            {
                return;
            }
            if (nodeParent != null)
            {
                var nameLine = $"{"Btn" + nodeParent.Data.Id.ToString() + "Btn" + node.Data.Id.ToString()}";
                var line = FindLineInGrid(grid, nameLine);
                grid.Children.Remove(line);
                var remainingSpace = grid.ActualWidth / Math.Pow(2, ((nodeParent.Y + VerticalMarging) / VerticalMarging));
                node.X = nodeParent.X + (isRight == true ? remainingSpace : -remainingSpace);
                node.Y = nodeParent.Y + VerticalMarging;
                DrawLine(grid, nodeParent.X, node.X, nodeParent.Y, node.Y, isRight, nameLine);
            }
            else
            {
                node.X = grid.ActualWidth / 2;
                node.Y = VerticalMarging;
            }
            var button = grid.Children.OfType<Button>().Where(p => p.Name.Equals("Btn" + node.Data.Id.ToString())).Single();
            AnimationButtonMovetTo(node.X, node.Y, button);

            var taskR = Task.Factory.StartNew(() =>
            {
                Application.Current.Dispatcher.Invoke(async () => { await RelayoutButtonAfterDeleteAsync(grid, node, true, node.Right); });
            });
            var taskL = Task.Factory.StartNew(() =>
            {
                Application.Current.Dispatcher.Invoke(async () => { await RelayoutButtonAfterDeleteAsync(grid, node, false, node.Left); });
            });
            await Task.WhenAll(new Task[] { taskL, taskR });
        }

        /// <summary>
        /// Move a button from here to (x,y) with animation
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="button"></param>
        private void AnimationButtonMovetTo(double x, double y, Button button)
        {
            if (button == null)
            {
                return;
            }
            Storyboard sb = new Storyboard();
            ThicknessAnimation animation = new ThicknessAnimation(new Thickness(x, y, 0, 0), TimeSpan.FromSeconds(1));
            Storyboard.SetTarget(animation, button);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            sb.Children.Add(animation);
            sb.Completed += (o, s) =>
            {
                var margin = button.Margin;
                button.BeginAnimation(Button.MarginProperty, null);
                button.Margin = new Thickness(x, y, 0, 0);
            };
            sb.Begin();
        }

        #endregion


    }
}
