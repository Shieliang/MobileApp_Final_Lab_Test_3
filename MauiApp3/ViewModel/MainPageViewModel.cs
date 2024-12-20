using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp3.Model;
using Microsoft.Maui.Controls;
using MauiApp3.Service;

namespace MauiApp3.ViewModel
{
    public class MainPageViewModel : BindableObject
    {
        private readonly Postservice _postService;

        private string _newPostTitle;
        public string NewPostTitle
        {
            get => _newPostTitle;
            set
            {
                _newPostTitle = value;
                OnPropertyChanged();
            }
        }

        private string _newPostBody;
        public string NewPostBody
        {
            get => _newPostBody;
            set
            {
                _newPostBody = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PostRecord> _posts;
        public ObservableCollection<PostRecord> Posts
        {
            get => _posts;
            set
            {
                _posts = value;
                OnPropertyChanged();
            }
        }

        private PostRecord _selectedPost;
        public PostRecord SelectedPost
        {
            get => _selectedPost;
            set
            {
                _selectedPost = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsPostSelected));
            }
        }

        public bool IsPostSelected => SelectedPost != null;

        public ICommand CreatePostCommand { get; }
        public ICommand UpdatePostCommand { get; }
        public ICommand DeletePostCommand { get; }

        public MainPageViewModel()
        {
            _postService = new Postservice();

            CreatePostCommand = new Command(async () => await CreatePost());
            UpdatePostCommand = new Command(async () => await UpdatePost(), CanExecuteUpdateOrDelete);
            DeletePostCommand = new Command(async () => await DeletePost(), CanExecuteUpdateOrDelete);

            LoadPosts();
        }

        private async Task LoadPosts()
        {
            var posts = await _postService.GetPostsAsync();
            Posts = new ObservableCollection<PostRecord>(posts);
        }

        private async Task CreatePost()
        {
            var newPost = new PostRecord
            {
                Title = NewPostTitle,
                Body = NewPostBody,
                UserId = "1"  // You can modify this as needed
            };

            var createdPost = await _postService.CreatePostAsync(newPost);
            Posts.Add(createdPost);
            NewPostTitle = string.Empty;
            NewPostBody = string.Empty;
        }

        private async Task UpdatePost()
        {
            if (SelectedPost != null)
            {
                var updatedPost = await _postService.UpdatePostAsync(SelectedPost.Id, SelectedPost);
                var postIndex = Posts.IndexOf(SelectedPost);
                Posts[postIndex] = updatedPost;
            }
        }

        private async Task DeletePost()
        {
            if (SelectedPost != null)
            {
                await _postService.DeletePostAsync(SelectedPost.Id);
                Posts.Remove(SelectedPost);
                SelectedPost = null;
            }
        }

        private bool CanExecuteUpdateOrDelete() => IsPostSelected;
    }
}
