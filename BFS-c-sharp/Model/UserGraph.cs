using System.Collections.Generic;
using System.Linq;

namespace BFS_c_sharp.Model
{
    public class UserGraph
    {
        private  List<UserNode> _userNodes;

        public UserGraph(List<UserNode> userNodes)
        {
            _userNodes = userNodes;
        }
        
        public int GetShortestDistanceBetweenNodes(UserNode start, UserNode end)
        {
            if (start.Equals(end))
            {
                return 0;
            }
            
            var adjacencyQueue = new Queue<UserNode>();
            var visitedNodeSet = new HashSet<UserNode>{start};
            var distances = InitializeDistanceMap();

            adjacencyQueue.Enqueue(start);
            distances[start] = 0;

            while (adjacencyQueue.Count > 0)
            {
                var nodeBeingChecked = adjacencyQueue.Dequeue();

                if (nodeBeingChecked.Equals(end))
                {
                    return distances[nodeBeingChecked];
                }
                
                foreach (var friend in nodeBeingChecked.Friends)
                {
                    if (visitedNodeSet.Contains(friend)) continue;
                    
                    distances[friend] = distances[nodeBeingChecked] + 1;
                    adjacencyQueue.Enqueue(friend);
                    visitedNodeSet.Add(friend);
                }
            }

            return 0;
        }

        public List<UserNode> GetShortestPathBetweenNodes(UserNode start, UserNode end)
        {
            var adjacencyQueue = new Queue<UserNode>();
            var visitedNodeSet = new HashSet<UserNode>{start};
            var pathBetweenNodes = new Dictionary<UserNode, UserNode>();

            adjacencyQueue.Enqueue(start);

            while (adjacencyQueue.Count > 0)
            {
                var nodeBeingChecked = adjacencyQueue.Dequeue();

                foreach (var friend in nodeBeingChecked.Friends)
                {
                    if (visitedNodeSet.Contains(friend)) continue;
                    
                    adjacencyQueue.Enqueue(friend);
                    visitedNodeSet.Add(friend);
                    pathBetweenNodes[friend] = nodeBeingChecked;
                }
            }

            var shortestPathStack = ReconstructShortestPath(pathBetweenNodes, start, end);

            return shortestPathStack.ToList();

        }

        private Dictionary<UserNode, int> InitializeDistanceMap()
        {
            var distanceMap = new Dictionary<UserNode, int>();

            foreach (var node in _userNodes)
            {
                distanceMap[node] = -1;
            }

            return distanceMap;
        }

        private IEnumerable<UserNode> ReconstructShortestPath(Dictionary<UserNode, UserNode> path, UserNode start, UserNode end)
        {
            var shortestPath = new Stack<UserNode>();
            var currentNode = end;

            while (currentNode != start)
            {
                shortestPath.Push(currentNode);
                currentNode = path[currentNode];
            }
            
            shortestPath.Push(start);

            return shortestPath;
        }
        
    }
}