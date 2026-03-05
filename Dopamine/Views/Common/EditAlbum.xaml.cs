using Dopamine.ViewModels.Common;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace Dopamine.Views.Common
{
    public partial class EditAlbum : UserControl
    {
        public EditAlbum()
        {
            InitializeComponent();
        }

        private void CopyAlbumTitleToClipboard(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is EditAlbumViewModel vm && !string.IsNullOrEmpty(vm.AlbumViewModel.AlbumTitle))
            {
                try
                {
                    Clipboard.SetText(vm.AlbumViewModel.AlbumTitle);
                }
                catch
                {
                    // 可选：失败处理或提示用户
                }
            }
        }

        private void CopyAlbumArtistToClipboard(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is EditAlbumViewModel vm && !string.IsNullOrEmpty(vm.AlbumViewModel.AlbumArtist))
            {
                try
                {
                    Clipboard.SetText(vm.AlbumViewModel.AlbumArtist);
                }
                catch
                {
                    // 可选：失败处理或提示用户
                }
            }
        }
    }
}
