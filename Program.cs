﻿using System;
using System.Linq;
using Cloo.Extensions;

namespace TestGpuProg
{
    static class Program
    {
        static void Main(string[] args)
        {
            int[] primes = Enumerable.Range(2, 1000000).ToArray();

            primes.ClooForEach(IsPrime);

            Console.WriteLine(string.Join(", ", primes.Where(n => n != 0).Take(100)));
            Console.ReadKey();
        }

        static string IsPrime
        {
            get
            {
                return
@"
kernel void GetIfPrime(global int* message)
{
    int index = get_global_id(0);
    int upperl=(int)sqrt((float)message[index]);
    for(int i=2;i<=upperl;i++)
    {
        if(message[index]%i==0)
        {
            message[index]=0;
            return;
        }
    }
}";
            }
        }
    }
}
