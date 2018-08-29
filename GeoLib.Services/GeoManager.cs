using System.Collections.Generic;
using GeoLib.Contracts;
using GeoLib.Data.Entities;
using GeoLib.Data.Repositories;
using GeoLib.Data.RepositoryInterfaces;

namespace GeoLib.Services
{
    public class GeoManager : IGeoService
    {
        public GeoManager()
        {
        }

        public GeoManager(IZipCodeRepository zipCodeRepository)
        {
            _zipCodeRepository = zipCodeRepository;
        }

        public GeoManager(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public GeoManager(IZipCodeRepository zipCodeRepository, IStateRepository stateRepository)
        {
            _zipCodeRepository = zipCodeRepository;
            _stateRepository = stateRepository;
        }

        private readonly IZipCodeRepository _zipCodeRepository;
        private readonly IStateRepository _stateRepository;

        public ZipCodeData GetZipCodeInfo(string zipCode)
        {
            ZipCodeData zipCodeData = null;
            IZipCodeRepository zipCodeRepository = _zipCodeRepository ?? new ZipCodeRepository();
            ZipCode zipCodeEntity = zipCodeRepository.GetByZipCode(zipCode);

            if (zipCodeEntity != null)
            {
                zipCodeData = new ZipCodeData
                {
                    City = zipCodeEntity.City,
                    State = zipCodeEntity.State.Abbreviation,
                    ZipCode = zipCodeEntity.Zip
                };
            }

            return zipCodeData;
        }

        public IEnumerable<string> GetStates(bool isPrimaryOnly)
        {
            List<string> stateData = new List<string>();
            IStateRepository stateRepository = _stateRepository ?? new StateRepository();
            IEnumerable<State> states = stateRepository.Get(isPrimaryOnly);

            // ReSharper disable once InvertIf
            if (states != null)
            {
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (State state in states)
                {
                    stateData.Add(state.Abbreviation);
                }
            }

            return stateData;
        }

        public IEnumerable<ZipCodeData> GetZipCodes(string state)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();
            IZipCodeRepository zipCodeRepository = _zipCodeRepository ?? new ZipCodeRepository();
            IEnumerable<ZipCode> zipCodes = zipCodeRepository.GetByState(state);

            // ReSharper disable once InvertIf
            if (zipCodes != null)
            {
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (ZipCode zipCode in zipCodes)
                {
                    ZipCodeData newZipCodeData = new ZipCodeData
                    {
                        City = zipCode.City,
                        State = zipCode.State.Abbreviation,
                        ZipCode = zipCode.Zip
                    };

                    zipCodeData.Add(newZipCodeData);
                }
            }

            return zipCodeData;
        }

        public IEnumerable<ZipCodeData> GetZipCodes(string zipCode, int zipCodeRange)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();
            IZipCodeRepository zipCodeRepository = _zipCodeRepository ?? new ZipCodeRepository();
            ZipCode zipCodeEntity = zipCodeRepository.GetByZipCode(zipCode);
            IEnumerable<ZipCode> zipCodes = zipCodeRepository.GetZipCodesForRange(zipCodeEntity, zipCodeRange);

            // ReSharper disable once InvertIf
            if (zipCodes != null)
            {
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (ZipCode zipCode2 in zipCodes)
                {
                    ZipCodeData newZipCodeData = new ZipCodeData
                    {
                        City = zipCode2.City,
                        State = zipCode2.State.Abbreviation,
                        ZipCode = zipCode2.Zip
                    };

                    zipCodeData.Add(newZipCodeData);
                }
            }

            return zipCodeData;
        }
    }
}
