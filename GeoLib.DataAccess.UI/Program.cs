using GeoLib.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using GeoLib.Data.Entities;
using GeoLib.Data.Interfaces;

namespace GeoLib.DataAccess.UI
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        public static void Main()
        {
            TestStateRepository();
            TestZipCodeRepository();

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static void TestStateRepository()
        {
            IStateRepository stateRepository = new StateRepository();

            const string stateString = "NJ";
            State state = stateRepository.Get(stateString);

            const bool isPrimaryOnly = true;
            List<State> states = stateRepository.Get(isPrimaryOnly);
            int count = states.Count;
            Console.WriteLine($"states.Count() = {count}");
        }

        private static void TestZipCodeRepository()
        {
            IZipCodeRepository zipCodeRepository = new ZipCodeRepository();

            List<ZipCode> zipCodes1 = zipCodeRepository.Get();
            int count = zipCodes1.Count;
            Console.WriteLine($"zipCodes1.Count() = {count}");

            const string stateString = "NJ";
            List<ZipCode> zipCodes2 = zipCodeRepository.GetByState(stateString);
            count = zipCodes2.Count;
            Console.WriteLine($"zipCodes2.Count() = {count}");

            ZipCode zipCode = new ZipCode
            {
                City = "LINCOLN PARK",
                State = new State { Abbreviation = "NJ" },
                Zip = "07035"
            };
            const int range = 10000;
            List<ZipCode> zipCodes3 = zipCodeRepository.GetZipCodesForRange(zipCode, range);
            count = zipCodes3.Count;
            Console.WriteLine($"zipCodes3.Count() = {count}");
        }
    }
}
