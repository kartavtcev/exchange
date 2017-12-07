# TenX - Technical Exercise

## Design ideas

+ Programming language of choice is C# (aka sibling of C++ and Java), i'm most experienced with & many classical algorithms (incl. graph shortest path) fit fine OOP languages with mutable state. (Rust looks like sibling of C++ and Scala with specific memory management)
+ OS Windows 10, IDE Visual Studio; OS Ubuntu 17.10, IDE JetBrains Rider.
+ Platform of choice is .NET Core 2.0, its package manager nuget.org plays the role of crates.io. .NET Core runs on Windows, macOS, Linux, etc.
+ Graph shortest path search algorithm choice:  

1. Available [algs](https://cs.stackexchange.com/questions/2942/am-i-right-about-the-differences-between-floyd-warshall-dijkstra-and-bellman-fo) are of 2 categories: single source and all pairs. Dijkstra's & Bellman-Ford algs are SSSP. Floyd-Warshall is APSP. APSP would fit the case with rare/small price updates, making the graph nearly immutable aka cache. APSP algorihm would allow to calcullate all shortest path once and use for all (frequent) exchange rate requests for a period of time aka expiration time. In real-life case of larger number of price updates, i'd expect 'all pairs' to expire quickly.

2. I propose to use single source Bellman-Ford's alg. Its complexity (edge-weighted digraphs with negative cycle detection) is ***O(EV)*** worst case time compared to Floyd-Warshall ***O(V^3)***. Bellman-Ford alg implementation is based on my favourite Java OOP-style [Algorithms, 4th Edition by Robert Sedgewick and Kevin Wayne](https://algs4.cs.princeton.edu/44sp/)). In-house algs code would also allow to not include 3rd party algs libs to prod deployment.

3. What is substitution: min->max, + -> * and how it's legal?   
min -> max(-1*) : In order to get minimum number, we can negate maximum number.   
+ -> * : it's a result of applying ln to edge weight, then during calcullation sum of path it'd become:  
weight = -ln(rate)  
path*= exp(-weight)  
I don't like the idea of modifying original alg. There's a rule to move in production only official, battle-tested algs. The risk of error increases (at least because each classical alg has formal corectness math prove; worst-case time, space costs) => "Substitution" step will be outside of Bellman-Ford. () I'd also try to check precision errors of log, exp funcs.

(4.  Price update timestamp must be max{less or equal to time} of exchange rate request. In prod this will be important for parallel updates, requests to graph. We could have had a list of timelapse projections for each edge, simular to one how [RDBMS could use timelapses to provide isolation levels](https://en.wikipedia.org/wiki/Timestamp-based_concurrency_control). This also could be solved using noSQL DB Neo4j. But keep in mind, storing graph in RAM would speedup things compared to hard drive. "Price updates are not guaranteed to arrive in chronological order." - looks like a characteristic of parallel.) But probably it's an overhead for the demo. Let's just store the most recent price by date time. To prevent parallel access to graph, wrapper would be introduced to lock entire graph on each operation. This later could be updated to {edge + 2 from, to vertexes} lock.  

+ Read-only log of exchanges responses for legal compliance ??? Will skip IoC(DI), logging for this demo.

---

