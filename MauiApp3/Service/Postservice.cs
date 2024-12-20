using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp3.Model;

namespace MauiApp3.Service
{
    public class Postservice
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/post"; // Replace with your actual API base URL

    public Postservice()
    {
        _httpClient = new HttpClient();
    }

       
        // GET /posts
        public async Task<List<PostRecord>> GetPostsAsync()
    {
        var response = await _httpClient.GetStringAsync($"{BaseUrl}/posts");
        return JsonConvert.DeserializeObject<List<PostRecord>>(response);
    }

    // GET /posts/{id}
    public async Task<PostRecord> GetPostByIdAsync(int id)
    {
        var response = await _httpClient.GetStringAsync($"{BaseUrl}/posts/{id}");
        return JsonConvert.DeserializeObject<PostRecord>(response);
    }

    // GET /posts/{id}/comments
    public async Task<List<Comment>> GetCommentsForPostAsync(int postId)
    {
        var response = await _httpClient.GetStringAsync($"{BaseUrl}/posts/{postId}/comments");
        return JsonConvert.DeserializeObject<List<Comment>>(response);
    }

    // POST /posts
    public async Task<PostRecord> CreatePostAsync(PostRecord post)
    {
        var json = JsonConvert.SerializeObject(post);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{BaseUrl}/posts", content);
        var responseString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<PostRecord>(responseString);
    }

    // PUT /posts/{id}
    public async Task<PostRecord> UpdatePostAsync(int id, PostRecord post)
    {
        var json = JsonConvert.SerializeObject(post);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{BaseUrl}/posts/{id}", content);
        var responseString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<PostRecord>(responseString);
    }

    // PATCH /posts/{id}
    public async Task<PostRecord> PatchPostAsync(int id, PostRecord post)
    {
        var json = JsonConvert.SerializeObject(post);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PatchAsync($"{BaseUrl}/posts/{id}", content);
        var responseString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<PostRecord>(responseString);
    }

    // DELETE /posts/{id}
    public async Task DeletePostAsync(int id)
    {
        await _httpClient.DeleteAsync($"{BaseUrl}/posts/{id}");
    }
    }
}
public class Comment
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("postId")]
    public int PostId { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("body")]
    public string Body { get; set; }
}