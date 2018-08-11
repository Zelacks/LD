using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using UnityEngine;

using Random = System.Random;


namespace Source.Helpers
{

    public class ProportionValue< T >
    {

        public double Proportion { get; set; }
        public T Value { get; set; }

    }

    public static class ProportionValue
    {

        public static ProportionValue< T > Create< T >( double proportion, T value )
        {
            return new ProportionValue< T > { Proportion = proportion, Value = value };
        }

        static Random random = new Random( );

        public static T ChooseByRandom< T >(
            this IEnumerable< ProportionValue< T > > collection )
        {
            var sum = collection.Sum( t => t.Proportion );

            var rnd = random.NextDouble( );
            foreach ( var item in collection )
            {
                if ( rnd < ( item.Proportion / sum ) )
                {

                    return item.Value;
                }

                rnd -= ( item.Proportion / sum );
            }

            return collection.Last( ).Value;
        }

    }

}
