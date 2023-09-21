using System;
using System.Text;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace SecretsConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = KubernetesClientConfiguration.BuildDefaultConfig();
            var client = new Kubernetes(config);

            string? podName = Environment.GetEnvironmentVariable("JOB_NAME");
            string? namespaceName = Environment.GetEnvironmentVariable("JOB_NAMESPACE");
            string? secretName = Environment.GetEnvironmentVariable("JOB_SECRET");
            if (!string.IsNullOrEmpty(namespaceName) || !string.IsNullOrEmpty(secretName))
            {
                try
                {
                    V1Secret secret = await client.ReadNamespacedSecretAsync(secretName, namespaceName);

                    if (secret != null && secret.Data != null)
                    {
                        string username = Encoding.UTF8.GetString(secret.Data["username"]);
                        string password = Encoding.UTF8.GetString(secret.Data["password"]);

                        Console.WriteLine($"{podName} Username: {username}");
                        Console.WriteLine($"{podName} Password: {password}");
                    }
                    else
                    {
                        Console.WriteLine($"{podName} Secret not found or empty.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{podName} Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"{podName} Missing namespace or secret name.");
            }
        }
    }
}
