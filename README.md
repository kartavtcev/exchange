# Technical Exercise

## Design ideas

+ Programming language of choice is C# (aka sibling of C++ and Java), I'm most experienced with & many classical algorithms (incl. graph shortest path) fit fine OOP languages with mutable state. (Rust looks like sibling of C++ and Scala with specific memory management)
+ OS Windows 10, IDE Visual Studio Community 2017 (free) - recommended, IDE Visual Studio Pro 2017 (trial); OS Ubuntu 17.10, IDE JetBrains Rider (trial) (Rider's debug gives a duplication of line "Enter input text, 'exit' to exit:" so far).
+ Platform of choice is .NET Core 2.0, its package manager nuget.org plays the role of crates.io. .NET Core runs on Windows, macOS, Linux, etc.
+ Graph shortest path search algorithm choice:  

1. Available [algs](https://cs.stackexchange.com/questions/2942/am-i-right-about-the-differences-between-floyd-warshall-dijkstra-and-bellman-fo) are of 2 categories: single source and all pairs. Dijkstra's & Bellman-Ford algs are SSSP. Floyd-Warshall is APSP. APSP would fit the case with rare/small price updates, making the graph nearly immutable aka cache. APSP algorihm would allow to calcullate all shortest path once and use for all (frequent) exchange rate requests for a period of time aka expiration time. In real-life case of larger number of price updates, I'd expect 'all pairs' to expire quickly.

2. I propose to use single source Bellman-Ford's alg. Its complexity (edge-weighted digraphs with negative cycle detection) is ***O(EV)*** worst case time compared to Floyd-Warshall ***O(V^3)***. Bellman-Ford alg implementation is based on my favourite Java OOP-style [Algorithms, 4th Edition by Robert Sedgewick and Kevin Wayne](https://algs4.cs.princeton.edu/home/)). In-house algs code would also allow to not include 3rd party algs libs to prod deployment.

3. What is substitution: min->max, + -> * and how it's legal?   
min -> max(-1*) : In order to get minimum number, we can negate maximum number.   
+ -> * : it's a result of applying ln to edge weight, then during calcullation sum of path it'd become:  
weight = -ln(rate)  <-- (because of this, summation of edge's costs/weights, would be multiplication for original problem, (since ln,exp are monotonic increasing funcs))  
path*= exp(-weight)  
I don't like the idea of modifying original alg. There's a rule to move in production only official, battle-tested algs. The risk of error increases (at least because each classical alg has formal corectness math prove; worst-case time, space costs) => "substitution" step will be outside of Bellman-Ford.   

+ BTW, since I use log{e}(), exp() funcs, I have to use Double float type, instead of Decimal which is recomended for money $$$ for its precision. I'd round it to 5 decimal places.
Store the most recent price by date time. To prevent parallel access to graph, wrapper would be introduced to lock entire graph on each operation. This later could be updated to {edge + from, to 2 vertexes} lock.  

+ Read-only log of exchanges responses for legal compliance ??? Will skip IoC(DI), logging for this demo.

+ ***TODO:*** So far if cycle/arbitrage is found in the graph, no path would be returned. To solve this, will need to go 1 by 1 trying to delete cycle edges {with recursion until path not found} and then return the path found or move to next edge.

---