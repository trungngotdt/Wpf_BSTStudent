﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_BSTStudent.Model
{
    public class BSTTree<T> : ITree<T>
      where T : class, IComparable, new()
    {

        private Node<T> root;

        public Node<T> Root
        {
            get
            {
                return root;
            }
            set => root = value;
        }


        #region AddRange
        /// <summary>
        /// Adds the elements of the specified collection to the BST
        /// </summary>
        /// <param name="node"></param>
        public void AddRange(Node<T>[] node)
        {
            foreach (var item in node)
            {
                Insert(item.Data);
            }
        }

        /// <summary>
        /// Adds the elements of the specified collection to the BST
        /// </summary>
        /// <param name="node"></param>
        public void AddRange(T[] data)
        {
            foreach (var item in data)
            {
                Insert(item);
            }
        }
        #endregion        

        #region GetSuccessor
        /// <summary>
        /// Find inorder successor of a BST
        /// </summary>
        /// <returns><seealso cref="Node{T}"/></returns>
        public object Successor()
        {
            return Successor(Root);//GetMin(root.Right);//root.Right.GetMin();
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Find inorder successor of a node 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public object Successor(Node<T> node)
        {
            return GetMin(node.Right);
        }

        #endregion

        #region GetPredecessor
        /// <summary>
        /// Find inorder predecessor of a node
        /// </summary>
        /// <returns></returns>
        public object Predecessor(Node<T> node)
        {
            return GetMax(node.Left);
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Find inorder predecessor of a BST
        /// </summary>
        /// <returns></returns>
        public object Predecessor()
        {
            return Predecessor(Root);
            //throw new NotImplementedException();
        }


        #endregion

        #region GetHeight
        /// <summary>
        /// Find height of node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public async Task< int> HeightAsync(Node<T> node)
        {
            if (node == null) return -1;
            var leftH = HeightAsync(node.Left);
            var rightH = HeightAsync(node.Right);
            await Task.WhenAll(new Task[] {leftH,rightH });
            return Math.Max(leftH.Result, rightH.Result) + 1;
        }

        /// <summary>
        /// Find height of node root
        /// </summary>
        /// <returns></returns>
        public async Task<int> HeightAsync()
        {
            int v = await HeightAsync(Root);
            return v;
        }

        #endregion

        #region GetMin
        /// <summary>
        ///Return a minimum value in node
        /// </summary>
        /// <returns></returns>
        public Node<T> GetMin(Node<T> node)
        {
            var temp = node;
            if (node == null)
            {
                return node;
            }
            while (true)
            {
                if (temp.Left == null)
                {
                    return temp;
                }
                else if (temp.Left != null)
                {
                    temp = temp.Left;
                }
            }
        }
        /// <summary>
        ///Return a minimum value in BST tree
        /// </summary>
        /// <returns></returns>
        public Node<T> GetMin()
        {
            var temp = Root;
            if (Root == null)
            {
                return Root;
            }
            while (true)
            {
                if (temp.Left == null)
                {
                    return temp;
                }
                else if (temp.Left != null)
                {
                    temp = temp.Left;
                }
            }
        }

        #endregion

        #region GetMax
        /// <summary>
        /// Return a maximum value in BST
        /// </summary>
        /// <returns></returns>
        public Node<T> GetMax(Node<T> node)
        {
            var temp = node;
            if (node == null)
            {
                return node;
            }
            while (true)
            {
                if (temp.Right == null)
                {
                    return temp;
                }
                else if (temp.Right != null)
                {
                    temp = temp.Right;
                }
            }
        }

        /// <summary>
        /// Return a maximum value in BST
        /// </summary>
        /// <returns></returns>
        public Node<T> GetMax()
        {
            return GetMax(Root);
        }

        #endregion

        #region Traversal

        public List<string> NRL()
        {
            List<string> list = new List<string>();
            NRL(Root, list);
            return list;
        }

        private void NRL(Node<T> node, List<string> list)
        {
            if (node == null)
            {
                return;
            }
            list.Add(node.Data.ToString());
            NRL(node.Right, list);
            NRL(node.Left, list);
        }

        public List<string> NLR()
        {
            List<string> list = new List<string>();

            NLR(Root, list);
            return list;
        }

        private void NLR(Node<T> node, List<string> list)
        {
            if (node == null)
            {
                return;
            }
            list.Add(node.Data.ToString());
            NLR(node.Left, list);
            NLR(node.Right, list);
        }

        private void LRN(Node<T> node, List<string> list)
        {
            if (node == null)
            {
                return;
            }
            LRN(node.Left, list);
            LRN(node.Right, list);
            list.Add(node.Data.ToString());
        }

        public List<string> LRN()
        {
            List<string> list = new List<string>();
            LRN(Root, list);
            return list;

        }

        private void RLN(Node<T> node, List<string> list)
        {
            if (node == null)
            {
                return;
            }
            RLN(node.Right, list);
            RLN(node.Left, list);
            list.Add(node.Data.ToString());
        }

        public List<string> RLN()
        {
            List<string> list = new List<string>();
            RLN(Root, list);
            return list;

        }

        private void RNL(Node<T> node, List<string> list)
        {
            if (node == null)
            {
                return;
            }
            RNL(node.Right, list);
            list.Add(node.Data.ToString());
            RNL(node.Left, list);
        }

        public List<string> RNL()
        {
            List<string> list = new List<string>();

            RNL(Root, list);
            return list;
        }

        private void LNR(Node<T> node, List<string> list)
        {
            if (node == null)
            {
                return;
            }
            LNR(node.Left, list);
            list.Add(node.Data.ToString());
            LNR(node.Right, list);
        }

        public List<string> LNR()
        {
            List<string> list = new List<string>();
            LNR(Root, list);
            return list;
        }

        #endregion

        #region Contains
        /// <summary>
        /// Determines whether an element is in the BST
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Contains(T data)
        {
            return Contains(new Node<T>(data));
        }

        /// <summary>
        /// Determines whether an element is in the BST
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Contains(Node<T> node)
        {
            Node<T> temp = Root;
            if (node == null)
            {
                return false;
            }
            while (temp != null)
            {
                if (temp.CompareTo(node) == 0)
                {
                    return true;
                }
                if (temp > node)
                {
                    temp = temp.Left;
                }
                else
                {
                    temp = temp.Right;
                }
            }
            return false;
        }
        #endregion

        #region FindNode
        /// <summary>
        /// Searches for the element that matches the conditions defined by the specified
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Node<T> FindNode(T data)
        {
            return FindNode(new Node<T>(data));
        }

        public Node<T> FindNode(Node<T> node)
        {
            return FindNode(root, node);
        }

        /// <summary>
        /// Searches for the element that matches the conditions defined by the specified
        /// </summary>
        /// <param name="node"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Node<T> FindNode(Node<T> nodeRoot, Node<T> node)
        {

            Node<T> temp = nodeRoot;
            if (node == null)
            {
                return null;
            }
            while (temp != null)
            {
                if (temp.CompareTo(node) == 0)
                {
                    return temp;
                }
                if (temp > node)
                {
                    temp = temp.Left;
                }
                else
                {
                    temp = temp.Right;
                }
            }
            return null;

        }

        #endregion

        #region FindParent
        /// <summary>
        /// Searches for an parent of element that matches the conditions defined by the specified
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Tuple<Node<T>, int> FindParent(T data)
        {
            return FindParent(new Node<T>(data));
        }
        /// <summary>
        /// Searches for an parent of element that matches the conditions defined by the specified
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Tuple<Node<T>, int> FindParent(Node<T> node)
        {
            int check = 0;
            if (node == null)
            {
                return null;
            }
            Node<T> temp = Root;
            Node<T> parent = null;
            while (temp != null)
            {
                if (temp.CompareTo(node) == 0)
                {
                    return new Tuple<Node<T>, int>(parent, check);// temp;
                }
                if (temp > node)
                {
                    parent = temp;
                    check = -1;
                    temp = temp.Left;
                }
                else
                {
                    parent = temp;
                    check = 1;
                    temp = temp.Right;

                }
            }
            return null;
        }

        #endregion

        #region Insert

        /// <summary>
        /// Insert a value to
        /// </summary>
        /// <param name="key"></param>
        public void Insert(T key)
        {
            if (key == null)
            {
                return;
            }
            Root = new Node<T>(Insert(Root, new Node<T>(key)));
        }

        public void Insert(Node<T> node)
        {
            if (node == null)
            {
                return;
            }
            Root = Insert(Root, node);
        }

        private Node<T> Insert(Node<T> x, Node<T> key)
        {
            if (x == null)
                return key;
            int cmp = key.CompareTo(x);
            if (cmp < 0)
                x.Left = Insert(x.Left, key);
            else if (cmp > 0)
                x.Right = Insert(x.Right, key);
            else
                x.Data = key.Data;
            
            return x;
        }
                

        #endregion

        #region Remove

        /// <summary>
        /// Remove a element with minimum value in BST
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private Node<T> RemoveMin(Node<T> x)
        {
            if (x.Left == null)
                return x.Right;
            x.Left = RemoveMin(x.Left);
            return x;
        }

        /// <summary>
        /// Remove a minimum value in root
        /// </summary>
        /// <returns></returns>
        public Node<T> RemoveMin()
        {
            return Root = RemoveMin(Root);
        }


        /// <summary>
        /// Remove a element in root -paramater is a object <typeparamref name="T"/>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Remove(T data)
        {
            var x = Remove(Root, data);
            Root = x;
            if (this.Contains(data))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Remove a element in BST -paramater is a object <seealso cref="Node{T}"/>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Remove(Node<T> node)
        {
            if (node == null)
            {
                return false;
            }
            return Remove(node.Data);
        }

        /// <summary>
        /// Remove a element in BST -paramater is a object <seealso cref="Node{T}"/>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Node<T> Remove(Node<T> x, T key)
        {
            if (x == null) return null;
            int cmp = key.CompareTo(x);
            if (cmp < 0)
                x.Left = Remove(x.Left, key);
            else if (cmp > 0)
                x.Right = Remove(x.Right, key);
            else
            {
                if (x.Right == null)
                    return x.Left;
                if (x.Left == null)
                    return x.Right;
                Node<T> t = x;
                x.Data = GetMin(t.Right).Data;
                x.Right = RemoveMin(t.Right);
                x.Left = t.Left;
            }
            
            return x;
        }

        #endregion

   

        #region RemoveRange
        public List<Node<T>> RemoveRange(Node<T>[] node)
        {
            var list = new List<Node<T>>();
            foreach (var item in node)
            {
                var check = Remove(item);
                if (!check)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        #endregion

        #region ToList

        private void ToList(Node<T> node, List<T> list)
        {
            if (node == null)
            {
                return;
            }
            ToList(node.Left, list);
            list.Add(node.Data);
            ToList(node.Right, list);
        }

        /// <summary>
        /// A List with element from minimum to maximum
        /// </summary>
        /// <returns></returns>
        public List<T> ToList()
        {
            List<T> list = new List<T>();
            ToList(Root, list);
            return list;
        }

        #endregion

        public int CompareTo(object obj)
        {
            try
            {
                Node<T> node = obj as Node<T>;
                return Root.Data.CompareTo(node.Data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
