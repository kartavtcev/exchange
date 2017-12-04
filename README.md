# TenX - Technical Exercise

## Design ideas

+ Programming language of choice is C# (aka sibling of C++ and Java), i'm most experienced with. (Rust looks like sibling of C++ and Scala with specific memory management)
+ OS Ubuntu 17.10, IDE JetBrains Rider, OS Windows 10, IDE Visual Studio.
+ Platform of choice is .NET Core 2.0, its package manager nuget.org plays the role of crates.io. .NET Core runs on Windows, macOS, Linux, etc.
+ Graph shortest path search algorithm choice:  

1. Available [algs](https://cs.stackexchange.com/questions/2942/am-i-right-about-the-differences-between-floyd-warshall-dijkstra-and-bellman-fo) are of 2 categories: single source and all pairs. Dijkstra's & Bellman-Ford algs are SSSP. Floyd-Warshall is APSP. APSP would fit the case with rare/small price updates, making the graph nearly immutable aka cache. APSP algorihm would allow to calcullate all shortest path once and use for all (frequent) exchange rate requests for a period of time aka expiration time. In real-life case of larger number of price updates, i'd expect 'all pairs' to expire quickly. 

2. Taking into consideration we have no negative edges (otherwise aka non-profit exchanges) i propose to run per-exchange rate single source Dijkstra's alg. Dijkstra's single source complexity (edge-weighted digraphs with nonnegative weights) is ***O(E*log(V))*** worst case compared to Floyd-Warshall ***O(V^3)***.
Dijkstra's alg implementation is based on my favourite Java OOP-style [Algorithms, 4th Edition by Robert Sedgewick and Kevin Wayne](https://algs4.cs.princeton.edu/44sp/)

3.  Price update timestamp must be max{less or equal to time} of exchange rate request. In prod this will be important for parallel updates, requests to graph. We can have a list of timelapse projections for each edge, simular to one how [RDBMS could use timelapses to provide isolation levels](https://en.wikipedia.org/wiki/Timestamp-based_concurrency_control). This also could be solved using noSQL DB Neo4j. "Price updates are not guaranteed to arrive in chronological order." - looks like a characteristic of parallel.

4. Many of existing graph data structures & algs implementation use (int key) for vertexes [incl. the above](https://algs4.cs.princeton.edu/44sp/). I'm faced with a choice: 1. use int vertexes in graph & have external hash table to map keys to ints vertexes and vice versa OR 2. use keys as suggested (exchange, currency). 1st pros are: able to move to production well-designed & tested official algs implementations covered with unit tests. 1st cons are: custom hash table can have collisions & resulting total number of graph int vertexes could be much bigger then real number of hashed keys. Since porting Java to C# algs required (or deploy separate algs service in Java/JVM with REST, but it's an overhead for this task), I'd go the 2nd way & would plan to cove code with some unit tests. 2nd way would also allow to implement the task using system/standard libs only, without adding external libs/projects dependencies to prod deployment (unit tests lib dependency doesn't go to prod deployment).

+ Read-only log of exchanges responses for legal compliance ???

---

