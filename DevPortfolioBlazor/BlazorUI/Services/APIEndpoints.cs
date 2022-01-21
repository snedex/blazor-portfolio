namespace BlazorUI.Services
{
    internal static class APIEndpoints
    {

        //TODO: magic strings
#if DEBUG
        internal const string ServerBaseUrl = "https://localhost:5003";
#else
        internal const string ServerBaseUrl = "https://sneddonblazorapi.azurewebsites.net";
#endif

        internal readonly static string s_categories = $"{ServerBaseUrl}/api/categories";

        internal readonly static string s_skills = $"{ServerBaseUrl}/api/skills";

        internal readonly static string s_projects = $"{ServerBaseUrl}/api/projects";

        internal readonly static string s_projectDetails = $"{ServerBaseUrl}/api/projects/details";

        internal readonly static string s_imageUpload = $"{ServerBaseUrl}/api/uploadimage";

        //internal readonly static string s_categoriesWithPosts = $"{ServerBaseUrl}/api/categories/withposts";

        internal readonly static string s_posts = $"{ServerBaseUrl}/api/posts";

        internal readonly static string s_postsDTO = $"{ServerBaseUrl}/api/posts/dto";

        internal readonly static string s_signIn = $"{ServerBaseUrl}/api/signin";

    }
}
