using Amazon.S3;
using Amazon.S3.Model;
using System.Text;

var s3Client = new AmazonS3Client();

await using var inputStream = new FileStream("./example.json", FileMode.Open, FileAccess.Read);

var putRequest = new PutObjectRequest
{
    BucketName = "faulycoelho.bucket",
    Key = "files/example.json",
    ContentType = "text/plain",
    InputStream = inputStream
};

await s3Client.PutObjectAsync(putRequest);

var getRequest = new GetObjectRequest
{ 
    BucketName = "faulycoelho.bucket",
    Key = "files/example.json"
};


var getResponse = s3Client.GetObjectAsync(getRequest);
await using var memoryStream = new MemoryStream();
await getResponse.Result.ResponseStream.CopyToAsync(memoryStream);

var text =  Encoding.Default.GetString(memoryStream.ToArray());
Console.WriteLine(text);