using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure.Tree.Model
{
    public class BinaryTreeNode<T> 
    {
        #region Properties
        /// <summary>
        /// Item
        /// </summary>
        public T Item { get; set; }
        /// <summary>
        /// 부모 노드
        /// </summary>
        public BinaryTreeNode<T> Parent { get; set; }
        /// <summary>
        /// 자식 노드 (Left)
        /// </summary>
        public BinaryTreeNode<T> LeftChild { get; set; }
        /// <summary>
        /// 자식 노드 (rigth)
        /// </summary>
        public BinaryTreeNode<T> RightChild { get; set; }
        #endregion
        
        #region Constructor
        /// <summary>
        /// Consturctor
        /// </summary>
        /// <param name="item"></param>
        public BinaryTreeNode(T item) { Item = item; }
        #endregion

        #region Functions
        /// <summary>
        /// 해당 노드의 모든 하위 노드를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BinaryTreeNode<T>> GetSubTreeNodes()
        {
            yield return this;

            if(LeftChild != null)
            {
                foreach (var child in LeftChild.GetSubTreeNodes())
                    yield return child;
            }
            if(RightChild != null)
            {
                foreach (var child in RightChild.GetSubTreeNodes())
                    yield return child;
            }
        }
        #endregion
    }

    public static class BinaryTreeNodeExt
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

        /// <summary>
        /// 이진트리에서 Item을 찾습니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static BinaryTreeNode<T> Find<T>(this BinaryTreeNode<T> node, T item)
          where T : IComparable
        {
            if (node == default)
                return null;

            var compare = node.Item.CompareTo(item);

            if (compare == 0)
                return node;
            else if (compare > 0)
                return node.LeftChild != null ? node.LeftChild.Find(item) : null;
            else
                return node.RightChild != null ? node.RightChild.Find(item) : null;
        }

        /// <summary>
        /// 이진트리에 Target을 추가합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="target"></param>
        public static void Add<T>(this BinaryTreeNode<T> node, BinaryTreeNode<T> target)
            where T : IComparable
        {
            if (target == default)
                return;

            var compare = node.Item.CompareTo(target.Item);

            if (compare == 0)
                return;
            else if(compare > 0)
            {
                if (node.LeftChild == null)
                {
                    node.LeftChild = target;
                    target.Parent = node;
                }
                else
                    node.LeftChild.Add(target);
            }
            else if (compare < 0)
            {
                if (node.RightChild == null)
                {
                    node.RightChild = target;
                    target.Parent = node;
                }
                else
                    node.RightChild.Add(target);
            }
        }

        /// <summary>
        /// 이진트리에 Item을 추가합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="item"></param>
        public static void Add<T>(this BinaryTreeNode<T> node, T item) where T : IComparable
            => Add(node, new BinaryTreeNode<T>(item));

        /// <summary>
        /// 이진트리에서 Item을 제거합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="item"></param>
        public static void Delete<T>(this BinaryTreeNode<T> node, T item)
            where T : IComparable
        {
            var target = Find(node, item);
            if (target == null)
                return;

            // 자식노드가 없는 경우
            if (target.LeftChild == null && target.RightChild == null)
                target.Replace(null);
            // 오른쪽 자식노드만 존재하는 경우
            else if (target.LeftChild == null && target.RightChild != null)
                target.Replace(target.RightChild);
            // 왼쪽 자식노드만 존재하는 경우
            else if (target.LeftChild != null && target.RightChild == null)
                target.Replace(target.LeftChild);
            // 왼쪽, 오른쪽 모두 자식노드가 존재하는 경우
            else
                target.Replace(CreateTree(target.GetSubTreeNodes().Where(n => n != target)));
        }

        /// <summary>
        /// 입력된 Node 목록으로 Tree를 생성합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static BinaryTreeNode<T> CreateTree<T>(IEnumerable<BinaryTreeNode<T>> nodes)
            where T : IComparable
        {
            var ordered = nodes.OrderBy(n => n.Item).ToList();
            var idx = (int)(ordered.Count / 2);

            var root = ordered[idx];
            var others = ordered
                .Where(i => i != root)
                .Select(i => i.Item);

            foreach (var other in others)
                root.Add(other);

            return root;
        }

        /// <summary>
        /// Node를 Target으로 대체합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="target"></param>
        public static void Replace<T>(this BinaryTreeNode<T> node, BinaryTreeNode<T> target)
        {
            if (node.Parent.LeftChild == node)
            {
                node.Parent.LeftChild = target;
                target.Parent = node.Parent;
            }
            else if (node.Parent.RightChild == node)
            {
                node.Parent.RightChild = target;
                target.Parent = node.Parent;
            }

            return;
        }
    }
}
