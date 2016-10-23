using System;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using Google.Apis.Services;
using Google.Apis.Vision.v1;
using Google.Apis.Vision.v1.Data;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClosetInventory.WorkerClasses
{
    public static class VisionAPIManager
    
        {
            /// Creates the credentials.
            public static GoogleCredential CreateCredentials(string path)
            {
                GoogleCredential credential;
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var c = GoogleCredential.FromStream(stream);
                    credential = c.CreateScoped(VisionService.Scope.CloudPlatform);
                }
                return credential;
            }
            /// Creates the service.
            public static VisionService CreateService(
                string applicationName,
                IConfigurableHttpClientInitializer credentials)
            {
                var service = new VisionService(new BaseClientService.Initializer()
                {
                    ApplicationName = applicationName,
                    HttpClientInitializer = credentials
                });

                return service;
            }

            /// Creates the annotation image request.
            private static AnnotateImageRequest CreateAnnotationImageRequest(string path, string[] featureTypes)
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("Not found.", path);
                }

                var request = new AnnotateImageRequest();
                request.Image = new Google.Apis.Vision.v1.Data.Image();

                var bytes = File.ReadAllBytes(path);
                request.Image.Content = Convert.ToBase64String(bytes);

                request.Features = new List<Feature>();

                foreach (var featureType in featureTypes)
                {
                    request.Features.Add(new Feature() { Type = featureType });
                }

                return request;
            }

            /// Annotates the file asynchronously.
            public static async Task<AnnotateImageResponse> AnnotateAsync(this VisionService service, FileInfo file, params string[] features)
            {
                var request = new BatchAnnotateImagesRequest();
                request.Requests = new List<AnnotateImageRequest>();
                request.Requests.Add(CreateAnnotationImageRequest(file.FullName, features));

                BatchAnnotateImagesResponse result = await service.Images.Annotate(request).ExecuteAsync().ConfigureAwait(false);


                if (result?.Responses?.Count > 0)
                {
                    return result.Responses[0];
                }

                return null;
            }
        }
    }
