using System;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            /// <summary>
            /// the method override ToString method
            /// </summary>
            public override string ToString()
            {
                return $"Id: {Id} Name: {Name} Phone: {Phone}" +
                    $" Longitude: {Longitude} Lattitude: {Lattitude}";
            }
        }
    }
}