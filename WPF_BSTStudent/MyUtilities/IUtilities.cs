
namespace WPF_BSTStudent.MyUtilities
{
    public interface IUtilities
    {
        Model.ITree<Model.Student> Tree { get; set; }
        int VerticalMarging { get; set; }
        double WidthGridBST { get; set; }
        double HeightGridBST { get; set; }

        /// <summary>
        /// To calculate new position and button will be added to Grid
        /// </summary>
        /// <param name="p">this is a grid which will be add a button</param>
        /// <param name="student"></param>
        System.Threading.Tasks.Task AddButtonGridAsync(System.Windows.UIElement p, Model.Student student);

        /// <summary>
        /// Function for btnFindNodeClickCommand
        /// It will find a node-button in grid and draw a circle around button 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="grid"></param>
        void FindNodeInGrid(Model.Node<Model.Student> node, System.Windows.UIElement grid);

        /// <summary>
        /// Delete the student ( id is <paramref name="nodeDelete"/> ) in <paramref name="grid"/>
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="nodeDelete"></param>
        System.Threading.Tasks.Task DeleteNodeInGridAsync(System.Windows.Controls.Grid grid, int nodeDelete);

        /// <summary>
        /// Find the Button in grid  - async
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="match">name of the button</param>
        /// <returns></returns>
        System.Threading.Tasks.Task<System.Tuple<System.Collections.Generic.List<System.Threading.Tasks.Task>, System.Windows.Controls.Button>> FindButtonInGrid(System.Windows.Controls.Grid grid, object match);
    }
}
