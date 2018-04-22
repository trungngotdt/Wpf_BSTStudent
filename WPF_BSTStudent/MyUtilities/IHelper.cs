using System.Collections.Generic;
using WPF_BSTStudent.Model;

namespace WPF_BSTStudent.MyUtilities
{
    public interface IHelper
    {
        void ShowMessBoxTraversal(List<string> list, string name);

        List<T> GetRandomData<T>(int size);

        List<Student> GetData(int size);

        Student[] GetDataFromExcel();
    }
}
