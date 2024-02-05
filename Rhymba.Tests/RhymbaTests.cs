namespace Rhymba.Tests
{
    using Rhymba.Models.Codes;
    using Rhymba.Models.Downloads;
    using Rhymba.Models.Images;
    using Rhymba.Models.Playlist;
    using Rhymba.Models.Previews;
    using Rhymba.Models.Purchases;
    using Rhymba.Models.Streaming;

    public class RhymbaTests : IDisposable
    {
        private readonly RhymbaClient rhymbaClient;
        private readonly MockData mockData;

        public RhymbaTests()
        {
            this.rhymbaClient = new RhymbaClient("YOUR_ACCESS_TOKEN", "YOUR_ACCESS_SECRET");
            this.mockData = new MockData();
        }

        [Fact]
        public async void CreditCodeTests()
        {
            this.mockData.SetupCodes();

            var codeService = this.rhymbaClient.GetServices(this.mockData.GetHttpClient).GetContentService().GetCodes();

            // create a code
            var createCodeRequest = new CreateCreditCodeRequest()
            {
                amount = 10.00m,
                code = Guid.NewGuid(),
                created = DateTime.Now,
                enabled = true,
                name = "rhymba_netcore_sdk_tests",
                notes = "rhymba_netcore_sdk_tests"
            };

            var createCodeResponse = await codeService.CreateCode(createCodeRequest);

            Assert.NotNull(createCodeResponse);
            Assert.Equal(createCodeRequest.name, createCodeResponse.name);

            // edit code
            var editCodeRequest = new EditCodeRequest()
            {
                amount = 100.00m,
                code = createCodeResponse.code,
                created = DateTime.Now,
                enabled = true,
                name = "rhymba_netcore_sdk_tests",
                notes = "rhymba_netcore_sdk_tests"
            };

            var editCodeResponse = await codeService.EditCode(editCodeRequest);

            // nothing is returned on success, error returned on error
            Assert.Null(editCodeResponse);

            // get code
            var getCodeRequest = new GetCodeRequest()
            {
                code = createCodeResponse.code
            };

            var getCodeResponse = await codeService.GetCode(getCodeRequest);

            Assert.NotNull(getCodeResponse);
            Assert.Equal(getCodeRequest.code, getCodeResponse.code);
            Assert.Equal(editCodeRequest.amount, getCodeResponse.amount);
        }

        [Fact]
        public async void DownloadTests()
        {
            this.mockData.SetupDownloads();

            var downloadService = this.rhymbaClient.GetServices(this.mockData.GetHttpClient).GetContentService().GetDownloads();

            // create a download session
            var downloadSesssionRequest = new CreateDownloadSessionRequest()
            {
                albids = new[] { 4027 },
                countryCode = "US",
                downloadLimit = 1,
                test = true,
                ttlSeconds = 86400,
            };

            var downloadSession = await downloadService.CreateDownloadSession(downloadSesssionRequest);

            Assert.NotNull(downloadSession);
            Assert.NotNull(downloadSession.token);

            // get information on the download
            var downloadSessionInformationRequest = new GetDownloadSessionInformationRequest()
            {
                token = downloadSession.token
            };

            var downloadSessionInformation = await downloadService.GetDownloadSessionInformation(downloadSessionInformationRequest);

            Assert.NotNull(downloadSessionInformation);
            Assert.NotNull(downloadSessionInformation.token);
            Assert.Equal(downloadSession.token, downloadSessionInformation.token);

            // update download session
            var downloadSessionUpdateRequest = new UpdateDownloadSessionRequest()
            {
                downloadLimit = 5,
                token = downloadSession.token
            };

            var downloadSessionUpdate = await downloadService.UpdateDownloadSession(downloadSessionUpdateRequest);

            Assert.NotNull(downloadSessionUpdate);
            Assert.NotNull(downloadSessionUpdate.token);
            Assert.Equal(downloadSessionUpdate.token, downloadSession.token);

            // mark download session as complete
            var markSessionCompleteRequest = new MarkDownloadSessionCompleteRequest()
            { 
                token = downloadSession.token
            };

            var markSessionComplete = await downloadService.MarkDownloadSessionComplete(markSessionCompleteRequest);
            
            // nothing is returned on success
            Assert.Null(markSessionComplete);
        }

        [Fact]
        public void PreviewTests()
        {
            var previewService = this.rhymbaClient.GetServices().GetContentService().GetPreview();

            var previewRequest = new GetPreviewRequest()
            {
                filename = "preview.mp3",
                https = true,
                luid = "rhymba_netcore_sdk_tests",
                mediaId = 50695,
                suid = "rhymba_netcore_sdk_tests"
            };

            var previewUrl = previewService.GetPreviewUrl(previewRequest);

            Assert.NotNull(previewUrl);
        }

        [Fact]
        public async void StreamingTests()
        {
            this.mockData.SetupStreaming();

            var streamingService = this.rhymbaClient.GetServices(this.mockData.GetHttpClient).GetContentService().GetStreaming();

            // get stream
            var getStreamRequest = new GetStreamRequest()
            {
                bitrate = 128,
                encoding = GetStreamEncoding.MP3,
                fadeEnd = 0,
                fadeStart = 0,
                https = true,
                luid = "rhymba_netcore_sdk_tests",
                mediaId = 50695,
                mono = false,
                protocol = GetStreamProtocol.HLS,
                suid = "rhymba_netcore_sdk_tests",
                trimEnd = 0,
                trimStart = 0
            };

            var getStreamResponse = await streamingService.GetStream(getStreamRequest);

            Assert.NotNull(getStreamResponse);
            Assert.Equal(getStreamRequest.mediaId, getStreamResponse.id);
            Assert.NotNull(getStreamResponse.url_segment_one);

            // get streams
            var getStreamsRequest = new GetStreamsRequest()
            {
                bitrate = 128,
                encoding = GetStreamEncoding.MP3,
                fadeEnd = 0,
                fadeStart = 0,
                https = true,
                luid = "rhymba_netcore_sdk_tests",
                mediaIds = new[] { 50695, 50696 },
                mono = false,
                protocol = GetStreamProtocol.HLS,
                suid = "rhymba_netcore_sdk_tests",
                trimEnd = 0,
                trimStart = 0
            };

            var getStreamsResponse = await streamingService.GetStreams(getStreamsRequest);

            Assert.NotNull(getStreamsResponse);
            Assert.Equal(getStreamsRequest.mediaIds.Length, getStreamsResponse.Length);
            Assert.Equal(getStreamsRequest.mediaIds[0], getStreamsResponse[0].id);

            // get playlist stream
            var getPlaylistStreamRequest = new GetPlaylistStreamRequest()
            {
                bitrate = 128,
                encoding = GetStreamEncoding.MP3,
                fadeEnd = 0,
                fadeStart = 0,
                https = true,
                luid = "rhymba_netcore_sdk_tests",
                mono = false,
                playlistId = 704,
                protocol = GetStreamProtocol.HLS,
                suid = "rhymba_netcore_sdk_tests",
                trimEnd = 0,
                trimStart = 0
            };

            var getPlaylistStreamResponse = await streamingService.GetPlaylistStream(getPlaylistStreamRequest);

            Assert.NotNull(getPlaylistStreamResponse);
            Assert.True(getPlaylistStreamResponse.Length > 0);
            Assert.NotNull(getPlaylistStreamResponse[0].url_segment_one);
        }

        [Fact]
        public async void ImageTests()
        {
            this.mockData.SetupImages();

            // album artwork
            var albumImageService = this.rhymbaClient.GetServices(this.mockData.GetHttpClient).GetRhymbaImageService().GetAlbumImage();

            var albumCoverRequest = new AlbumCoverRequest()
            {
                albumId = 4027,
                imageOptions = new RhymbaImageOptions()
                {
                    height = 512,
                    width = 512
                }
            };

            var albumCoverUrl = albumImageService.GetAlbumCoverUrl(albumCoverRequest);

            Assert.NotNull(albumCoverUrl);

            // client image
            var rhymbaImageService = this.rhymbaClient.GetServices().GetRhymbaImageService().GetRhymbaImage();

            var iamgeUploadRequest = new RhymbaImageUploadRequest()
            {
                accountId = 13, // MCNEMANAGER, System Id 87
                focusXPercent = 100,
                focusYPercent = 100,
                galleryId = 110, // rhymba_sdk
                image = new FileInfo("rhymba_netcore_sdk_test_image.jpg"),
                title = "rhymba_netcore_sdk_tests"
            };

            var rhymbaImageId = await rhymbaImageService.PutImagge(iamgeUploadRequest);

            Assert.NotNull(rhymbaImageId);

            var rhymbaImageRequest = new RhymbaImageRequest()
            {
                imageId = rhymbaImageId
            };

            var rhymbaImageUrl = rhymbaImageService.GetImageUrl(rhymbaImageRequest);

            Assert.NotNull(rhymbaImageUrl);
        }

        [Fact]
        public async void PurchaseTests()
        {
            this.mockData.SetupPurchases();

            var validationService = this.rhymbaClient.GetServices(this.mockData.GetHttpClient).GetPurchaseService().GetValidation();

            // same purchase item for everything
            var purchaseItem = new PurchaseItem()
            {
                contenttype = ContentType.Album,
                deliverytype = DeliveryType.Web,
                discountamount = 0.0m,
                productid = 4027,
                quantity = 1,
                retail = 7.70m,
                saletype = SaleType.DigitialDownload,
                servicetype = ServiceType.PayPerUse,
                tax = 0.0m
            };

            // get invalid items
            var getInvalidItemsRequest = new GetInvalidItemsRequest()
            {
                countryCode = "US",
                purchasedItems = new[] { purchaseItem }
            };

            var getInvalidItemsResponse = await validationService.GetInvalidItems(getInvalidItemsRequest);

            Assert.NotNull(getInvalidItemsResponse);

            var reportingService = this.rhymbaClient.GetServices().GetPurchaseService().GetReporting();

            // report purchase, use confirm=1 so that we can test finialize purchase as well
            var reportPurchaseRequest = new ReportPurchaseRequest()
            {
                affiliateName = "rhymba_netcore_sdk_tests",
                affiliateTransactionId = Guid.NewGuid().ToString("n"),
                confirm = ConfirmFlag.Confirm,
                countryCode = "US",
                currencyCode = "USD",
                externalUserId = "e2f10f58-69ed-40b9-ac61-fe5ed3306a4a",
                isTest = true,
                purchasedItems = new[] { purchaseItem },
                saleDate = DateTime.UtcNow,
                zip = "11232"
            };

            var reportPurchaseResponse = await reportingService.ReportPurchase(reportPurchaseRequest);

            Assert.NotNull(reportPurchaseResponse);
            Assert.NotNull(reportPurchaseResponse.token);

            // finalize purchase
            var finalizePurchaseRequest = new FinalizePurchaseRequest()
            {
                token = reportPurchaseResponse.token
            };

            var finalizePurchaseResponse = await reportingService.FinalizePurchase(finalizePurchaseRequest);

            Assert.NotNull(finalizePurchaseResponse);
            Assert.NotNull(finalizePurchaseResponse.purchaseid);
        }

        [Fact]
        public async void SearchTests()
        {
            this.mockData.SetupSearch();

            var searchService = this.rhymbaClient.GetServices(this.mockData.GetHttpClient).GetSearchService();

            var searchRequestBuilder = searchService.GetRequestBuilder();

            // create one search request that will work for all collections
            var searchRequest = searchRequestBuilder.Keyword("Taylor Swift").Top(10).GetRequest();

            // albums
            var albumSearcher = searchService.GetAlbums();
            var albums = await albumSearcher.Search(searchRequest);

            Assert.NotNull(albums);
            Assert.NotNull(albums.results);
            Assert.True(albums.results.Any());

            searchRequest.select = null;

            // artists
            var artistSearcher = searchService.GetArtists();
            var artists = await artistSearcher.Search(searchRequest);

            Assert.NotNull(artists);
            Assert.NotNull(artists.results);
            Assert.True(artists.results.Any());

            searchRequest.select = null;

            // media
            var mediaSearcher = searchService.GetMedia();
            var media = await mediaSearcher.Search(searchRequest);

            Assert.NotNull(media);
            Assert.NotNull(media.results);
            Assert.True(media.results.Any());
        }

        [Fact]
        public async void PlaylistTests()
        {
            this.mockData.SetupPlaylist();

            var playlistService = this.rhymbaClient.GetServices(this.mockData.GetHttpClient).GetPlaylistService("3256b144-e656-4eab-9fd7-b7bacb9b818d");

            // create a user
            var playlistUserResponse = await playlistService.GetAccount().Register(new AccountRegisterRequest()
            {
                password = "rhymba_netcore_sdk_tests"
            });

            Assert.NotNull(playlistUserResponse);
            Assert.NotEqual(0, playlistUserResponse.data);

            // login
            var loginResponse = await playlistService.GetAccount().Login(new AccountLoginRequest()
            {
                id = playlistUserResponse.data,
                password = "rhymba_netcore_sdk_tests"
            });

            Assert.NotNull(loginResponse);
            Assert.NotNull(loginResponse.data);
            Assert.NotNull(loginResponse.data.Token);

            // create a playlist
            var playlistResponse = await playlistService.CreatePlaylist(new CreatePlaylistRequest()
            {
                access_token = loginResponse.data.Token,
                name = "rhymba_netcore_sdk_tests"
            });

            Assert.NotNull(playlistResponse);
            Assert.NotNull(playlistResponse.data);
            Assert.NotEqual(0, playlistResponse.data.PlaylistId);

            // add an album to the playlist
            var addAlbumResponse = await playlistService.AddAlbumToPlaylist(playlistResponse.data.PlaylistId, new AddAlbumRequest()
            {
                access_token = loginResponse.data.Token,
                albumId = 1263010627
            });

            Assert.NotNull(addAlbumResponse);
            Assert.NotEqual(0, addAlbumResponse.data);

            // get the playlist and make sure it has the 1 album item on it
            var getPlaylistResponse = await playlistService.GetPlaylist(playlistResponse.data.PlaylistId, new GetPlaylistRequest()
            {
                access_token= loginResponse.data.Token
            });

            Assert.NotNull(getPlaylistResponse);
            Assert.NotNull(getPlaylistResponse.data);
            Assert.Equal(playlistResponse.data.PlaylistId, getPlaylistResponse.data.PlaylistId);
            Assert.Equal(1, getPlaylistResponse.data.Items?.Count);

            // delete the album off the playlist
            var deleteAlbumResponse = await playlistService.DeletePlaylistAlbum(playlistResponse.data.PlaylistId, 1263010627, new DeletePlaylistAlbumRequest()
            {
                access_token = loginResponse.data.Token
            });

            Assert.NotNull(deleteAlbumResponse);

            // get the playlist again and make sure there are only 2 items on it now
            getPlaylistResponse = await playlistService.GetPlaylist(playlistResponse.data.PlaylistId, new GetPlaylistRequest()
            {
                access_token = loginResponse.data.Token
            });

            Assert.NotNull(getPlaylistResponse);
            Assert.NotNull(getPlaylistResponse.data);
            Assert.Equal(playlistResponse.data.PlaylistId, getPlaylistResponse.data.PlaylistId);
            Assert.Equal(0, getPlaylistResponse.data.Items?.Count);


            // delete the playlist
            var deletePlaylistResponse = await playlistService.DeletePlaylist(playlistResponse.data.PlaylistId, new DeletePlaylistRequest()
            {
                access_token = loginResponse.data.Token
            });

            Assert.NotNull(deletePlaylistResponse);
        }

        public void Dispose()
        {

        }
    }
}