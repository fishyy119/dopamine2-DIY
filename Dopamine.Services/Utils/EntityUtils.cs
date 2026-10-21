using Dopamine.Core.Base;
using Dopamine.Core.Utils;
using Dopamine.Data;
using Dopamine.Data.Entities;
using Dopamine.Data.Metadata;
using Dopamine.Services.Entities;
using Dopamine.Services.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dopamine.Services.Utils
{
    public static class EntityUtils
    {
        public static bool FilterAlbums(AlbumViewModel album, ISearchService search)
        {
            // Trim is required here, otherwise the filter might flip on the space at the beginning (and probably at the end)
            // 查询的首尾空白清理和分词统一由 SearchService 处理。
            return search.IsMatch(GetSearchFields(album));
        }

        public static bool FilterArtists(ArtistViewModel artist, ISearchService search)
        {
            // Trim is required here, otherwise the filter might flip on the space at the beginning (and probably at the end)
            // 查询的首尾空白清理和分词统一由 SearchService 处理。
            return search.IsMatch(artist.ArtistName);
        }

        public static bool FilterGenres(GenreViewModel genre, ISearchService search)
        {
            // Trim is required here, otherwise the filter might flip on the space at the beginning (and probably at the end)
            // 查询的首尾空白清理和分词统一由 SearchService 处理。
            return search.IsMatch(genre.GenreName);
        }

        public static bool FilterTracks(TrackViewModel track, ISearchService search)
        {
            // Trim is required here, otherwise the filter might flip on the space at the beginning (and probably at the end)
            // 查询的首尾空白清理和分词统一由 SearchService 处理。
            return search.IsMatch(GetSearchFields(track));
        }

        public static string[] GetSearchFields(TrackViewModel track)
        {
            return new[] { track.TrackTitle, track.ArtistName, track.AlbumTitle, track.FileName, track.Year.ToString() };
        }

        public static string[] GetSearchFields(AlbumViewModel album)
        {
            return new[] { album.AlbumTitle, album.AlbumArtist, album.Year.ToString() };
        }

        public static async Task<List<TrackViewModel>> OrderTracksAsync(IList<TrackViewModel> tracks, TrackOrder trackOrder)
        {
            var orderedTracks = new List<TrackViewModel>();

            await Task.Run(() =>
            {
                switch (trackOrder)
                {
                    case TrackOrder.Alphabetical:
                        orderedTracks = tracks.OrderBy((t) => !string.IsNullOrEmpty(FormatUtils.GetSortableString(t.TrackTitle)) ? FormatUtils.GetSortableString(t.TrackTitle) : FormatUtils.GetSortableString(t.FileName)).ToList();
                        break;
                    case TrackOrder.ByAlbum:
                        orderedTracks = tracks.OrderBy((t) => FormatUtils.GetSortableString(t.AlbumArtist)).ThenBy((t) => FormatUtils.GetSortableString(t.AlbumTitle)).ThenBy((t) => t.SortDiscNumber).ThenBy((t) => t.SortTrackNumber).ToList();
                        break;
                    case TrackOrder.ByFileName:
                        orderedTracks = tracks.OrderBy((t) => FormatUtils.GetSortableString(t.FileName)).ToList();
                        break;
                    case TrackOrder.ByRating:
                        orderedTracks = tracks.OrderByDescending((t) => t.Rating).ToList();
                        break;
                    case TrackOrder.ReverseAlphabetical:
                        orderedTracks = tracks.OrderByDescending((t) => !string.IsNullOrEmpty(FormatUtils.GetSortableString(t.TrackTitle)) ? FormatUtils.GetSortableString(t.TrackTitle) : FormatUtils.GetSortableString(t.FileName)).ToList();
                        break;
                    case TrackOrder.None:
                        orderedTracks = tracks.ToList();
                        break;
                    default:
                        // By album
                        orderedTracks = tracks.OrderBy((t) => FormatUtils.GetSortableString(t.AlbumTitle)).ThenBy((t) => t.SortDiscNumber).ThenBy((t) => t.SortTrackNumber).ToList();
                        break;
                }
            });

            return orderedTracks;
        }
    }
}
