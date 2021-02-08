namespace DataStructure.Tree.Model
{
    public class BinaryTreeNode<T> 
    {
        #region Properties
        public T Item { get; set; }
        public BinaryTreeNode<T> Parent { get; set; }
        public BinaryTreeNode<T> LeftChild { get; set; }
        public BinaryTreeNode<T> RightChild { get; set; }
        #endregion
        
        #region Constructor
        public BinaryTreeNode(T item) { Item = item; }
        #endregion
    }
}
