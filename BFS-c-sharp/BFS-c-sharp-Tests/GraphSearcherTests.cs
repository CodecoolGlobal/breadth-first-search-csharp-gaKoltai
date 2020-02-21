using System.Collections.Generic;
using BFS_c_sharp;
using BFS_c_sharp.Model;
using NUnit.Framework;

namespace BFS_c_sharp_Tests
{
    [TestFixture]
    public class GraphSearcherTests
    {

        private UserGraph _userGraph;
        private List<UserNode> _users;

        [SetUp]
        public void SetUp()
        {
            _users = new List<UserNode>
            {
                new UserNode("Sanyi", "Sanyi"),
                new UserNode("Bela", "Bela"),
                new UserNode("Brandon", "Siska"),
                new UserNode("Sophie", "Varga"),
                new UserNode("Wallie", "Eva")
            };
            SeedData();
            _userGraph = new UserGraph(_users);
            
        }

        private void SeedData()
        {
            _users[0].AddFriend(_users[1]);
            _users[0].AddFriend(_users[3]);
            _users[1].AddFriend(_users[0]);
            _users[1].AddFriend(_users[4]);
            _users[2].AddFriend(_users[3]);
            _users[2].AddFriend(_users[4]);
            _users[3].AddFriend(_users[0]);
            _users[3].AddFriend(_users[2]);
            _users[3].AddFriend(_users[4]);
            _users[4].AddFriend(_users[1]);
            _users[4].AddFriend(_users[2]);
            _users[4].AddFriend(_users[3]);
        }

        [TearDown]
        public void TearDown()
        {
            _users = null;
            _userGraph = null;
        }

        [Test]
        public void DistanceGivenSanyiAndBelaReturn1()
        {
            var sanyi = _users[0];
            var sophie = _users[3];
            var expectedDistance = 1;
            Assert.AreEqual(expectedDistance, _userGraph.GetShortestDistanceBetweenNodes(sanyi, sophie));
        }

        [Test]
        public void DistanceGivenSanyiAndBrandonReturns2()
        {
            var sanyi = _users[0];
            var brandon = _users[2];
            int expectedDistance = 2;
            Assert.AreEqual(expectedDistance, _userGraph.GetShortestDistanceBetweenNodes(sanyi, brandon));
        }
        
        [Test]
        [Ignore("Method not ready yet")]
        public void FriendsOfFriendsGivenSanyiAnd1ReturnBelaAndSophie()
        {
            var sanyi = _users[0];
            var bela = _users[1];
            var sophie = _users[3];
            HashSet<UserNode> expected = new HashSet<UserNode> { bela, sophie };
            // Assert.AreEqual(expected, _userGraph.GetShortestPathBetweenNodes(sanyi, 1));
        }

        [Test]
        [Ignore("Method not implemented yet")]
        public void FriendsOfFriendsGivenSanyiAnd2ReturnAnyone()
        {
            var sanyi = _users[0];
            HashSet<UserNode> expected = new HashSet<UserNode>
            {
                _users[1],
                _users[3],
                _users[4],
                _users[2]
            };
            // Assert.AreEqual(expected, _userGraph.GetShortestPathBetweenNodes(sanyi, 2));
        }

        [Test]
        public void ShortestPathGivenSanyiAndBrandonReturnSanyiSophieBrandon()
        {
            var sanyi = _users[0];
            var sophie = _users[3];
            var brandon = _users[2];
            var expected = new List<UserNode> { sanyi, sophie, brandon };
            Assert.AreEqual(expected, _userGraph.GetShortestPathBetweenNodes(sanyi, brandon));
        }
    }
    }
