using DataStructure.Tree;
using DataStructure.Tree.Model;
using NUnit.Framework;
using System.Linq;

namespace Test.Tree
{
    [TestFixture]
    public class TestTreeHelper
    {
        public BinaryTreeNode<string> Root { get; set; }

        [SetUp]
        public void SetUp()
        {
            Root = new BinaryTreeNode<string>("A");
            Root.LeftChild = new BinaryTreeNode<string>("B");
            Root.LeftChild.LeftChild = new BinaryTreeNode<string>("D");
            Root.LeftChild.LeftChild.LeftChild = new BinaryTreeNode<string>("F");
            Root.LeftChild.LeftChild.RightChild = new BinaryTreeNode<string>("G");
            Root.RightChild = new BinaryTreeNode<string>("C");
            Root.RightChild.LeftChild = new BinaryTreeNode<string>("E");
        }
        
        [Test]
        public void TestPreOrder()
        {
            // when
            var actual = Root.PreOrder().Select(o=> o.Item);
            var expect = new string[] { "A", "B", "D", "F", "G", "C", "E", };

            // then
            CollectionAssert.AreEqual(actual, expect);
        }

        [Test]
        public void TestInOrder()
        {
            // when
            var actual = Root.InOrder().Select(o => o.Item);
            var expect = new string[] { "F", "D", "G", "B", "A", "E", "C", };

            // then
            CollectionAssert.AreEqual(actual, expect);
        }

        [Test]
        public void TestPostOrder()
        {
            // when
            var actual = Root.PostOrder().Select(o => o.Item);
            var expect = new string[] { "F", "G", "D", "B", "E", "C", "A", };

            // then
            CollectionAssert.AreEqual(actual, expect);
        }
    }
}
