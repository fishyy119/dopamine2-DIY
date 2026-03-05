using Dopamine.ViewModels.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dopamine.Views.Common
{
    public partial class EditTrackTagsControl : UserControl
    {
        private EditTrackViewModel ViewModel => this.DataContext as EditTrackViewModel;
        public EditTrackTagsControl()
        {
            InitializeComponent();
        }

        private void CopyLabelContentToClipboard(object sender, MouseButtonEventArgs e)
        {
            if (sender is Label label)
            {
                var vm = this.ViewModel;
                if (vm == null)
                    return;

                string textToCopy = null;

                switch (label.Name)
                {
                    case "TitleLabel":
                        textToCopy = vm.Title.Value;
                        break;
                    case "ArtistsLabel":
                        textToCopy = vm.Artists.Value;
                        break;
                    case "AlbumLabel":
                        textToCopy = vm.Album.Value;
                        break;
                    case "AlbumArtistsLabel":
                        textToCopy = vm.AlbumArtists.Value;
                        break;
                    case "YearLabel":
                        textToCopy = vm.Year.Value;
                        break;
                    case "TrackNumberLabel":
                        textToCopy = vm.TrackNumber.Value;
                        break;
                    case "TrackCountLabel":
                        textToCopy = vm.TrackCount.Value;
                        break;
                    case "DiscNumberLabel":
                        textToCopy = vm.DiscNumber.Value;
                        break;
                    case "DiscCountLabel":
                        textToCopy = vm.DiscCount.Value;
                        break;
                    case "GenresLabel":
                        textToCopy = vm.Genres.Value;
                        break;
                    case "GroupingLabel":
                        textToCopy = vm.Grouping.Value;
                        break;
                    case "CommentLabel":
                        textToCopy = vm.Comment.Value;
                        break;
                }

                if (!string.IsNullOrEmpty(textToCopy))
                    try
                    {
                        Clipboard.SetText(textToCopy);
                    }
                    catch
                    {
                        // 忽略失败
                    }
            }
        }
    }
}
