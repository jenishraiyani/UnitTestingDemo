namespace UniversityManagementApi.Attributes
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RateLimitAttribute : Attribute
    {
        public int timeWindowInSeconds { get; set; }
        public int maxRequests { get; set; }

    }
}
