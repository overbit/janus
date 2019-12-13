namespace overapp.janus.Configurations
{
    public class OpenApiConfig
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string Title { get; set; }

        public string EndpointName => $"{Name} {Version}";

        public string EndpointUrl => $"/swagger/{Version}/swagger.json";
    }
}
