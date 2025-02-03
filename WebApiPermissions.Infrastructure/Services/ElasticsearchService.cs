using Elastic.Clients.Elasticsearch;
using WebApiPermissions.Domain;

namespace WebApiPermissions.Infrastructure.Services;

public class ElasticsearchService
{
    private readonly ElasticsearchClient _elasticClient;

    public ElasticsearchService(ElasticsearchClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task IndexPermissionAsync(Permission permission)
    {
        var response = await _elasticClient.IndexAsync(permission, idx => idx.Index("permissions"));

        if (!response.IsValidResponse)
        {
            throw new Exception($"Error al indexar en Elasticsearch: {response.DebugInformation}");
        }
    }
}
