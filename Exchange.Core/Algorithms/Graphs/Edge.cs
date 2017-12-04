using System;

namespace Exchange.Core.Algorithms.Graphs
{
    public class Edge<T> : IComparable<Edge<T>> where T : IComparable<T>
    {
        private T v;
        private T w;
        private double weight;

        public Edge(T v, T w, double weight)
        {
            this.v = v;
            this.w = w;
            this.weight = weight;
        }

        public T Either()
        {
            return v;
        }

        public T Other(T v)
        {
            return this.v.CompareTo(v) == 0 ? w : this.v;
        }

        public T From()
        {
            return v;
        }

        public T To()
        {
            return w;
        }

        public double Weight => weight;


        public int CompareTo(Edge<T> that)
        {
            return weight.CompareTo(that.Weight);
        }

        public override String ToString()
        {
            return v + " =(" + weight + ")=> " + w;
        }
    }
}
