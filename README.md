# Rhymba API net core SDK
Net Core SDK for [VL Group's](https://vlgroup.com/) Rhymba API.

![Build Status](https://github.com/vlgroup/rhymba-netcore-sdk/actions/workflows/build.yml/badge.svg) [![License](https://img.shields.io/github/license/vlgroup/rhymba-netcore-sdk)](https://github.com/vlgroup/rhymba-netcore-sdk/blob/main/LICENSE)

# Documentation
[Net Core SDK Documentation](https://documentation.vlgroup.com/sdk/netcore)

[General Instructions](https://documentation.vlgroup.com) for the Rhymba API.

Sign Up for the [Rhymba API](https://rhymbamanager.vlgroup.com/APISignup).

# Quick Start
```csharp
// create instance of the rhymba client
var rhymbaClient = new RhymbaClient("YOUR_ACCESS_TOKEN", "YOUR_ACCESS_SECRET");

// create a search for some songs (media)
var searchService = rhymbaClient.GetServices().GetSearchService();
var searchRequest = new SearchRequest()
{
    keyword = "Taylor Swift",
    top = 25
};

var mediaSearcher = searchService.GetMedia();
var media = await mediaSearcher.Search(searchRequest);

// get a stream for the first track we found
var streamingService = rhymbaClient.GetServices().GetContentService().GetStreaming();
var getStreamRequest = new GetStreamRequest()
{
    bitrate = 128,
    encoding = GetStreamEncoding.MP3,
    https = true,
    mediaId = media.results[0].id,
    mono = false,
    protocol = GetStreamProtocol.HLS,
};

var getStreamResponse = await streamingService.GetStream(getStreamRequest);

// get the album cover for this track to display
var albumImageService = rhymbaClient.GetServices().GetRhymbaImageService().GetAlbumImage();
var albumCoverRequest = new AlbumCoverRequest()
{
    albumId = media.results[0].album_id,
};

var albumCoverUrl = albumImageService.GetAlbumCoverUrl(albumCoverRequest);
```

The above will search for tracks (media) with the 'Taylor Swift' keyword, generate a URL for an HLS stream that you can feed to your desired player, and get the album cover for the track for display. For more information see the [Documentation](#documentation).