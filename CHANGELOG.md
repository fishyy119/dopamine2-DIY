# Changelog

Notable changes to Dopamine and this DIY version are recorded here.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).
Version numbers retain the original upstream and DIY release labels.

## [Unreleased]

### Added

- Added full-pinyin search for Chinese text across existing searchable fields
- Added romaji search for Japanese text, including kanji

### Changed

- Search now normalizes character width, hiragana/katakana, and case while preserving substring matching and requiring all search terms to match
- Replaced ToolGood with EzPinyin for Chinese pinyin conversion and sorting prefixes

## [2.0.10+koifish.7] - 2026-03-29

### Added

- Repair invalid path in .m3u files (by M3U Manager)

### Changed

- Bump M3U Manager to 0.2.0

## [2.0.10+koifish.6] - 2026-03-06

### Added

- Enable quick copying of album and track information in the edit dialog

## [2.0.10+koifish.5] - 2026-02-15

### Changed

- Bump M3U Manager to 0.1.3

### Fixed

- Use uniform scaling for album cover rather than fill

## [2.0.10+koifish.4] - 2025-10-30

### Added

- Added the "Open with M3U Manager" option in the context menu of playlist

### Changed

- Categorize Artists and Genres using Pinyin/Romaji initials for Chinese and Japanese characters
- Changed 'edit' to 'rename' in the playlist right-click menu

## [2.0.10+koifish.3] - 2025-08-24

### Removed

- Removed negative offset from popup menus
- Removed the dependency on ColorHelper

## [2.0.10+koifish.2] - 2025-03-19

### Changed

- Better "Follow the album cover color"

## [2.0.10+koifish.1] - 2025-03-15

### Fixed

- Add scrollbar to "Add to Playlist" menu for long item lists

## 2.0.10 - 2024-08-10

### Added

- Added a Catalan translation
- Added a Farsi translation

### Changed

- Links to website and social networks have been updated
- Update checker now points to GitHub
- Updated the Chinese (Traditional) translation
- Updated the Indonesian translation
- Updated the Italian translation
- Updated the Portuguese translation
- Updated the Russian translation
- Updated the Serbian (Cyrillic) translation
- Updated the Serbian (Latin) translation
- Updated the Swedish translation
- Updated the Spanish translation

## 2.0.9 - 2022-02-08

### Added

- Added a Blacklist
- Added a Kurdish translation

### Changed

- Updated the Chinese (Simplified) translation
- Updated the Finnish translation
- Updated the Indonesian translation
- Updated the Portuguese (Brazil) translation
- Updated the Portuguese (Portugal) translation

## 2.0.8 - 2020-04-10

### Changed

- Updated the Bulgarian translation
- Updated the Chinese Traditional translation
- Updated the Spanish translation

### Fixed

- Progress bar goes to zero when a song is paused

## 2.0.7 - 2020-04-03

### Changed

- Updated the Danish translation

### Fixed

- Dopamine prevents Windows from sleeping when a song is paused
- Overriding screensaver should be optional

## 2.0.6 - 2020-04-02

### Added

- Added Discord Rich Presence
- Added a Finnish translation

### Changed

- Updated the Bulgarian translation
- Updated the French translation
- Updated the German translation
- Updated the Indonesian translation
- Updated the Korean translation
- Updated the Serbian (Cyrillic) translation
- Updated the Serbian (Latin) translation

### Removed

- Removed support for LyricsWiki. The site was shut down.

### Fixed

- Fixed Folder screen compatibility issue with the Dopamine 3 database
- Fixed volume tooltip overlaps volume control numbers

## 2.0.5 - 2020-09-22

### Added

- Added support for the APE file format
- Added support for ModernFlyouts (<https://github.com/ShankarBUS/ModernFlyouts/>)

### Changed

- Updated the Portuguese (Portugal) translation
- Updated the Chinese (Simplified) translation

## 2.0.4 - 2020-09-05

### Changed

- Updated the Hungarian translation

### Fixed

- Playlists now load slightly faster
- Album list does not always reflect the choice of artist type (Song artists, Album artists or All artists)
- Audio is resampled to lower quality instead of playing at full quality

## 2.0.3 - 2020-07-11

### Added

- The "Songs" screen now remembers your sorting preferences!

### Changed

- Updated the Bulgarian translation
- Updated the Chinese translation
- Updated the Indonesian translation
- Updated the Portuguese (Brazil) translation
- Updated the Russian translation
- Updated the Serbian (Cyrillic)
- Updated the Serbian (Latin)
- Updated the Spanish translation
- Updated the Swedish translation
- Updated the Ukranian translation

### Fixed

- Fixed a crash that occurs when using WinDock
- The order of artists is not respected on now playing screens (Remove and re-add collection folders to apply this fix)

## 2.0.2 - 2020-06-17

### Added

- It is now possible to sort albums by year ascending or descending
- Added a smart playlist rule "does not contain"
- Added an Indonesian translation
- Added Serbian translations

### Changed

- Updated the Japanese translation
- Updated the Polish translation

### Fixed

- Startup crash which is caused by the spectrum analyzer
- ".opus" files are ignored
- ".aif" files are ignored
- When double-clicking a playlist, it is not enqueued.
- A crash which occurs when creating a new playlist after deleting the playlists folder
- PC goes to sleep while playing music
- Fixed a focus stealing issue when skipping inside songs using the arrow keys

## 2.0.1 - 2020-01-12

### Added

- Added support for m3u8 playlists

### Changed

- Reverted some changes to audio decoding. This hopefully removes lag when decoding high bitrate files.

## 2.0 - 2020-01-11

### Added

- Added a setting to remove songs from disk in the Now Playing context menu
- Added folder view
- Added smart playlists
- It is now possible to sort songs by album, even when no album is selected.
- The search field can now be activated by pressing CTRL+F
- Added sort by "date added" and "date created" in the song screen
- It is now possible to drop files on now playing, when no songs are playing.
- It is now possible to drop folders on the playlists screen
- Added rating and love to the bottom left now playing pane on the main screen
- Added a Japanese translation
- Added a Norwegian translation

### Changed

- Changed the overall design
- Tweaked the now playing screen
- Updated the Bulgarian translation
- Updated the Chinese (Simplified) translation
- Updated the Chinese (Traditional) translation
- Updated the Croatian translation
- Updated the German translation
- Updated the Greek translation
- Updated the Hungarian translation
- Updated the Italian translation
- Updated the Japanese translation
- Updated the Korean translation
- Updated the Polish translation
- Updated the Portuguese (Portugal) translation
- Updated the Russian translation
- Updated the Spanish translation
- Updated the Swedish translation
- Updated the Ukrainian translation
- Updated the Vietnamese translation

### Removed

- Removed song covers in playlists. Loading images in memory causes a huge memory footprint for large playlists.
- Removed the "Frequent" page

### Fixed

- Added an extra button to access Now Playing
- Multiple memory leaks
- Lyrics scrolling doesn't work correctly for lyrics which have no timestamps
- It is impossible to fetch lyrics from LyricWikia when song artist or title have special casing and special characters
- The spectrum analyzer causes crashes
- Artists or genres which are identical, but use a different casing, are shown as multiple artists or genres.
- Enabling the system notification on Windows 10 N causes a crash
- Rating songs makes the UI non-responsive
- Songs which are enqueued from Explorer, are enqueued in the wrong order.
- It is impossible to delete songs from playlists, for which the song files have been removed from or renamed on disk.
- Pressing multimedia keys causes glitches when the spectrum analyzer is enabled
- System notifications causes a crash when playing a song which has a malformed track number
- Artists and Genres screens don't show songs for the Cyrillic alphabet
- Artists info pane displays white stars instead of actual artist pictures
- Lyrics "Search again" does not work
- Lowered CPU and memory usage
- Short sound after starting up
- Zero-sized images which are embedded in audio files, are mistakenly treated as valid images.
- Queued songs which are not in the collection (e.g. from playlists), are not resumed after a restart.
- Playlists containing the same song multiple times, confuses play previous and play next.
- Incorrect ordering of timed lyrics which contain duplicate timestamps
- Tray icon is not visible when using light system theme in Windows 10
- It is not possible to type + and - in the last.fm password box
- Removing all songs from Now Playing causes a crash
- Discs are ordered wrong when an album has more than 10 discs
- Pressing the keyboard left arrow doesn't jump a song back to the beginning when it has been playing for less than 15 seconds
- Lyrics are not saved to the audio files
- Lyrics are not displayed the 1st time the lyrics page is shown
- File size information in the songs lists is rounded down
- It is not possible to add songs to playlists from the Now Playing screen
- Playback order is not reset when song order is changed on the songs list and songs are enqueued again
- Multimedia play and pause keys are not always working
- External control server fails to retrieve the current track

## 1.5.14 - 2018-05-16

### Changed

- Updated the Chinese (Simplified) translation
- Updated the Korean translation
- Updated the Swedish translation
- Decreased the minimum window size for people using narrow or vertical monitors
- Before adding a collection folder, a check is now performed to ensure its content is accessible.
- Improved parsing of lyrics

### Fixed

- We now don't crash anymore when a collection folder's content is not accessible.
- Indexing stops when encountering an unreadable file or folder
- Fixed another issue playing files which contain special characters
- Fixed multiple crashes when searching for lyrics and when editing lyrics
- Fixed a crash which occurs when indexing MP3 files which have corrupt headers
- Fixed a small issue displaying lyrics download timeout settings

## 1.5.13 - 2018-04-18

### Changed

- Improved parsing of timestamped lyrics
- Updated the Chinese (Simplified) translation
- Updated the Korean translation
- Updated the Portuguese (Portugal) translation
- Updated the Vietnamese translation
- Updated the Spanish translation
- Updated the Swedish translation

### Fixed

- Fixed MetroLyrics parsing issues
- Spectrum styles are not localized

## 1.5.12 - 2018-04-02

### Added

- Songs can now be added to Now Playing by dragging audio and playlist files from Explorer

### Changed

- Updated the Russian translation
- Updated the Vietnamese translation

### Fixed

- Files cannot be enqueued from Windows Explorer since version 1.5.11

## 1.5.11 - 2018-03-21

### Added

- Added support for N editions of Windows (Even without Media Feature Pack)

### Changed

- Updated the Chinese (Simplified) translation
- Updated the Croatian translation
- Updated the Greek translation
- Updated the Korean translation
- Updated the Portuguese (Portugal) translation
- Updated the Swedish translation

### Fixed

- Fixed a crash which happens just after startup on some editions of Windows 10
- Playback fails on 32 bit Windows versions

## 1.5.10 - 2018-02-10

### Added

- Added option to center lyrics

### Fixed

- Fixed an issue where indexing fails completely if at least 1 subdirectory is inaccessible
- Fixed checkboxes to show/hide folders from collection are not displayed
- Fixed high memory usage when shuffling a lot of songs

## 1.5.9 - 2018-01-28

### Fixed

- Fixed albums not being displayed for artists and genres which only have 1 album

## 1.5.8 - 2018-01-23

### Added

- Added support for the WMA Lossless file format

### Changed

- Updated the Bulgarian translation
- Updated the Chinese (Simplified) translation
- Updated the Portuguese (Brazil) translation

### Fixed

- Fixed a problem playing folders and playlists from Windows Explorer
- Fixed sorting by track number in the songs screen
- Fixed possible crash when drawing the list of albums

## 1.5.7 - 2018-01-12

### Changed

- Updated the Portuguese (Brazil) translation
- Updated the Spanish translation
- Updated the Vietnamese translation

### Fixed

- Fixed multiple issues concerning lyrics support

## 1.5.6 - 2018-01-06

### Added

- Added Croatian translation
- Added an option to use all speakers on multi-speaker systems

### Changed

- Updated the Spanish translation

### Fixed

- Fixed strange behaviour when playing files from Windows Explorer
- Fixed wrong Play and Skip count order on the Songs screen
- Fixed bad album cover quality on 4K displays with 250% scaling

## 1.5.5 - 2017-12-06

### Added

- Added support for the Opus file format
- Added optional automatic download of album covers from the Internet
- Added optional enqueue of other audio files which are in the same folder as a double-clicked audio file
- Added button to download covers from the edit song window
- Added Vietnamese translation

### Changed

- Updated the French translation
- Updated the Korean translation
- Updated the Portuguese (Portugal) translation

### Fixed

- Fixed another conflict between playing a file from Explorer and loading of saved queued songs
- Fixed a potential issue when changing album covers

## 1.5.4 - 2017-11-24

### Changed

- Updated the Spanish translation

### Fixed

- A conflict between playing a file from Explorer and loading of saved queued songs
- User interface becomes unresponsive when a collection folder contains the characters "{" and/or "}"
- Click to play from the Frequent screen doesn't work anymore
- Fixed missing lyrics editor in song edit window
- Media keys don't work since the last release

## 1.5.3 - 2017-11-22

### Changed

- Updated the French translation
- Updated the German translation
- Updated the Swedish translation
- Updated the Turkish translation

### Fixed

- Korean language file is missing in installable version
- Indexing fails for all files if 1 of the files has a path which is too long
- Some broken files generate faulty artists, genres and albums in the collection during indexing
- Portable version update notification forwards the user to the installer instead of download location
- Files with special characters fail to play
- Buttons don't work on the system notification
- A thick accented window border is displayed when maximized with a hidden Windows Taskbar
- Last selected settings page is not remembered when going back to Settings from the Collection or Information pages

## 1.5.2 - 2017-11-21

### Changed

- Updated the Korean translation

### Fixed

- Crash on startup when using a non-English language
- Menu texts not updated when switching language

## 1.5.1 - 2017-11-20

### Fixed

- Potential database issues while indexing album covers in the background

## 1.5 - 2017-11-16

### Added

- Added a Context Menu option to play (enqueue) selected tracks
- Added option to use native Windows 10 notifications
- The artists information screen now shows an image for similar artists
- Added option to toggle display of song covers in playlists and Now playing
- Added support for Monkey's Audio APE file format
- Added an external control API (Remote control can be enabled in Settings > Playback > External control).
  For developers, see <https://github.com/Magentaize/Dopamine-ExternalControl-Support>

### Changed

- Updated audio engine to CSCore 1.2.1.2 and changed the audio decoder.
- Updated the Russian translation
- Updated the Spanish translation
- Updated the Swedish translation
- Updated the Chinese (Simplified)
- Updated the German translation
- Updated the Portuguese (Portugal) translation
- Updated the Korean translation
- When double clicking files in Windows Explorer, we now start in Mini Player mode
- Re-introduced automatic loop when performing shuffled playback. There is now an option to disable it.
- Album covers are updated in the background
- Added some options for reloading of album covers
- It's now possible to check for updates at startup only (without periodic update check)

### Removed

- Removed the splash screen

### Fixed

- Some WAV files fail to play
- Multichannel FLAC files fail to play
- Indexer starts even when disabled in the settings
- Indexer starts too often due to corrupt or unreadable files
- Window is restored behind the task bar when double clicking tray icon
- "Unknown artist", "Unknown album", "Unknown genre" are not translated
- Fixed multiple issues when removing songs from Now Playing
- Fixed a crash when moving mini players around when the playlist window is opened
- It is possible to scrobble tracks by skipping to the end of the track
- Tracks on the playlists screen are not cleared when the last playlist is deleted
- Unable to type underscore in Last.fm user field
- Crash when attempting to jump to albums using key presses
- Renaming a playlist does not update the playlist name in the context menus

## 1.3 - 2017-07-07

Disclaimer:  

Playlist support was rewritten from scratch. Your existing playlists will not be migrated.
Save your playlists to files manually before upgrading to this version. Import your saved
playlists after upgrading to this version.

### Added

- Added a button to export album and song covers to files
- Added an option to switch between 4 styles of spectrum analyzers
- Smoother list animations
- Added extra album sorting option "By date created"
- Added optional play count, skip count and date last played column to the songs screen
- Added a context menu option to add the playing song to a playlist
- Left and right arrow keys allow jumping 5 or 15 seconds backward or forward (Information > help for hotkeys)
- Added an option to follow the album cover color
- Added support for local lyrics files (LRC)
- Added audio device selection
- Changes to the collection folders are now detected automatically
- Changes to the Music\Dopamine\Playlists folder are now detected automatically
- Added NeteaseLyrics support
- Added XiamiLyrics support
- Double clicking a playlist queues and plays the songs of that playlist
- The keyboard space bar now toggles play and pause

### Changed

- Lyrics are not downloaded anymore when the lyrics screen is not visible
- When using "Loop one", pressing Previous or Next now skips to the previous or next song.
- Playlists support has been rewritten: playlists are now saved to files automatically.
- The 'Cloud' screen was renamed to 'Frequent' and the layout was changed
- The song title on playback info panes now scales to the available width
- Updated the Swedish translation
- Updated the Spanish translation
- Updated the French translation
- Updated the Turkish translation
- Updated the Bulgarian translation
- Updated the Greek translation
- Updated the Polish translation
- Updated the German translation
- Updated the Russian translation

### Fixed

- Automatic install or download button for updates is confusing for some users
- Improved sizing of time indicator on the main window
- A potential crash when trying to figure out the Windows version at startup.
- Blurry context menu, notification window and tooltips at desktop scaling higher as 100%
- Timestamped lyrics for songs longer than 1 hour don't work
- Audio files which reside on a NAS cannot be played from playlists
- Dopamine process remains active in Task Manager after a crash
- Tray controls are positioned outside of the screen when using desktop scaling > 100%
- Fixed an occasional crash which happened when automatically scrolling to the playing song
- Mini player windows are buggy in Windows 10 tablet mode
- A possible startup crash caused by the tray icon
- A conflict with the search box when changing the volume by pressing - or +
- Tray controls don't respect the position of the Windows Taskbar.
- Added optional 'Remove from disk' option to the song context menu
- Date in the Song Information window is not localized
- WASAPI Exclusive mode plays high pitched sound
- Play next doesn't work correctly when shuffle is enabled

## 1.2.4 - 2017-01-03

### Fixed

- Artists are not sorted alphabetically
- "Shuffle all" doesn't shuffle

## 1.2.3 - 2017-01-01

### Fixed

- Instability on 32-bit Windows versions
- Fixed a bug in the Artists sorting

## 1.2.2 - 2016-12-30

Disclaimer:

This release requires unpinning and pinning Dopamine from and to the Windows Taskbar.
That is expected, and caused by changes to the installer, see (*). Please don't e-mail about this.

### Added

- Added MetroLyrics as extra automatic lyrics provider (because Chartlyrics went dead)
- Added an option to enable/disable automatic lyrics providers individually and an option to set a lyrics download timeout
- Mouse XButton1 plays the previous song, XButton2 plays the next song.

### Fixed

- Artists sorting (song, album, all) was restored after an unmanageable amount of complaints about its removal.
- Lyrics search stops if querying 1 lyrics provider fails, even if there are still other providers to left to use.
- Mute doesn't stick when the next song starts playing
- (*) Hopefully fixed an issue with the installer, which caused missing files on some computers (I hate Windows installer).

## 1.2.1 - 2016-12-24

### Fixed

- A bug where the installer fails to upgrade from a previous version

## 1.2 - 2016-12-24

### Added

- The last played song and its progress is now optionally remembered on next start
- Added "Play next" feature
- Songs screen has an optional column that shows if a song has lyrics
- Possibility to re-order songs by drag and drop in playlists and Now Playing screen
- A more visible tray icon for systems which are not running Windows 10
- It is now possible to edit tags in Now Playing screen and Mini Player
- Added options to show a notification when pausing and resuming a song
- Added Last.fm scrobbling
- Added a 'Love' button which can send 'Love' and 'Unlove' requests to Last.fm
- Added lyrics support (automatic lyrics download, edit lyrics, and screen to display lyrics while playing)
- Added an option to modify how much the volume changes when scrolling with the mouse on the volume bar
- Collection refresh on startup is now optional
- Added an Artist information screen
- Added download of album covers from the Internet
- Added "Play with Dopamine" to the Windows folder context menu
- Added "Add to playlist > New playlist..." on the context menu of all screens
- Added download of cover art for albums

### Changed

- When enabling "Open the last selected page at startup", the last selected Now Playing page is now also remembered.
- Upgraded to the latest version of the CSCore Audio Library
- Changed the album selection box, as it wasn't visible enough.
- The notification window now puts emphasis on the song title instead of the artist name
- Sorting by album on the Songs screen now respects the track order inside an album
- The "Columns..." button on Songs screen was moved to right-click menu
- The playback controls pane is now narrower
- Improved the quality of the cloud screen covers
- Improved the "Follow song" description as it was confusing
- "See you later" screen when closing is only displayed when there are background tasks remaining
- Updated the Chinese Traditional translation
- Updated the Chinese Simplified translation
- Updated the Spanish translation
- Updated the Greek translation
- The comment field is now multiline
- Moved the "Shuffle all" button to a more logical place

### Removed

- Removed song covers from playlists. They were in fact albums covers and were confusing for users.

### Fixed

- Improved sorting on the Songs screen
- The same audio files are added multiple times to the collection in some cases
- Removed the blurry background when semantic zoom is visible. It made semantic zoom harder to read.
- The main window is still visible in the ALT-TAB menu when minimized or closed to the tray
- Multiple issues when switching audio devices
- Notifications cause closing of tray controls
- Opening a playlist causes a crash
- A lot of bugs in processing of metadata
- Unrating a song is now easier
- Refreshing of covers is more fluid
- Song covers are more accurate. Covers stored in or with the audio files have higher priority than album covers.
- Fixed now playing information not updated when metadata changes
- MusicBee ratings are not read from MP3's
- "Follow the Windows color", which was broken since version 1.1.
- A big memory leak
- Search field not always visible when coming back from Settings or Information pages
- Album art is not displayed on the background of the Now playing screen when double-clicking files from explorer
- The Manual equalizer preset is not translated when selected
- Chinese metadata is garbled
- Clicking a scrollbar plays the selected item
- Rating to MP3 files doesn't work if the file is not in the collection
- Windows case insensitivity for files and folder names isn't respected

## 1.1.1 - 2016-09-11

### Changed

- Updated the Portuguese translation

### Fixed

- Dopamine is not running as a 64 bit application on 64 bit systems

## 1.1 - 2016-09-09

### Added

- Added a 10 band Equalizer with 17 built-in presets and possibility to create and save custom presets
- Added a customizable online search for videos and lyrics
- Added a confirmation dialog when enabling WASAPI Exclusive mode
- Added a shuffle all button
- Added a Bitrate column on the Songs screen
- Added importing of .wpl playlists
- Added Italian translation
- Added Polish translation
- Added Ukrainian translation

### Changed

- Dopamine was translated from VB.NET to C# and is now Open Source Software! Source code: <https://github.com/digimezzo/Dopamine>
- This version requires .NET Framework 4.6.1
- Updated to the latest release of the CSCore audio library
- Shuffle doesn't perform a loop anymore. It is now possible to enable shuffle and loop individually.
- Updated the Russian translation
- Updated the Greek translation
- Updated the Portuguese translation

### Removed

- Removed the search field on the Cloud screen

### Fixed

- Impossible to import some .m3u and .zpl playlists
- Collection updating incorrectly when a new collection folder is added just after removing another
- Songs having a white space as Genre cause the Genres list to be empty (A refresh of the collection is required to apply this fix)
- Updating album metadata causes an album with title 'Multiple Values' to appear
- Files removed in Windows are not removed from the collection
- Album art is cut off on lower left playback information pane on the main screen
- Dialogs show a 1px bottom border at some resolutions
- Duplicate semantic Artist and Genre groups appear after performing a search
- Removed songs can still be played when pressing the play button
- Bluetooth headset play/pause button not detected

## 1.0 - 2016-06-29

### Added

- Final 1.0 release!
- Modern design with dynamic User Interface
- Light and dark theme
- Color schemes: 6 built-in color schemes, supports adding custom color schemes
- Plays wav, mp3, ogg vorbis, flac, wma and m4a/aac.
- Organizes music by Artist, Genre and Album.
- Tag editor
- Supports playlist creation (m3u) and import (m3u, zpl)
- Multimedia key support
- Multilanguage
- Notification area controls
- Notifications with optional audio controls
- Multiple modes: Full, Cover player, Micro player, Nano Player.
- Now playing screen

[Unreleased]: https://github.com/fishyy119/dopamine2-DIY/compare/2.0.10+koifish.7...HEAD
[2.0.10+koifish.7]: https://github.com/fishyy119/dopamine2-DIY/compare/2.0.10+koifish.6...2.0.10+koifish.7
[2.0.10+koifish.6]: https://github.com/fishyy119/dopamine2-DIY/compare/2.0.10+koifish.5...2.0.10+koifish.6
[2.0.10+koifish.5]: https://github.com/fishyy119/dopamine2-DIY/compare/2.0.10+koifish.4...2.0.10+koifish.5
[2.0.10+koifish.4]: https://github.com/fishyy119/dopamine2-DIY/compare/2.0.10+koifish.3...2.0.10+koifish.4
[2.0.10+koifish.3]: https://github.com/fishyy119/dopamine2-DIY/compare/2.0.10+koifish.2...2.0.10+koifish.3
[2.0.10+koifish.2]: https://github.com/fishyy119/dopamine2-DIY/compare/2.0.10+koifish.1...2.0.10+koifish.2
[2.0.10+koifish.1]: https://github.com/fishyy119/dopamine2-DIY/releases/tag/2.0.10+koifish.1
