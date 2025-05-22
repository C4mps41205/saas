using CRM_SAAS.Models;

namespace CRM_SAAS.Services.Repository;

public interface IUtilsServices
{
    Task<ViaCepApiResponse> GetAddressInfos(string zipCode);
}