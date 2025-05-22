using System.Net.Http.Json;
using CRM_SAAS.Models;

namespace CRM_SAAS.Services;

public class UtilsServices(HttpClient httpClient) : IUtilsServices
{
    #region --Queries

    public async Task<ViaCepApiResponse> GetAddressInfos(string zipCode)
    {
        if (zipCode.Length != 13) throw new Exception("Invalid Zip Code");

        return await httpClient.GetFromJsonAsync<ViaCepApiResponse>("https://viacep.com.br/ws/" + zipCode + "/json/") ??
               new();
    }

    #endregion
}