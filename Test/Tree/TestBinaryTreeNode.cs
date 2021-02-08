using DataStructure.Tree.Model;
using NUnit.Framework;
using System.Linq;

namespace Test.Tree
{
    [TestFixture]
    public class TestBinaryTreeNode
    {
        public BinaryTreeNode<int> Root { get; set; }

        [SetUp]
        public void SetUp()
        {
            Root = new BinaryTreeNode<int>(1);
            Root.LeftChild = new BinaryTreeNode<int>(2);
            Root.LeftChild.LeftChild = new BinaryTreeNode<int>(4);
            Root.LeftChild.LeftChild.LeftChild = new BinaryTreeNode<int>(6);
            Root.LeftChild.LeftChild.RightChild = new BinaryTreeNode<int>(7);
            Root.RightChild = new BinaryTreeNode<int>(3);
            Root.RightChild.LeftChild = new BinaryTreeNode<int>(5);
        }
        
        [Test]
        public void TestPreOrder()
        {
            // when
            var actual = Root.PreOrder().Select(o=> o.Item);
            var expect = new int[] { 1, 2, 4, 6, 7, 3, 5, };

            // then
            CollectionAssert.AreEqual(actual, expect);
        }

        [Test]
        public void TestInOrder()
        {
            // when
            var actual = Root.InOrder().Select(o => o.Item);
            var expect = new int[] { 6, 4, 7, 2, 1, 5, 3, };

            // then
            CollectionAssert.AreEqual(actual, expect);
        }

        [Test]
        public void TestPostOrder()
        {
            // when
            var actual = Root.PostOrder().Select(o => o.Item);
            var expect = new int[] { 6, 7, 4, 2, 5, 3, 1, };

            // then
            CollectionAssert.AreEqual(actual, expect);
        }

        [Test]
        public void TestAdd()
        {
            // given
            var root = new BinaryTreeNode<int>(10);

            // when
            root.Add(5);
            root.Add(15);

            // then
            Assert.AreEqual(root.LeftChild.Item, 5);
            Assert.AreEqual(root.RightChild.Item, 15);
        }

        [Test]
        public void TestDelete()
        {
            // given
            var root = new BinaryTreeNode<int>(6);
            var values = new int[] { 1, 2, 3, 4, 5, 7, 8, 9, 10, 11,};

            foreach (var value in values)
                root.Add(value);

            // when
            root.Delete(3);
            root.Delete(9);

            var actual = root.GetSubTreeNodes().Select(n => n.Item);
            var expect = new int[] { 1, 2, 4, 5, 6, 7, 8, 10, 11 };

            // then
            Assert.IsTrue(!actual.Except(expect).Any());
        }

        [Test]
        public void TestFind()
        {
            // given
            var root = new BinaryTreeNode<int>(6);
            var values = new int[] { 1, 3, 5, 7, 9, };

            foreach (var value in values)
                root.Add(value);

            // when
            var actual1 = root.Find(1);
            var actual2 = root.Find(2);
            var actual3 = root.Find(3);
            var actual4 = root.Find(4);
            var actual5 = root.Find(5);
            var actual6 = root.Find(6);
            var actual7 = root.Find(7);
            var actual8 = root.Find(8);
            var actual9 = root.Find(9);
            var actual10 = root.Find(10);

            // then
            Assert.IsNotNull(actual1);
            Assert.IsNull(actual2);
            Assert.IsNotNull(actual3);
            Assert.IsNull(actual4);
            Assert.IsNotNull(actual5);
            Assert.IsNotNull(actual6);
            Assert.IsNotNull(actual7);
            Assert.IsNull(actual8);
            Assert.IsNotNull(actual9);
            Assert.IsNull(actual10);
        }
    }
}
