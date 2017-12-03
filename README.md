# TenX - Technical Exercise

## Design ideas

+ Programming language of choice is C# (aka C++ with managed memory), i'm most experienced with. Rust looks like sibling of C++ and Scala with specific memory management.
+ Platform of choice is .NET Core, its package manager nuget.org plays the role of crates.io. .NET Core runs on Windows, macOS, Linux, etc.
+ Graph shortest path search algorithm choice:  

1. Available [algs](https://cs.stackexchange.com/questions/2942/am-i-right-about-the-differences-between-floyd-warshall-dijkstra-and-bellman-fo) are of 2 categories: single source and all pairs. Dijkstra's & Bellman-Ford algs are SSSP. Floyd-Warshall is APSP. APSP would fit the case with rare/small price updates, making the graph nearly immutable aka cache. APSP algorihm would allow to calcullate all shortest path once and use for all (frequent) exchange rate requests for a period of time aka expiration time. In real-life case of equal or larger / bigger amount number of price updates requests compared to exchange rate request, i'd expect graph 'all pairs' to expire quickly. 

2. Thus taking into consideration we have no negative edges (otherwise aka non-profit exchanges) i propose to run per-exchange request single source Dijkstra's alg. Dijkstra's single source complexity (edge-weighted digraphs with nonnegative weights) is ***O(E*log(V))*** worst case compared to Floyd-Warshall ***O(V^3)***.
Dijkstra's alg implementation is based on Java OOP-style [Algorithms, 4th Edition by Robert Sedgewick and Kevin Wayne](https://algs4.cs.princeton.edu/44sp/)

3.  Price updates timelapses must be less or equal to time of exchange rate request. In prod this could happen with parallel updates, requests to graph. We'd have an array of timelapse projections for each edge. This also could be solved using noSQL DB Neo4j.

---

