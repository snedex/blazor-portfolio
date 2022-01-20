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
        internal IList<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                NotifyCategoriesDataChanged();
            }
        }

        private IList<Skill> _skills = null;
        internal IList<Skill> Skills
        {
            get { return _skills; }
            set
            {
                _skills = value;
                NotifySkillsDataChanged();
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

        //event to subscribe to, to listen for data change
        internal event Action OnCategoriesDataChanged;
        internal event Action OnSkillsDataChanged;

        //Fires the event if there are subscribers 
        private void NotifyCategoriesDataChanged() => OnCategoriesDataChanged?.Invoke();
        private void NotifySkillsDataChanged() => OnSkillsDataChanged?.Invoke();

    }
}