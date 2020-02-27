namespace _4oito6.Infra.CrossCutting.Configuration.Swagger.Models
{
    public class SwaggerConfigModel
    {
        public string Title { get; private set; }

        public string Version { get; private set; }

        public string Description { get; private set; }

        public string ContactName { get; private set; }

        public string ContactEmail { get; private set; }

        public string ContactUrl { get; private set; }

        protected SwaggerConfigModel()
        {
        }

        public SwaggerConfigModel(string title, string version, string description, string contactName, string contactEmail, string contactUrl)
        {
            Title = title;
            Version = version;
            Description = description;
            ContactName = contactName;
            ContactEmail = contactEmail;
            ContactUrl = contactUrl;
        }
    }
}