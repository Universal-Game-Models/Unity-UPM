using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Samples.UGM.Scripts.Examples.Features.SkinSwap.Core;
using UGM.Core;
using UGM.Examples.Inventory;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.OpsiveIntegration
{
    public class ItemFilteringHandler : GetNftsOwned
    {
        public static ItemFilteringHandler Instance;
        protected bool isDataBeenLoaded = false;
        protected List<UGMDataTypes.TokenInfo> tokenInfos = new List<UGMDataTypes.TokenInfo>();
        public bool IsTokenDataLoaded => isDataBeenLoaded;
        public bool isFirstPersonOnly = false;
        public bool isThirdPersonOnly = true;
        public bool runOnStart = true;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            if (runOnStart == true)
                GetTokenDataList();
        }

        protected async Task GetTokenDataList()
        {
            if (tokenInfos != null)
                tokenInfos.Clear();
            else
                tokenInfos = new List<UGMDataTypes.TokenInfo>();

            
            tokenInfos = await GetNftsByAddress();
            isDataBeenLoaded = true;
        }
        
        public virtual List<UGMDataTypes.TokenInfo> GetTokenDataByFilter(FilterItem filter)
        {
            if(tokenInfos?.Count <= 0) Debug.LogError("Token Data List is empty!");
            List<UGMDataTypes.TokenInfo> filteredData = new List<UGMDataTypes.TokenInfo>();
            foreach (var tokenInfo in tokenInfos)
            {
                UGMDataTypes.Attribute attribute = GetFilteredAttribute(tokenInfo, filter);
                if (IsAttributeFiltered(attribute))
                {
                    filteredData.Add(tokenInfo);
                }
            }
            return filteredData;
        }
        
        protected virtual UGMDataTypes.Attribute GetFilteredAttribute(UGMDataTypes.TokenInfo tokenInfo, FilterItem filterType)
        {
            return tokenInfo.metadata.attributes.FirstOrDefault(md =>
                filterType.isNameTraitType
                    ? md.trait_type == filterType.name
                    : md.value.ToString() == filterType.name);
        }
        

        protected virtual bool IsAttributeFiltered(UGMDataTypes.Attribute attribute)
        {
            return attribute != null;
        }
    }
}