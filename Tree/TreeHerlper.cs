using DataStructure.Tree.Model;
using System.Collections.Generic;

namespace DataStructure.Tree
{
    public static class TreeHerlper
    {
        /// <summary>
        /// 전위탐색
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IEnumerable<BinaryTreeNode<T>> PreOrder<T>(this BinaryTreeNode<T> root)
        {
            if (root != null)
            {
                yield return root;

                foreach (var leftChild in PreOrder(root.LeftChild))
                    yield return leftChild;

                foreach (var rightChild in PreOrder(root.RightChild))
                    yield return rightChild;
            }
        }

        /// <summary>
        /// 중위탐색
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IEnumerable<BinaryTreeNode<T>> InOrder<T>(this BinaryTreeNode<T> root)
        {
            if (root != null)
            {
                foreach (var leftChild in InOrder(root.LeftChild))
                    yield return leftChild;

                yield return root;

                foreach (var rightChild in InOrder(root.RightChild))
                    yield return rightChild;
            }
        }

        /// <summary>
        /// 후위탐색
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IEnumerable<BinaryTreeNode<T>> PostOrder<T>(this BinaryTreeNode<T> root)
        {
            if (root != null)
            {
                foreach (var leftChild in PostOrder(root.LeftChild))
                    yield return leftChild;
                
                foreach (var rightChild in PostOrder(root.RightChild))
                    yield return rightChild;

                yield return root;
            }
        }
    }
}
