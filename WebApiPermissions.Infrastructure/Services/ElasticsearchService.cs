using Elastic.Clients.Elasticsearch;
using WebApiPermissions.Domain.Entities;

namespace WebApiPermissions.Infrastructure.Services;

public class ElasticsearchService
{
    private readonly ElasticsearchClient _elasticClient;
    private const string IndexName = "permissions";


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

    public async Task BulkIndexPermissionsAsync(List<Permission> permissions)
    {
        try
        {
            if (permissions == null || permissions.Count == 0)
            {
                throw new Exception("No hay permisos para indexar en Elasticsearch.");
            }

            var response = await _elasticClient.IndexManyAsync(permissions, IndexName);

            if (!response.IsValidResponse)
            {
                throw new Exception($"Error en indexación masiva: {response.DebugInformation}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en BulkIndexPermissionsAsync: {ex.Message}");
            throw;
        }
    }
}
