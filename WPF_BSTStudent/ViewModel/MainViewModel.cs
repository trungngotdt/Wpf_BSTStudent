using FizzWare.NBuilder;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LinqToExcel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WPF_BSTStudent.Model;

namespace WPF_BSTStudent.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private string name;
        private string avgMark;
        private string accumulationCredit;
        private DateTime birthDay = DateTime.Now;
        private string id;

        private string nameUpdate;
        private string avgMarkUpdate;
        private string accumulationCreditUpdate;
        private DateTime birthDayUpdate = DateTime.Now;
        private string idUpdate;

        private string txbFindName;
        private string txbFindAvgMark;
        private string txbFindAccumulationCredit;
        private DateTime dpkFindBirthDay = DateTime.Now;
        private string txbFindId;

        private Node<Student> nodeRoot;
        private int numbeFind;
        private int numBeDelete;
        private bool isRdbFindId;
        private bool isRdbFindName;
        private bool isRdbFindAvgMark;
        private bool isRdbFindAccumulationCredit;
        private bool isRdbFindBirthDay;
        private bool isRdbSuperLeft;
        private bool isRdbSuperRight;
        private bool isRdbNormal;
        private bool isTxbDeleteNode;
        private bool isTxbUpdateStudent = true;
        private bool isToggleBtnUpdate;
        private bool isTxbUpdateName;
        private bool isTxbUpdateAvgMark;
        private bool isTxbUpdateAccumulationCredit;
        private bool isDpkUpdateBirthDay;
        private bool isTxbIdAdd;
        private bool isTxbNameAdd;
        private bool isTxbAvgMarkAdd;
        private bool isTxbAccumulationCreditAdd;
        private bool isDpkBirthDayAdd;
        private bool isCkbAddArray = false;
        private bool isCkbDeleteArray = false;
        private readonly int VerticalMarging = 100;
        private readonly int HorizontalMarging = 50;
        private double heightGridBST;
        private double widthGridBST;

        private ICommand btnAddNodeClickCommand;
        private ICommand btnFindNodeClickCommand;
        private ICommand btnDeleteNodeClickCommand;
        private ICommand btnUpdateClickCommand;
        private ICommand btnTraversal;
        private ICommand btnGenerateData;
        private ICommand btnClear;
        private ICommand expClearData;

        public string IdUpdate { get => idUpdate; set => idUpdate = value; }
        public string NameUpdate { get => nameUpdate; set => nameUpdate = value; }
        public string AvgMarkUpdate { get => avgMarkUpdate; set => avgMarkUpdate = value; }
        public string AccumulationCreditUpdate { get => accumulationCreditUpdate; set => accumulationCreditUpdate = value; }
        public DateTime BirthDayUpdate { get => birthDayUpdate; set => birthDayUpdate = value; }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string AvgMark { get => avgMark; set => avgMark = value; }
        public string AccumulationCredit { get => accumulationCredit; set => accumulationCredit = value; }
        public DateTime BirthDay { get => birthDay; set => birthDay = value; }

        public int NumbeFind { get => numbeFind; set => numbeFind = value; }
        public int NumBeDelete { get => numBeDelete; set => numBeDelete = value; }
        public Node<Student> NodeRoot { get => nodeRoot; set => nodeRoot = value; }
        public double WidthGridBST { get => widthGridBST; set => widthGridBST = value; }
        public double HeightGridBST { get => heightGridBST; set => heightGridBST = value; }

        public bool IsTxbDeleteNode { get => isTxbDeleteNode; set => isTxbDeleteNode = value; }
        public bool IsToggleBtnUpdate { get => isToggleBtnUpdate; set => isToggleBtnUpdate = value; }
        public bool IsTxbUpdateStudent { get => isTxbUpdateStudent; set => isTxbUpdateStudent = value; }
        public bool IsTxbUpdateName { get => isTxbUpdateName; set => isTxbUpdateName = value; }
        public bool IsTxbUpdateAvgMark { get => isTxbUpdateAvgMark; set => isTxbUpdateAvgMark = value; }
        public bool IsDpkUpdateBirthDay { get => isDpkUpdateBirthDay; set => isDpkUpdateBirthDay = value; }
        public bool IsTxbUpdateAccumulationCredit { get => isTxbUpdateAccumulationCredit; set => isTxbUpdateAccumulationCredit = value; }
        public bool IsCkbAddArray
        {
            get
            {
                ChangeStateTxbAdd(isCkbAddArray);
                return isCkbAddArray;
            }
            set => isCkbAddArray = value;
        }
        public bool IsCkbDeleteArray
        {
            get
            {
                ChangeStateTxbDelete(isCkbDeleteArray);
                return isCkbDeleteArray;
            }
            set => isCkbDeleteArray = value;
        }
        public bool IsTxbIdAdd { get => isTxbIdAdd; set => isTxbIdAdd = value; }
        public bool IsTxbNameAdd { get => isTxbNameAdd; set => isTxbNameAdd = value; }
        public bool IsTxbAvgMarkAdd { get => isTxbAvgMarkAdd; set => isTxbAvgMarkAdd = value; }
        public bool IsTxbAccumulationCreditAdd { get => isTxbAccumulationCreditAdd; set => isTxbAccumulationCreditAdd = value; }
        public bool IsDpkBirthDayAdd { get => isDpkBirthDayAdd; set => isDpkBirthDayAdd = value; }
        public bool IsRdbSuperLeft { get => isRdbSuperLeft; set => isRdbSuperLeft = value; }
        public bool IsRdbSuperRight { get => isRdbSuperRight; set => isRdbSuperRight = value; }
        public bool IsRdbNormal { get => isRdbNormal; set => isRdbNormal = value; }
        public bool IsRdbFindId { get => isRdbFindId; set => isRdbFindId = value; }
        public bool IsRdbFindName { get => isRdbFindName; set => isRdbFindName = value; }
        public bool IsRdbFindAvgMark { get => isRdbFindAvgMark; set => isRdbFindAvgMark = value; }
        public bool IsRdbFindAccumulationCredit { get => isRdbFindAccumulationCredit; set => isRdbFindAccumulationCredit = value; }
        public bool IsRdbFindBirthDay { get => isRdbFindBirthDay; set => isRdbFindBirthDay = value; }
        public string TxbFindName { get => txbFindName; set => txbFindName = value; }
        public string TxbFindAvgMark { get => txbFindAvgMark; set => txbFindAvgMark = value; }
        public string TxbFindAccumulationCredit { get => txbFindAccumulationCredit; set => txbFindAccumulationCredit = value; }
        public DateTime DpkFindBirthDay { get => dpkFindBirthDay; set => dpkFindBirthDay = value; }
        public string TxbFindId { get => txbFindId; set => txbFindId = value; }
        #region Command

        public ICommand BtnAddNodeClickCommand
        {
            get
            {
                return btnAddNodeClickCommand = new RelayCommand<UIElement>(async (p) =>
                {
                    Student student;
                    if (IsCkbAddArray)
                    {
                        StringBuilder builder = new StringBuilder();
                        var list = GetDataFromExcel();
                        for (int i = 0; i < list.Length; i++)
                        {
                            if ((p as Grid).Children.OfType<Button>().Where(pa => pa.Name.Equals($"Btn{list[i].Id.ToString()}")).ToList().Count != 0)
                            {
                                builder.Append(list[i].ToString() + "\n");
                                continue;
                            }
                            student = new Student(list[i].Id, list[i].Name, list[i].BirthDay, list[i].AvgMark, list[i].AccumulationCredit);
                            await AddButtonGridAsync(p as Grid, student);
                            await Task.Delay(1000);
                        }
                        if (builder.Length != 0)
                        {
                            MessageBox.Show("We can't add : \n" + builder.ToString());
                        }
                        return;
                    }
                    if ((Id.Trim().Length == 0) || (p as Grid).Children.OfType<Button>().Where(pa => pa.Name.Equals($"Btn{Id.ToString()}")).ToList().Count != 0)
                    {
                        MessageBox.Show($"We can't add {Id.ToString()}");
                        return;
                    }
                    student = new Student(int.Parse(Id), Name, BirthDay, float.Parse(AvgMark), int.Parse(AccumulationCredit));
                    await AddButtonGridAsync(p as Grid, student);
                });
            }
        }

        public ICommand BtnFindNodeClickCommand
        {
            get
            {
                return btnFindNodeClickCommand = new RelayCommand<object[]>((p) =>
                {
                    
                    string propertyContent = "";
                    var propertyName = (p[1] as WrapPanel).Children.OfType<RadioButton>().Where(r => r.IsChecked == true && r.Name.StartsWith("RdbFind")).FirstOrDefault()?.Name.Substring(7);
                    if (propertyName == null)
                    {
                        MessageBox.Show("Test");
                        return;
                    }
                    propertyContent = (p[1] as WrapPanel).Children.OfType<TextBox>().Where(t => t.IsEnabled).FirstOrDefault().Text;

                    List<Student> students = NodeRoot.ToList().Where(pr => pr.GetType().GetProperty(propertyName).GetValue(pr, null).ToString().Equals(propertyContent)).ToList();
                    if (students == null)
                    {
                        MessageBox.Show("Test");
                        return;
                    }
                    for (int i = 0; i < students.Count; i++)
                    {
                        FindNodeInGrid(new Node<Student>(students[i]), p[0] as Grid);
                    }

                });
            }
        }

        public ICommand BtnDeleteNodeClickCommand
        {
            get
            {
                return btnDeleteNodeClickCommand = new RelayCommand<Grid>(async (p) =>
                {

                    if (IsCkbDeleteArray)
                    {
                        var list = GetDataFromExcel();
                        for (int i = 0; i < list.Length; i++)
                        {
                            await HelpCommandDeleteAsync(p, list[i].Id);
                        }
                        return;
                    }
                    await HelpCommandDeleteAsync(p, NumBeDelete);
                    /*
                    if ((NumBeDelete == null) || (p as Grid).Children.OfType<Button>().Where(pa => pa.Name.Equals($"Btn{NumBeDelete.ToString()}")).ToList().Count == 0)
                    {
                        MessageBox.Show($"We can't delete {NumBeDelete.ToString()}");
                        return;
                    }

                    await DeleteNodeInGridAsync(p, NumBeDelete);
                    */
                });
            }
        }

        private async Task HelpCommandDeleteAsync(Grid grid, int id)
        {
            if ((id == null) || (grid as Grid).Children.OfType<Button>().Where(pa => pa.Name.Equals($"Btn{id.ToString()}")).ToList().Count == 0)
            {
                MessageBox.Show($"We can't delete {id.ToString()}");
                return;
            }

            await DeleteNodeInGridAsync(grid, NumBeDelete);
        }

        public ICommand BtnUpdateClickCommand
        {
            get
            {
                return btnUpdateClickCommand = new RelayCommand<Grid>(async (p) =>
                {
                    var studentUpdate = NodeRoot.FindNode(new Student(int.Parse(Id)));
                    if (studentUpdate == null || IsToggleBtnUpdate == true)//State :Find the student
                    {
                        var button = FindButtonInGrid(p, Id);
                        studentUpdate.Name = NameUpdate;
                        studentUpdate.BirthDay = BirthDayUpdate;
                        studentUpdate.AvgMark = int.Parse(AvgMarkUpdate);
                        studentUpdate.AccumulationCredit = int.Parse(AccumulationCreditUpdate);
                        ChangeStateTxbUpdate(false);
                        (await button).Item2.ToolTip = studentUpdate.ToString();
                    }
                    else//State :Update
                    {
                        NameUpdate = studentUpdate.Name;
                        AvgMarkUpdate = studentUpdate.AvgMark.ToString();
                        AccumulationCreditUpdate = studentUpdate.AccumulationCredit.ToString();
                        BirthDayUpdate = studentUpdate.BirthDay;
                        ChangeContentTxbUpdate();
                        ChangeStateTxbUpdate(true);
                    }
                });
            }
        }

        public ICommand BtnTraversal
        {
            get
            {
                return btnTraversal = new RelayCommand<string>((p) =>
                  {
                      var returnList = NodeRoot.GetType().GetMethod(p, new Type[] { }).Invoke(NodeRoot, new object[] { });
                      ShowMessBoxTraversal(returnList as List<string>, p);
                  });
            }
        }

        public ICommand BtnGenerateData
        {
            get
            {

                return btnGenerateData = new RelayCommand<UIElement>(async (p) =>
                  {
                      StringBuilder builder = new StringBuilder();
                      List<Student> list = new List<Student>();
                      if (IsRdbNormal)
                      {
                          list = GetData(5);
                      }
                      else if (isRdbSuperLeft)
                      {
                          list = GetRandomData<Student>(5);
                          list.Reverse();
                      }
                      else if (IsRdbSuperRight)
                      {
                          list = GetRandomData<Student>(5);
                      }
                      else
                      {
                          MessageBox.Show("Don't");
                          return;
                      }
                    //var list = GetData(5);
                    Student student;
                      for (int i = 0; i < list.Count(); i++)
                      {
                          if ((p as Grid).Children.OfType<Button>().Where(pa => pa.Name.Equals($"Btn{list[i].Id.ToString()}")).ToList().Count != 0)
                          {
                              builder.Append(list[i].ToString() + "\n");
                              continue;
                          }
                          student = new Student(list[i].Id, list[i].Name, list[i].BirthDay, list[i].AvgMark, list[i].AccumulationCredit);
                          await AddButtonGridAsync(p as Grid, student);
                          await Task.Delay(1000);
                      }
                      if (builder.Length != 0)
                      {
                          MessageBox.Show("We can't add : \n" + builder.ToString());
                      }
                      return;
                  });
            }
        }

        public ICommand BtnClear
        {
            get
            {
                return btnClear=new RelayCommand<UIElement>((p)=> 
                {
                    (p as Grid).Children.Clear();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                });
            }
        }

        public ICommand ExpClearData
        {
            get
            {
                return expClearData=new RelayCommand<string>((p)=>
                {
                    this.GetType().GetMethod("ClearData" + p, BindingFlags.NonPublic|BindingFlags.Instance).Invoke(this,new object[] { });
                });
            }
        }

        




        #endregion

        #region Helper
        void ShowMessBoxTraversal(List<string> list, string name)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                builder.Append(list[i]);
                builder.Append("\n");
            }
            MessageBox.Show(builder.ToString(), $"Traversal {name}", MessageBoxButton.OK);
        }

        private void ClearDataExpAdd()
        {
            IsCkbAddArray = false;
            RaisePropertyChanged("IsCkbAddArray");
            Id = "";
            Name = "";
            AccumulationCredit = "";
            AvgMark = "";
            BirthDay = DateTime.Now;
            ChangeContentTxbAdd();
        }

        private void ClearDataExpFind()
        {
            TxbFindId = "";
            TxbFindAvgMark = "";
            TxbFindName = "";
            TxbFindAccumulationCredit = "";
            DpkFindBirthDay = DateTime.Now;
            ChangeContentTxbFind();
            IsRdbFindAccumulationCredit = false;
            IsRdbFindAvgMark = false;
            IsRdbFindBirthDay = false;
            IsRdbFindId = false;
            IsRdbFindName = false;
            ChangeStateRdbFind();
        }

        private void ClearDataExpDelete()
        {
            NumBeDelete = 0;
            IsCkbDeleteArray = false;
            RaisePropertyChanged("NumBeDelete");
            RaisePropertyChanged("IsCkbDeleteArray");
        }

        private void ClearDataExpUpdate()
        {
            IdUpdate = "";
            NameUpdate = "";
            AccumulationCreditUpdate = "";
            AvgMarkUpdate = "";
            BirthDayUpdate = DateTime.Now;
            ChangeContentTxbUpdate();
            ChangeStateTxbUpdate(false);

        }

        private void ClearDataExpGenerateData()
        {
            IsRdbNormal = false;
            IsRdbSuperLeft = false;
            IsRdbSuperRight = false;
            RaisePropertyChanged("IsRdbSuperRight");
            RaisePropertyChanged("IsRdbSuperLeft");
            RaisePropertyChanged("IsRdbNormal");
        }

        private List<T> GetRandomData<T>(int size)
        {
            var list = Builder<T>.CreateListOfSize(size).Build().ToList();

            return list;
        }

        private List<Student> GetData(int size)
        {
            Random random = new Random();
            var list = GetRandomData<Student>(size);
            Parallel.ForEach(list, (item) =>
            {
                item.Id = item.Id - random.Next(0, 500) / 2 + random.Next(0, 500) + random.Next(0, 500) * 2;
                float mark = (random.Next(0, 10) / 1.0f) + 10.0f / (random.Next(0, 99) * 1.0f);
                item.AvgMark = float.Parse(String.Format("{0:0.00}", mark));
            });
            return list;
        }

        public void ChangeStateTxbDelete(bool isenable)
        {
            IsTxbDeleteNode = !isenable;
            RaisePropertyChanged("IsTxbDeleteNode");
        }

        private void ChangeStateRdbFind()
        {
            RaisePropertyChanged("IsRdbFindId");
            RaisePropertyChanged("IsRdbFindAvgMark");
            RaisePropertyChanged("IsRdbFindName");
            RaisePropertyChanged("IsRdbFindAccumulationCredit");
            RaisePropertyChanged("IsRdbFindBirthDay");
        }

        private void ChangeContentTxbAdd()
        {
            RaisePropertyChanged("Id");
            RaisePropertyChanged("AvgMark");
            RaisePropertyChanged("Name");
            RaisePropertyChanged("AccumulationCredit");
            RaisePropertyChanged("BirthDay");
        }

        private void ChangeContentTxbFind()
        {
            RaisePropertyChanged("TxbFindId");
            RaisePropertyChanged("TxbFindAvgMark");
            RaisePropertyChanged("TxbFindName");
            RaisePropertyChanged("TxbFindAccumulationCredit");
            RaisePropertyChanged("DpkFindBirthDay");
        }

        public void ChangeContentTxbUpdate()
        {
            RaisePropertyChanged("IdUpdate");
            RaisePropertyChanged("AvgMarkUpdate");
            RaisePropertyChanged("NameUpdate");
            RaisePropertyChanged("AccumulationCreditUpdate");
            RaisePropertyChanged("BirthDayUpdate");
        }

        public void ChangeStateTxbAdd(bool isenable)
        {
            IsTxbIdAdd = !isenable;
            IsTxbAccumulationCreditAdd = !isenable;
            IsTxbNameAdd = !isenable;
            IsTxbAvgMarkAdd = !isenable;
            IsDpkBirthDayAdd = !isenable;
            RaisePropertyChanged("IsTxbIdAdd");
            RaisePropertyChanged("IsTxbAvgMarkAdd");
            RaisePropertyChanged("IsTxbNameAdd");
            RaisePropertyChanged("IsTxbAccumulationCreditAdd");
            RaisePropertyChanged("IsDpkBirthDayAdd");
        }

        public void ChangeStateTxbUpdate(bool isenable)
        {
            IsToggleBtnUpdate = !isenable;
            IsTxbUpdateStudent = !isenable;
            IsTxbUpdateName = isenable;
            IsTxbUpdateAvgMark = isenable;
            IsTxbUpdateAccumulationCredit = isenable;
            IsDpkUpdateBirthDay = isenable;
            RaisePropertyChanged("IsTxbUpdateStudent");
            RaisePropertyChanged("IsTxbUpdateAvgMark");
            RaisePropertyChanged("IsTxbUpdateName");
            RaisePropertyChanged("IsTxbUpdateAccumulationCredit");
            RaisePropertyChanged("IsDpkUpdateBirthDay");
            RaisePropertyChanged("IsToggle");
        }

        public Student[] GetDataFromExcel()
        {
            Student[] students;
            var open = new OpenFileDialog() { Filter = "Excel Workbook[97-2003] | *.xls|Excel Workbook|*.xlsx", ValidateNames = true };
            int i = 0;
            if (open.ShowDialog() == true)
            {
                string fileName = open.FileName;
                var excelFile = new ExcelQueryFactory(fileName);
                var dataExcel = from a in excelFile.Worksheet() select a;
                students = new Student[dataExcel.Count()];
                foreach (var a in dataExcel)
                {
                    try
                    {
                        var ID = a[0].Cast<int>();
                        var Name = a[1].Value.ToString();
                        var AvgMark = a[2].Cast<float>();
                        var AccumulationCredit = a[3].Cast<int>();
                        var BirthDay = a[4].Cast<DateTime>();
                        Student student = new Student(ID, Name, BirthDay, AvgMark, AccumulationCredit);
                        //tree.Insert(new Node<Student>(student));
                        students[i] = student;
                        i++;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        MessageBox.Show("Something wrong with data !Please try again !");
                    }
                }
                return students;
            }
            return null;
        }
        #endregion
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            HeightGridBST = 705;
            WidthGridBST = 1138.3333333333335;
            this.IsToggleBtnUpdate = true;
        }

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
        private async Task DrawLine(Grid grid, double x1, double x2, double y1, double y2, bool isRightLeaf, string name)
        {
            await Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Line l = new Line
                        {
                            Stroke = new SolidColorBrush(Colors.Black),
                            StrokeThickness = 2.0,
                            Name = name,
                            X1 = x1 + (isRightLeaf ? 25 : 25),
                            X2 = x1 + (isRightLeaf ? 25 : 25),
                            Y1 = y1 + 50,
                            Y2 = y1 + 50
                        };

                        AnimationGrowLine(x2 + (isRightLeaf ? 25 : 25), y2 /*+25*/, TimeSpan.FromSeconds(1), l);
                        grid.Children.Add(l);
                    });
                    return;
                }
            });
        }

        /// <summary>
        /// Grow a line to x2,y2
        /// </summary>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="time">The time the line will be grow</param>
        /// <param name="line"></param>
        public void AnimationGrowLine(double x2, double y2, TimeSpan time, Line line)
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
        private async Task AddButtonGridAsync(UIElement p, Student student)
        {
            double x = 0;
            double y = 0;
            if ((p as Grid).Children.OfType<Button>().Count<Button>() == 0)
            {
                nodeRoot = new Node<Student>(student, (p as Grid).ActualWidth / 2, VerticalMarging);
                AddNode(p as Grid, student, new Thickness((p as Grid).ActualWidth / 2, VerticalMarging, 0, 0));
                return;
            }
            if (NodeRoot.Contains(new Node<Student>(student)))
            {
                return;
            }
            var node = new Node<Student>(student, x, y);
            NodeRoot.Insert(node);
            var checkExitsParent = nodeRoot.FindParent(new Node<Student>(student));
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
                if (x + 50 >= WidthGridBST || x - 50 <= 0)
                {
                    ResizeGrid();
                    await ReLayoutAllButtonAsync(p as Grid);
                }
                await DrawLine(p as Grid, checkExitsParent.Item1.X, node.X, checkExitsParent.Item1.Y, node.Y,
                    checkExitsParent.Item2 > 0, $"{"Btn" + checkExitsParent.Item1.Data.Id.ToString() + "Btn" + node.Data.Id.ToString() }");

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
                Button button = new Button() { Name = "Btn" + data.Id.ToString(), Content = data.Id.ToString(), ToolTip = data.ToString() };

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
                        Student student = new Student(int.Parse(p.Content.ToString()), "", new DateTime(), 0, 0);
                        NodeRoot.FindNode(new Node<Student>(student)).X = Left;
                        //Tree.FindNode(new Node<int>(int.Parse(p.Content.ToString()))).X = Left;
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
                        var number1 = Regex.Split(name, "Btn")[1]; //Get first number
                        var number2 = Regex.Split(name, "Btn")[2];//Get last number
                        p.BeginAnimation(Line.X2Property, null);//Animation be removed
                        if (int.Parse(number2) > int.Parse(number1))//Right
                        {
                            p.X1 = p.X1 * 2 - 25;
                            p.X2 = X2 * 2 - 25;
                        }
                        else//Left
                        {
                            p.X2 = X2 * 2 - 25;
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
            RaisePropertyChanged("WidthGridBST");
            RaisePropertyChanged("HeightGridBST");
        }

        #endregion

        /// <summary>
        /// Draw the round button
        /// </summary>
        /// <param name="button"></param>
        public void RoundButton(Button button)
        {
            Style style = Application.Current.FindResource("MaterialDesignFloatingActionDarkButton") as Style;
            button.Style = style;
        }
        #endregion

        #region Find a Node (button) in grid

        /// <summary>
        /// Function for btnFindNodeClickCommand
        /// It will find a node-button in grid and draw a circle around button 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="grid"></param>
        private void FindNodeInGrid(Node<Student> node, UIElement grid)
        {
            var nodeBeFind = NodeRoot.FindNode(node);
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
        /// Create a circle in 10 seconds
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
                        Stroke = new SolidColorBrush(Colors.Black),
                        Width = 60,
                        Height = 60,
                        StrokeThickness = 1.0
                    };
                    (grid as Grid).Children.Add(ellipse);

                });
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

        private async Task DeleteNodeInGridAsync(Grid grid, int nodeDelete)
        {
            var tup = FindButtonInGrid(grid, nodeDelete);
            //Task.Factory.ContinueWhenAll(tup.Result.Item1.ToArray(), p => { });
            await tup.ContinueWith(p =>
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    var nodeDe = NodeRoot.FindNode(new Node<Student>(new Student(nodeDelete)));
                    if (nodeDe.Left != null && nodeDe.Right != null)
                    {
                        var nodeSucc = nodeDe.FindNode(new Node<Student>(new Student((int)nodeDe.Successor())));
                        var buttonSucc = grid.Children.OfType<Button>().Where(s => s.Content.Equals(nodeSucc.Data.ToString())).FirstOrDefault();
                        AnimationButtonMovetTo(nodeDe.X, nodeDe.Y, buttonSucc);//to move a successor to new position (the button will be deleted)
                        var nodeDelPar = new Node<Student>();
                        Task taskFindParent = Task.Factory.StartNew(() => { nodeDelPar = NodeRoot.FindParent(new Node<Student>(new Student(nodeDelete))).Item1; });
                        Task task = Task.Factory.StartNew(() =>
                        {
                            Application.Current.Dispatcher.Invoke(() => { UpdateButtonAfterDeleteAsync(grid, nodeSucc.Data.Id); });
                        });
                        grid.Children.Remove(p.Result.Item2);

                        await Task.Factory.ContinueWhenAll(new Task[] { task, taskFindParent }, t =>
                        {
                            NodeRoot.Remove(new Node<Student>(new Student(NumBeDelete)));
                        });
                        grid.Children.OfType<Line>().Where(l => l.Name.Contains($"{"Btn" + nodeDelete.ToString()}")).ToList().ForEach((item) =>
                        {
                            item.Name = item.Name.Replace($"{"Btn" + nodeDelete.ToString()}", $"{"Btn" + nodeSucc.Data.ToString()}");
                        });
                    }
                    else
                    {
                        Task task = Task.Factory.StartNew(() =>
                        {
                            Application.Current.Dispatcher.Invoke(() => { UpdateButtonAfterDeleteAsync(grid, nodeDelete); });
                        });
                        await Task.Factory.ContinueWhenAll(new Task[] { task }, t =>
                        {

                            if (NodeRoot.FindParent(new Node<Student>(new Student(nodeDelete))).Item1 == null && nodeDe.Right == null && nodeDe.Left == null)
                            {
                                NodeRoot = null;
                            }
                            else
                            {
                                NodeRoot.Remove(new Node<Student>(new Student(NumBeDelete)));
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
        Line FindLineInGrid(Grid grid, string name)
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
        async Task<Tuple<List<Task>, Button>> FindButtonInGrid(Grid grid, object match)
        {
            Button button = null;
            List<Task> listTask = new List<Task>();
            var allButtonInGrid = grid.Children.OfType<Button>().ToList();
            for (int i = 0; i < allButtonInGrid.Count; i++)//Find a button will be remove
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
        async void UpdateButtonAfterDeleteAsync(Grid grid, int nodeDelete)
        {
            var nodeDel = NodeRoot.FindNode(new Node<Student>(new Student(nodeDelete)));
            //Delete button have one or none child
            var nodePa = NodeRoot.FindParent(new Node<Student>(nodeDel.Data));//Parent of nodeDelete
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
                Application.Current.Dispatcher.Invoke(() => { RelayoutButtonAfterDeleteAsync(grid, nodeP, isRight, nodeL); });
            });
            var taskR = Task.Factory.StartNew(() =>
            {
                Application.Current.Dispatcher.Invoke(() => { RelayoutButtonAfterDeleteAsync(grid, nodeP, isRight, nodeR); });
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
        async void RelayoutButtonAfterDeleteAsync(Grid grid, Node<Student> nodeParent, bool isRight, Node<Student> node = null)
        {
            if (node == null)
            {
                return;
            }
            Task taskDraw = null;
            if (nodeParent != null)
            {
                var nameLine = $"{"Btn" + nodeParent.Data.Id.ToString() + "Btn" + node.Data.Id.ToString()}";
                var line = FindLineInGrid(grid, nameLine);
                grid.Children.Remove(line);
                var remainingSpace = grid.ActualWidth / Math.Pow(2, ((nodeParent.Y + VerticalMarging) / VerticalMarging));
                node.X = nodeParent.X + (isRight == true ? remainingSpace : -remainingSpace);
                node.Y = nodeParent.Y + VerticalMarging;
                taskDraw = DrawLine(grid, nodeParent.X, node.X, nodeParent.Y, node.Y, isRight, nameLine);
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
                Application.Current.Dispatcher.Invoke(() => { RelayoutButtonAfterDeleteAsync(grid, node, true, node.Right); });
            });
            var taskL = Task.Factory.StartNew(() =>
            {
                Application.Current.Dispatcher.Invoke(() => { RelayoutButtonAfterDeleteAsync(grid, node, false, node.Left); });
            });
            if (nodeParent != null)
            {
                await Task.WhenAll(new Task[] { taskL, taskR, taskDraw });
            }
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



        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}