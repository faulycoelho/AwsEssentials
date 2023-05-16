using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

var secretsManagerClient = new AmazonSecretsManagerClient();
var request = new GetSecretValueRequest
{
    SecretId = "myApyKey"
};

var response = secretsManagerClient.GetSecretValueAsync(request);
Console.WriteLine(response.Result.SecretString);