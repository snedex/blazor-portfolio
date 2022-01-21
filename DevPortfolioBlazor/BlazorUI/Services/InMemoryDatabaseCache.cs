using BlazorUI.Services;
using Core.Models;
using System.Net.Http.Json;

namespace BlazorUI.Services
{
    internal sealed class InMemoryDatabaseCache
    {
        private HttpClient _httpClient;

        public InMemoryDatabaseCache(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private IList<Category> _categories = null;
        private IList<Post> _posts = null;
        private IList<Skill> _skills = null;
        private IList<ProjectDetail> _projectDetails = null;
        private IList<Project> _projects = null;

        internal IList<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                NotifyCategoriesDataChanged();
            }
        }
 
        internal IList<Skill> Skills
        {
            get { return _skills; }
            set
            {
                _skills = value;
                NotifySkillsDataChanged();
            }
        }

        internal IList<Project> Projects
        {
            get { return _projects; }
            set
            {
                _projects = value;
                NotifyProjectsDataChanged();
            }
        }

        internal IList<ProjectDetail> ProjectDetails
        {
            get { return _projectDetails; }
            set
            {
                _projectDetails = value;
                NotifyProjectDetailsChanged();
            }
        }

        internal IList<Post> Posts
        {
            get { return _posts; }
            set
            {
                _posts = value;
                NotifyPostsChanged();
            }
        }

        private bool _fetchingRecords = false;

        internal async Task GetCategoriesAndCache()
        {
            if (!_fetchingRecords)
            {
                _fetchingRecords = true;
                _categories = await _httpClient.GetFromJsonAsync<List<Category>>(APIEndpoints.s_categories);
                _fetchingRecords = false;
            }
        }

        internal async Task GetSkillsAndCache()
        {
            if (!_fetchingRecords)
            {
                _fetchingRecords = true;
                _skills = await _httpClient.GetFromJsonAsync<List<Skill>>(APIEndpoints.s_skills);
                _fetchingRecords = false;
            }
        }

        internal async Task GetProjectsAndCache()
        {
            if (!_fetchingRecords)
            {
                _fetchingRecords = true;
                _projects = await _httpClient.GetFromJsonAsync<List<Project>>(APIEndpoints.s_projects);
                _fetchingRecords = false;
            }
        }

        internal async Task GetProjectDetailsAndCache()
        {
            if (!_fetchingRecords)
            {
                _fetchingRecords = true;
                _projectDetails = await _httpClient.GetFromJsonAsync<List<ProjectDetail>>(APIEndpoints.s_projectDetails);
                _fetchingRecords = false;
            }
        }

        internal async Task GetPostsAndCache()
        {
            if (!_fetchingRecords)
            {
                _fetchingRecords = true;
                _posts = await _httpClient.GetFromJsonAsync<List<Post>>(APIEndpoints.s_posts);
                _fetchingRecords = false;
            }
        }

        internal async Task<Category> GetCategoryById(int id)
        {
            if (_categories == null)
            {
                await GetCategoriesAndCache();
            }

            return _categories.FirstOrDefault(category => category.CategoryId == id);
        }

        internal async Task<Post> GetPostById(int id)
        {
            if (_posts == null)
            {
                await GetPostsAndCache();
            }

            return _posts.FirstOrDefault(p => p.PostId == id);
        }

        //event to subscribe to, to listen for data change
        internal event Action OnCategoriesDataChanged;
        internal event Action OnSkillsDataChanged;
        internal event Action OnProjectsDataChanged;
        internal event Action OnProjectDetailsChanged;
        internal event Action OnPostsChanged;

        //Fires the event if there are subscribers 
        private void NotifyCategoriesDataChanged() => OnCategoriesDataChanged?.Invoke();
        private void NotifySkillsDataChanged() => OnSkillsDataChanged?.Invoke();
        private void NotifyProjectsDataChanged() => OnProjectsDataChanged?.Invoke();
        private void NotifyProjectDetailsChanged() => OnProjectDetailsChanged?.Invoke();
        private void NotifyPostsChanged() => OnPostsChanged?.Invoke();

    }
}