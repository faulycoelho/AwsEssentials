using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System.Net;
using System.Text.Json;
using Users.Api.Contracts.Data;

namespace Users.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IAmazonDynamoDB _dynamoDB;
        private readonly string _tableName = "users";

        public UserRepository(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDB = dynamoDB;
        }

        public async Task<bool> CreateAsync(UserDto user)
        {
            user.UpdatedAt = DateTime.UtcNow;
            var userAsJson = JsonSerializer.Serialize(user);
            var userAsAttributes = Document.FromJson(userAsJson).ToAttributeMap();

            var createItemRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = userAsAttributes,
                ConditionExpression = "attribute_not_exists(pk) and attribute_not_exists(sk)"
            };

            var response = await _dynamoDB.PutItemAsync(createItemRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task<UserDto?> GetAsync(int id)
        {
            var getItemRequest = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
            };

            var response = await _dynamoDB.GetItemAsync(getItemRequest);
            if (response.Item.Count == 0)
            {
                return null;
            }

            var itemAsDocument = Document.FromAttributeMap(response.Item);
            return JsonSerializer.Deserialize<UserDto>(itemAsDocument.ToJson());
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var queryRequest = new QueryRequest
            {
                TableName = _tableName,
                IndexName = "email-id-index",
                KeyConditionExpression = "Email = :v_Email",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                {
                    ":v_Email", new AttributeValue{ S = email }
                }
            }
            };

            var response = await _dynamoDB.QueryAsync(queryRequest);
            if (response.Items.Count == 0)
            {
                return null;
            }

            var itemAsDocument = Document.FromAttributeMap(response.Items[0]);
            return JsonSerializer.Deserialize<UserDto>(itemAsDocument.ToJson());
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var scanRequest = new ScanRequest
            {
                TableName = _tableName
            };
            var response = await _dynamoDB.ScanAsync(scanRequest);
            return response.Items.Select(x =>
            {
                var json = Document.FromAttributeMap(x).ToJson();
                return JsonSerializer.Deserialize<UserDto>(json);
            })!;
        }

        public async Task<bool> UpdateAsync(UserDto user, DateTime requestStarted)
        {
            user.UpdatedAt = DateTime.UtcNow;
            var userAsJson = JsonSerializer.Serialize(user);
            var userAsAttributes = Document.FromJson(userAsJson).ToAttributeMap();

            var updateItemRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = userAsAttributes,
                ConditionExpression = "UpdatedAt < :requestStarted",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":requestStarted", new AttributeValue{S = requestStarted.ToString("O")} }
            }
            };

            var response = await _dynamoDB.PutItemAsync(updateItemRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deletedItemRequest = new DeleteItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
            };

            var response = await _dynamoDB.DeleteItemAsync(deletedItemRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }
    }
}
