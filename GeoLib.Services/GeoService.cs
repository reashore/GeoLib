using System.Collections.Generic;
using System.ServiceModel;
using GeoLib.Contracts;
using GeoLib.Data.Entities;
using GeoLib.Data.Interfaces;
using GeoLib.Data.Repositories;

namespace GeoLib.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    public class GeoService : IGeoService
    {
        #region Constructors and Fields

        public GeoService()
        {
        }

        public GeoService(IZipCodeRepository zipCodeRepository)
        {
            _zipCodeRepository = zipCodeRepository;
        }

        public GeoService(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public GeoService(IZipCodeRepository zipCodeRepository, IStateRepository stateRepository)
        {
            _zipCodeRepository = zipCodeRepository;
            _stateRepository = stateRepository;
        }

        private readonly IZipCodeRepository _zipCodeRepository;
        private readonly IStateRepository _stateRepository;

        #endregion

        public List<string> GetStates(bool isPrimaryOnly)
        {
            List<string> stateData = new List<string>();
            IStateRepository stateRepository = _stateRepository ?? new StateRepository();
            List<State> statesList = stateRepository.Get(isPrimaryOnly);

            // ReSharper disable once InvertIf
            if (statesList != null)
            {
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (State state in statesList)
                {
                    stateData.Add(state.Abbreviation);
                }
            }

            return stateData;
        }

        public ZipCodeData GetZipCodeInfo(string zipCode)
        {
            ZipCodeData zipCodeData;
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
            else
            {
                string message = $"Zip code {zipCode} not found";
                throw new FaultException(message); 
            }

            return zipCodeData;
        }

        public List<ZipCodeData> GetZipCodes(string state)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();
            IZipCodeRepository zipCodeRepository = _zipCodeRepository ?? new ZipCodeRepository();
            List<ZipCode> zipCodeList = zipCodeRepository.GetByState(state);

            // ReSharper disable once InvertIf
            if (zipCodeList != null && zipCodeList.Count > 0)
            {
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (ZipCode zipCode in zipCodeList)
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
            else
            {
                string message = $"State {state} not found";
                throw new FaultException(message);
            }

            return zipCodeData;
        }

        public List<ZipCodeData> GetZipCodes(string zipCode, int zipCodeRange)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();
            IZipCodeRepository zipCodeRepository = _zipCodeRepository ?? new ZipCodeRepository();
            ZipCode zipCodeEntity = zipCodeRepository.GetByZipCode(zipCode);
            List<ZipCode> zipCodeList = zipCodeRepository.GetZipCodesForRange(zipCodeEntity, zipCodeRange);

            // ReSharper disable once InvertIf
            if (zipCodeList != null && zipCodeList.Count > 0)
            {
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (ZipCode zipCode2 in zipCodeList)
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
            else
            {
                string message = $"Zip code range ({zipCode}, {zipCodeRange}) not found";
                throw new FaultException(message);
            }

            return zipCodeData;
        }
    }
}
