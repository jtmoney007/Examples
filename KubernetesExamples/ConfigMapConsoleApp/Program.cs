using k8s;
using k8s.Models;
using System;
using System.Threading.Tasks;

namespace ConfigMapReader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = KubernetesClientConfiguration.BuildDefaultConfig();
            var client = new Kubernetes(config);

            string? podName = Environment.GetEnvironmentVariable("JOB_NAME");
            string? namespaceName = Environment.GetEnvironmentVariable("JOB_NAMESPACE");
            string? configMapName = Environment.GetEnvironmentVariable("JOB_CONFIGMAP");

            if (!string.IsNullOrEmpty(namespaceName) || !string.IsNullOrEmpty(configMapName))
            {
                try
                {
                    V1ConfigMap configMap = await client.ReadNamespacedConfigMapAsync(configMapName, namespaceName);

                    if (configMap != null && configMap.Data != null)
                    {
                        string databaseUrl = configMap.Data["database_url"];
                        string apiKey = configMap.Data["api_key"];
                        string appConfig = configMap.Data["app_config"];

                        Console.WriteLine($"{podName} Database URL: " + databaseUrl);
                        Console.WriteLine($"{podName} API Key: " + apiKey);
                        Console.WriteLine($"{podName} App Config: " + appConfig);
                    }
                    else
                    {
                        Console.WriteLine($"{podName} ConfigMap not found or empty.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{podName} Error: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine($"{podName} Missing namespace or configmap name.");
            }
        }
    }
}
